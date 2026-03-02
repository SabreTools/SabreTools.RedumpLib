using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
#if NETCOREAPP
using System.Net.Http;
using System.Net.Http.Headers;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    public class RedumpClient
    {
        #region Properties

        /// <summary>
        /// Determines if debug outputs are printed
        /// </summary>
        public bool Debug { get; set; } = false;

        /// <summary>
        /// Maximum attempt count for any operation
        /// </summary>
        /// <remarks>Value has to be greater than 0</remarks>
        public int AttemptCount
        {
            get;
            set { field = value < 0 ? 3 : value; }
        } = 3;

        /// <summary>
        /// The timespan to wait before the request times out.
        /// </summary>
        public TimeSpan Timeout
        {
            get => _internalClient.Timeout;
            set
            {
                // Ensure a positive timespan
                if (value <= TimeSpan.Zero)
                    value = TimeSpan.FromSeconds(30);

                _internalClient.Timeout = value;
            }
        }

        /// <summary>
        /// Indicates if existing files will be overwritten
        /// </summary>
        public bool Overwrite { get; set; } = false;

        /// <summary>
        /// Indicates if download errors are ignored
        /// </summary>
        public bool IgnoreErrors { get; set; } = false;

        #endregion

        #region Fields

        /// <summary>
        /// Internal client for interaction
        /// </summary>
#if NETCOREAPP
        private readonly HttpClient _internalClient;
#else
        private readonly CookieWebClient _internalClient;
#endif

        /// <summary>
        /// Indicates if user is logged into Redump
        /// </summary>
        /// <remarks>Modifying to set as true does not change actual logged-in status</remarks>
        private bool _loggedIn = false;

        /// <summary>
        /// Indicates if the user is a staff member
        /// </summary>
        /// <remarks>Modifying to set as true does not change actual staff status</remarks>
        private bool _staffMember = false;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RedumpClient()
        {
#if NETCOREAPP
            _internalClient = new HttpClient(new HttpClientHandler { UseCookies = true });
#else
            _internalClient = new CookieWebClient();
#endif
            Timeout = TimeSpan.FromSeconds(30);
        }

        #region Credentials

        /// <summary>
        /// Validate supplied credentials
        /// </summary>
        /// <param name="username">Redump username</param>
        /// <param name="password">Redump password</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
        public static async Task<bool?> ValidateCredentials(string? username, string? password)
        {
            // If options are invalid or we're missing something key, just return
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            // Try logging in with the supplied credentials otherwise
            var redumpClient = new RedumpClient();
            return await redumpClient.Login(username, password);
        }

        /// <summary>
        /// Login to Redump, if possible
        /// </summary>
        /// <param name="username">Redump username</param>
        /// <param name="password">Redump password</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
        public async Task<bool?> Login(string? username, string? password)
        {
            // Check for already logged in
            if (_loggedIn)
            {
                Console.WriteLine("Already logged in!");
                return true;
            }

            // Credentials verification
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Credentials entered, will attempt Redump login...");
            }
            else if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                Console.Error.WriteLine("Only a username was specified, will not attempt Redump login...");
                return false;
            }
            else if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("No credentials entered, will not attempt Redump login...");
                return false;
            }

            // HTTP encode the password
#if NET20 || NET35 || NET40
            password = Uri.EscapeUriString(password);
#else
            password = WebUtility.UrlEncode(password);
#endif

            // Attempt to login up as many times as the retry count allows
            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    Console.WriteLine($"Login attempt {i + 1} of {AttemptCount}");

                    // Get the current token from the login page
                    var loginPage = await DownloadString(Constants.LoginUrl);
                    string token = Constants.TokenRegex.Match(loginPage ?? string.Empty).Groups[1].Value;

#if NETCOREAPP
                    // Construct the login request
                    var postContent = new StringContent($"form_sent=1&redirect_url=&csrf_token={token}&req_username={username}&req_password={password}&save_pass=0", Encoding.UTF8);
                    postContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    // Send the login request and get the result
                    var response = await _internalClient.PostAsync(Constants.LoginUrl, postContent);
                    string? responseContent = null;
                    if (response?.Content is not null)
                        responseContent = await response.Content.ReadAsStringAsync();
#else
                    // Construct the login request
                    _internalClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    _internalClient.Encoding = Encoding.UTF8;

                    // Send the login request and get the result
                    string? responseContent = _internalClient.UploadString(Constants.LoginUrl, $"form_sent=1&redirect_url=&csrf_token={token}&req_username={username}&req_password={password}&save_pass=0");
#endif

                    // An empty response indicates an error
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        Console.Error.WriteLine($"An error occurred while trying to log in on attempt {i}: No response");
                        continue;
                    }

                    // Explcit confirmation the login was wrong
                    if (responseContent.Contains("Incorrect username and/or password."))
                    {
                        Console.Error.WriteLine("Invalid credentials entered, continuing without logging in...");
                        return false;
                    }

                    // The user was able to be logged in
                    Console.WriteLine("Credentials accepted! Logged into Redump...");
                    _loggedIn = true;

                    // If the user is a moderator or staff, set accordingly
                    if (responseContent.Contains("http://forum.redump.org/forum/9/staff/"))
                        _staffMember = true;

                    return true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An exception occurred while trying to log in on attempt {i}: {ex}");
                }
            }

            Console.Error.WriteLine($"Could not login to Redump in {AttemptCount} attempts, continuing without logging in...");
            return false;
        }

        #endregion

        #region Generic Helpers

        /// <summary>
        /// Download from a URI to a byte array
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <returns>Byte array from the URI, null on error</returns>
        public async Task<byte[]?> DownloadData(string uri)
        {
            // Only retry a positive number of times
            if (AttemptCount <= 0)
            {
                Console.Error.WriteLine("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) Console.WriteLine($"DEBUG: DownloadData(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    return await _internalClient.GetByteArrayAsync(uri);
#elif NET40
                    return await Task.Factory.StartNew(() => _internalClient.DownloadData(uri));
#else
                    return await Task.Run(() => _internalClient.DownloadData(uri));
#endif
                }
                catch { }

                // Intentional delay here so we don't flood the server
                DelayHelper.DelayRandom();
            }

            Console.Error.WriteLine($"Could not download \"{uri}\" after {AttemptCount} attempts");
            return null;
        }

        /// <summary>
        /// Download from a URI to a local file
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <param name="fileName">Filename to write to</param>
        /// <returns>The remote filename from the URI, null on error</returns>
        public async Task<string?> DownloadFile(string uri, string fileName)
        {
            // Only retry a positive number of times
            if (AttemptCount <= 0)
            {
                Console.Error.WriteLine("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) Console.WriteLine($"DEBUG: DownloadFile(\"{uri}\", \"{fileName}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    // Make the call to get the file
                    var response = await _internalClient.GetAsync(uri);
                    if (response?.Content?.Headers is null || !response.IsSuccessStatusCode)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: DownloadFile failed, continuing...");
                        continue;
                    }

                    // Copy the data to a local temp file
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    using (var tempFileStream = File.OpenWrite(fileName))
                    {
                        responseStream.CopyTo(tempFileStream);
                    }

                    return response.Content.Headers.ContentDisposition?.FileName?.Replace("\"", "");
#elif NET40
                    await Task.Factory.StartNew(() => { _internalClient.DownloadFile(uri, fileName); return true; });
                    string? lastFilename = _internalClient.GetLastFilename();
                    if (lastFilename is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: DownloadFile failed, continuing...");
                        continue;
                    }

                    return lastFilename;
#else
                    await Task.Run(() => _internalClient.DownloadFile(uri, fileName));
                    string? lastFilename = _internalClient.GetLastFilename();
                    if (lastFilename is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: DownloadFile failed, continuing...");
                        continue;
                    }

                    return lastFilename;
#endif
                }
                catch { }

                // Intentional delay here so we don't flood the server
                DelayHelper.DelayRandom();
            }

            Console.Error.WriteLine($"Could not download \"{uri}\" after {AttemptCount} attempts");
            return null;
        }

        /// <summary>
        /// Download from a URI to a string
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <returns>String from the URI, null on error</returns>
        public async Task<string?> DownloadString(string uri)
        {
            // Only retry a positive number of times
            if (AttemptCount <= 0)
            {
                Console.Error.WriteLine("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) Console.WriteLine($"DEBUG: DownloadString(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    return await _internalClient.GetStringAsync(uri);
#elif NET40
                    return await Task.Factory.StartNew(() => _internalClient.DownloadString(uri));
#else
                    return await Task.Run(() => _internalClient.DownloadString(uri));
#endif
                }
                catch { }

                // Intentional delay here so we don't flood the server
                DelayHelper.DelayRandom();
            }

            Console.Error.WriteLine($"Could not download \"{uri}\" after {AttemptCount} attempts");
            return null;
        }

        #endregion

        #region Single Page Helpers

        /// <summary>
        /// Process a Redump discs page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="query">Discs query string to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsPage(string query, int pageNumber, bool convertForwardSlashes)
        {
            query = NormalizeQuery(query, convertForwardSlashes);
            string url = string.Format(Constants.DiscsUrl, query, pageNumber);
            return await CheckSingleSitePage(url);
        }

        /// <summary>
        /// Process a Redump discs page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="query">Discs query string to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsPage(string query, int pageNumber, string? outDir, bool convertForwardSlashes)
        {
            query = NormalizeQuery(query, convertForwardSlashes);
            string url = string.Format(Constants.DiscsUrl, query, pageNumber);
            return await CheckSingleSitePage(url, outDir);
        }

        /// <summary>
        /// Process a Redump discs by last modified page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="pageNumber">Page number to use</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsLastModifiedPage(int pageNumber)
        {
            string url = string.Format(Constants.LastModifiedUrl, pageNumber);
            return await CheckSingleSitePage(url);
        }

        /// <summary>
        /// Process a Redump discs by last modified page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsLastModifiedPage(int pageNumber, string? outDir)
        {
            string url = string.Format(Constants.LastModifiedUrl, pageNumber);
            return await CheckSingleSitePage(url, outDir);
        }

        /// <summary>
        /// Process a Redump quicksearch page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="query">Quicksearch query string to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleQuicksearchPage(string query, int pageNumber, bool convertForwardSlashes)
        {
            query = NormalizeQuery(query, convertForwardSlashes);
            string url = string.Format(Constants.QuickSearchUrl, query, pageNumber);
            return await CheckSingleSitePage(url);
        }

        /// <summary>
        /// Process a Redump quicksearch page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="query">Quicksearch query string to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-` in queries</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleQuicksearchPage(string query, int pageNumber, string? outDir, bool convertForwardSlashes)
        {
            query = NormalizeQuery(query, convertForwardSlashes);
            string url = string.Format(Constants.QuickSearchUrl, query, pageNumber);
            return await CheckSingleSitePage(url, outDir);
        }

        /// <summary>
        /// Process a Redump user page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="username">Username to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="lastModified">True to sort by last modified, false otherwise</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleUserPage(string username, int pageNumber, bool lastModified)
        {
            string url = lastModified
                ? string.Format(Constants.UserDumpsLastModifiedUrl, username, pageNumber)
                : string.Format(Constants.UserDumpsUrl, username, pageNumber);
            return await CheckSingleSitePage(url);
        }

        /// <summary>
        /// Process a Redump user page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="username">Username to use</param>
        /// <param name="pageNumber">Page number to use</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="lastModified">True to sort by last modified, false otherwise</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleUserPage(string username, int pageNumber, string? outDir, bool lastModified)
        {
            string url = lastModified
                ? string.Format(Constants.UserDumpsLastModifiedUrl, username, pageNumber)
                : string.Format(Constants.UserDumpsUrl, username, pageNumber);
            return await CheckSingleSitePage(url, outDir);
        }

        /// <summary>
        /// Process a Redump WIP page as a list of possible IDs or disc page
        /// </summary>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        /// <remarks>Limited to moderators and staff</remarks>
        public async Task<List<int>?> CheckSingleWIPPage()
        {
            List<int> ids = [];

            // If the user is not a moderator
            if (!_loggedIn || !_staffMember)
            {
                Console.Error.WriteLine("WIP download functionality is only available to Redump moderators");
                return null;
            }

            // Try to retrieve the data
            string url = Constants.WipDumpsUrl;
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleWIPPage() - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleWIPPage() - No discs found");
                return ids;
            }

            // Otherwise, traverse each dump on the page
            var matches = Constants.NewDiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches)
            {
                if (match is null)
                    continue;

                try
                {
                    if (int.TryParse(match.Groups[2].Value, out int value))
                        ids.Add(value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return ids;
        }

        /// <summary>
        /// Process a Redump WIP page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>List of IDs that were found on success, empty on error</returns>
        /// <remarks>Limited to moderators and staff</remarks>
        public async Task<List<int>?> CheckSingleWIPPage(string? outDir)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleWIPPage();
            if (ids is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleWIPPage(\"{outDir}\") - Client failure");
                return null;
            }

            // Try to download all IDs
            List<int> processed = [];
            foreach (int id in ids)
            {
                try
                {
                    bool downloaded = await DownloadSingleWIPID(id, outDir, rename: false);
                    if (!downloaded && !IgnoreErrors)
                        return processed;

                    processed.Add(id);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return processed;
        }

        /// <summary>
        /// Process a Redump site page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        private async Task<List<int>?> CheckSingleSitePage(string url)
        {
            List<int> ids = [];

            // Try to retrieve the data
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleSitePage(\"{url}\") - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleSitePage(\"{url}\") - No discs found");
                return ids;
            }

            // If we have a single disc page already
            if (dumpsPage.Contains("<b>Download:</b>"))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleSitePage(\"{url}\") - Single disc page");
                var value = Constants.SfvRegex.Match(dumpsPage).Groups[1].Value;
                if (int.TryParse(value, out int id))
                    ids.Add(id);

                return ids;
            }

            // Otherwise, traverse each dump on the page
            var matches = Constants.DiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches)
            {
                if (match is null)
                    continue;

                try
                {
                    if (int.TryParse(match.Groups[1].Value, out int value))
                        ids.Add(value);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return ids;
        }

        /// <summary>
        /// Process a Redump site page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        private async Task<List<int>?> CheckSingleSitePage(string url, string? outDir)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleSitePage(url);
            if (ids is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleSitePage(\"{url}\", \"{outDir}\") - Client failure");
                return null;
            }

            // Try to download all IDs
            List<int> processed = [];
            foreach (int id in ids)
            {
                try
                {
                    bool downloaded = await DownloadSingleSiteID(id, outDir, rename: false);
                    if (!downloaded && !IgnoreErrors)
                        return processed;

                    processed.Add(id);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return processed;
        }

        /// <summary>
        /// Normalize a URL query string
        /// </summary>
        /// <param name="query">Query string to normalize</param>
        /// <param name="convertForwardSlashes">Replace forward slashes with `-`</param>
        /// <returns>Normalized query</returns>
        private static string NormalizeQuery(string query, bool convertForwardSlashes)
        {
            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            query = query.Replace('\\', '-');
            if (convertForwardSlashes)
                query = query.Replace('/', '-');
            else
                query = query.TrimStart('/');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();
            return query;
        }

        #endregion

        #region Download Helpers

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">System to download packs for</param>
        /// <returns>Byte array containing the downloaded pack, null on error</returns>
        public async Task<byte[]?> DownloadSinglePack(PackType packType, RedumpSystem? system)
        {
            try
            {
                if (Debug) Console.WriteLine($"DEBUG: DownloadSinglePack(\"{packType}\", {system})");

                // Determine the base URL, if possible
                string? baseUrl = PackTypeToBaseUrl(packType);
                if (baseUrl is null)
                {
                    if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                    return null;
                }

                string packUri = string.Format(baseUrl, system.ShortName());
                return await DownloadData(packUri);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">System to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        public async Task<bool> DownloadSinglePack(PackType packType, RedumpSystem? system, string? outDir)
        {
            try
            {
                if (Debug) Console.WriteLine($"DEBUG: DownloadSinglePack(\"{packType}\", {system}, \"{outDir}\")");

                // Determine the base URL, if possible
                string? baseUrl = PackTypeToBaseUrl(packType);
                if (baseUrl is null)
                {
                    if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                    return false;
                }

                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                {
                    if (Debug) Console.WriteLine("DEBUG: Output directory was not provided, setting to current directory");
                    outDir = Environment.CurrentDirectory;
                }

                string tempfile = Path.Combine(outDir, "tmp" + Guid.NewGuid().ToString());
                string packUri = string.Format(baseUrl, system.ShortName());

                // Make the call to get the pack
                string? remoteFileName = await DownloadFile(packUri, tempfile);
                if (remoteFileName is null)
                    return false;

                MoveOrDelete(tempfile, remoteFileName, outDir!);
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">System to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadSinglePack(PackType packType, RedumpSystem? system, string? outDir, string? subfolder)
        {
            try
            {
                if (Debug) Console.WriteLine($"DEBUG: DownloadSinglePack(\"{packType}\", {system}, \"{outDir}\", \"{subfolder}\")");

                // Determine the base URL, if possible
                string? baseUrl = PackTypeToBaseUrl(packType);
                if (baseUrl is null)
                {
                    if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                    return false;
                }

                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                {
                    if (Debug) Console.WriteLine("DEBUG: Output directory was not provided, setting to current directory");
                    outDir = Environment.CurrentDirectory;
                }

                string tempfile = Path.Combine(outDir, "tmp" + Guid.NewGuid().ToString());
                string packUri = string.Format(baseUrl, system.ShortName());

                // Make the call to get the pack
                string? remoteFileName = await DownloadFile(packUri, tempfile);
                if (remoteFileName is null)
                    return false;

                MoveOrDelete(tempfile, remoteFileName, outDir!, subfolder);
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual site ID data, if possible
        /// </summary>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
        public async Task<string?> DownloadSingleSiteID(int id)
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.DiscPageUrlTemplate, +id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
                    return null;
                }
                else if (discPage.Contains($"Disc with ID \"{id}\" doesn't exist"))
                {
                    Console.Error.WriteLine($"ID {paddedId} could not be found!");
                    return null;
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download an individual site ID data, if possible
        /// </summary>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="rename">True to rename deleted entries, false otherwise</param>
        /// <returns>True if all data was downloaded, false otherwise</returns>
        public async Task<bool> DownloadSingleSiteID(int id, string? outDir, bool rename)
        {
            // If no output directory is defined, use the current directory instead
            if (string.IsNullOrEmpty(outDir))
                outDir = Environment.CurrentDirectory;

            string paddedId = id.ToString().PadLeft(6, '0');
            string paddedIdDir = Path.Combine(outDir, paddedId);
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.DiscPageUrlTemplate, +id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
                    return false;
                }
                else if (discPage.Contains($"Disc with ID \"{id}\" doesn't exist"))
                {
                    if (rename)
                    {
                        try
                        {
                            if (Directory.Exists(paddedIdDir) && rename)
                                Directory.Move(paddedIdDir, $"{paddedIdDir}-deleted");
                            else
                                Directory.CreateDirectory($"{paddedIdDir}-deleted");
                        }
                        catch { }
                    }

                    Console.Error.WriteLine($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (!Overwrite && File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Check for the last modified date in both pages
                    var oldResult = Constants.LastModifiedRegex.Match(oldDiscPage);
                    var newResult = Constants.LastModifiedRegex.Match(discPage);

                    // If both pages contain the same modified date, skip it
                    if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }

                    // If neither page contains a modified date, skip it
                    else if (!oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }

                    // Check the added date as a backup
                    oldResult = Constants.AddedRegex.Match(oldDiscPage);
                    newResult = Constants.AddedRegex.Match(discPage);

                    // If the downloaded data is invalid or otherwise empty, skip it
                    if (oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} retieved an empty page, skipping...");
                        return false;
                    }
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

                // View Edit History
                if (discPage.Contains($"<a href=\"/disc/{id}/changes/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscChangesSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, "changes.html"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;

                }

                // CUE
                if (discPage.Contains($"<a href=\"/disc/{id}/cue/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscCueSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.cue"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // Edit disc
                if (discPage.Contains($"<a href=\"/disc/{id}/edit/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscEditSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, "edit.html"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // GDI
                if (discPage.Contains($"<a href=\"/disc/{id}/gdi/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscGdiSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.gdi"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // KEYS
                if (discPage.Contains($"<a href=\"/disc/{id}/key/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscKeySubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.key"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // LSD
                if (discPage.Contains($"<a href=\"/disc/{id}/lsd/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscLsdSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.lsd"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // MD5
                if (discPage.Contains($"<a href=\"/disc/{id}/md5/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscMd5Subpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.md5"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // Review WIP entry
                if (Constants.NewDiscRegex.IsMatch(discPage))
                {
                    var match = Constants.NewDiscRegex.Match(discPage);
                    string uri = string.Format(Constants.NewDiscUrlTemplate, match.Groups[2].Value);
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, "newdisc.html"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // SBI
                if (discPage.Contains($"<a href=\"/disc/{id}/sbi/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscSbiSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.sbi"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // SFV
                if (discPage.Contains($"<a href=\"/disc/{id}/sfv/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscSfvSubpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.sfv"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // SHA1
                if (discPage.Contains($"<a href=\"/disc/{id}/sha1/\""))
                {
                    string uri = string.Format(Constants.DiscPageUrlTemplate, +id) + Constants.DiscSha1Subpath;
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.sha1"));
                    if (!IgnoreErrors && remoteName is null)
                        return false;
                }

                // HTML (Last in case of errors)
                using (var discStreamWriter = File.CreateText(Path.Combine(paddedIdDir, "disc.html")))
                {
                    discStreamWriter.Write(discPage);
                    discStreamWriter.Flush();
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual WIP ID data, if possible
        /// </summary>
        /// <param name="id">Redump WIP disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
        /// <remarks>Limited to moderators and staff</remarks>
        public async Task<string?> DownloadSingleWIPID(int id)
        {
            // If the user is not a moderator
            if (!_loggedIn || !_staffMember)
            {
                Console.Error.WriteLine("WIP download functionality is only available to Redump moderators");
                return null;
            }

            string paddedId = id.ToString().PadLeft(6, '0');
            Console.WriteLine($"Processing WIP ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.NewDiscUrlTemplate, +id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
                    return null;
                }
                else if (discPage.Contains($"WIP disc with ID \"{id}\" doesn't exist"))
                {
                    Console.Error.WriteLine($"ID {paddedId} could not be found!");
                    return null;
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download an individual WIP ID data, if possible
        /// </summary>
        /// <param name="id">Redump WIP disc ID to retrieve</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="rename">True to rename deleted entries, false otherwise</param>
        /// <returns>True if all data was downloaded, false otherwise</returns>
        /// <remarks>Limited to moderators and staff</remarks>
        public async Task<bool> DownloadSingleWIPID(int id, string? outDir, bool rename)
        {
            // If the user is not a moderator
            if (!_loggedIn || !_staffMember)
            {
                Console.Error.WriteLine("WIP download functionality is only available to Redump moderators");
                return false;
            }

            // If no output directory is defined, use the current directory instead
            if (string.IsNullOrEmpty(outDir))
            {
                if (Debug) Console.WriteLine("DEBUG: Output directory was not provided, setting to current directory");
                outDir = Environment.CurrentDirectory;
            }

            string paddedId = id.ToString().PadLeft(6, '0');
            string paddedIdDir = Path.Combine(outDir, paddedId);
            Console.WriteLine($"Processing WIP ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.NewDiscUrlTemplate, +id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
                    return false;
                }
                else if (discPage.Contains($"WIP disc with ID \"{id}\" doesn't exist"))
                {
                    if (rename)
                    {
                        try
                        {
                            if (Directory.Exists(paddedIdDir) && rename)
                                Directory.Move(paddedIdDir, $"{paddedIdDir}-deleted");
                            else
                                Directory.CreateDirectory($"{paddedIdDir}-deleted");
                        }
                        catch { }
                    }

                    Console.Error.WriteLine($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (!Overwrite && File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Check for the full match ID in both pages
                    var oldResult = Constants.FullMatchRegex.Match(oldDiscPage);
                    var newResult = Constants.FullMatchRegex.Match(discPage);

                    // If both pages contain the same ID, skip it
                    if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }

                    // If neither page contains an ID, skip it
                    else if (!oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }

                    // Check the added date as a backup
                    oldResult = Constants.AddedRegex.Match(oldDiscPage);
                    newResult = Constants.AddedRegex.Match(discPage);

                    // If the downloaded data is invalid or otherwise empty, skip it
                    if (oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} retieved an empty page, skipping...");
                        return false;
                    }
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

                // HTML
                using (var discStreamWriter = File.CreateText(Path.Combine(paddedIdDir, "disc.html")))
                {
                    discStreamWriter.Write(discPage);
                    discStreamWriter.Flush();
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">Systems to download packs for</param>
        public async Task<Dictionary<RedumpSystem, byte[]>> DownloadPacks(PackType packType, RedumpSystem[] systems)
        {
            // Determine the base URL, if possible
            string? baseUrl = PackTypeToBaseUrl(packType);
            if (baseUrl is null)
            {
                if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                return [];
            }

            var packsDictionary = new Dictionary<RedumpSystem, byte[]>();
            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (!system.IsAvailable())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not marked as available on Redump, skipping...");
                    continue;
                }

                // If we didn't have credentials
                if (!_loggedIn && system.IsBanned())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} requires a user login to access, skipping...");
                    continue;
                }

                // If the system is unknown, we can't do anything
                string? longName = system.LongName();
                if (string.IsNullOrEmpty(longName))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not a recognized system, skipping...");
                    continue;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {packType} is not available for {system}, skipping...");
                    continue;
                }

                if (Debug)
                    Console.WriteLine(longName);
                else
                    Console.Write($"\r{longName}{new string(' ', Console.BufferWidth - longName!.Length - 1)}");

                byte[]? pack = await DownloadSinglePack(packType, system);
                if (pack is not null)
                    packsDictionary.Add(system, pack);
            }

            if (Debug)
                Console.WriteLine("Complete!");
            else
                Console.Write($"\rComplete!{new string(' ', Console.BufferWidth - 10)}");

            Console.WriteLine();

            return packsDictionary;
        }

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        public async Task<bool> DownloadPacks(PackType packType, RedumpSystem[] systems, string? outDir)
        {
            // Determine the base URL, if possible
            string? baseUrl = PackTypeToBaseUrl(packType);
            if (baseUrl is null)
            {
                if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                return false;
            }

            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (!system.IsAvailable())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not marked as available on Redump, skipping...");
                    continue;
                }

                // If we didn't have credentials
                if (!_loggedIn && system.IsBanned())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} requires a user login to access, skipping...");
                    continue;
                }

                // If the system is unknown, we can't do anything
                string? longName = system.LongName();
                if (string.IsNullOrEmpty(longName))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not a recognized system, skipping...");
                    continue;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {packType} is not available for {system}, skipping...");
                    continue;
                }

                if (Debug)
                    Console.WriteLine(longName);
                else
                    Console.Write($"\r{longName}{new string(' ', Console.BufferWidth - longName!.Length - 1)}");

                await DownloadSinglePack(packType, system, outDir);
            }

            if (Debug)
                Console.WriteLine("Complete!");
            else
                Console.Write($"\rComplete!{new string(' ', Console.BufferWidth - 10)}");

            Console.WriteLine();
            return true;
        }

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadPacks(PackType packType, RedumpSystem[] systems, string? outDir, string? subfolder)
        {
            // Determine the base URL, if possible
            string? baseUrl = PackTypeToBaseUrl(packType);
            if (baseUrl is null)
            {
                if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                return false;
            }

            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (!system.IsAvailable())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not marked as available on Redump, skipping...");
                    continue;
                }

                // If we didn't have credentials
                if (!_loggedIn && system.IsBanned())
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} requires a user login to access, skipping...");
                    continue;
                }

                // If the system is unknown, we can't do anything
                string? longName = system.LongName();
                if (string.IsNullOrEmpty(longName))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not a recognized system, skipping...");
                    continue;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {packType} is not available for {system}, skipping...");
                    continue;
                }

                if (Debug)
                    Console.WriteLine(longName);
                else
                    Console.Write($"\r{longName}{new string(' ', Console.BufferWidth - longName!.Length - 1)}");

                await DownloadSinglePack(packType, system, outDir, subfolder);
            }

            if (Debug)
                Console.WriteLine("Complete!");
            else
                Console.Write($"\rComplete!{new string(' ', Console.BufferWidth - 10)}");

            Console.WriteLine();
            return true;
        }

        /// <summary>
        /// Move a tempfile to a new name unless it aleady exists, in which case, delete the tempfile
        /// </summary>
        /// <param name="tempfile">Path to existing temporary file</param>
        /// <param name="newfile">Path to new output file</param>
        /// <param name="outDir">Output directory to save data to</param>
        private static void MoveOrDelete(string tempfile, string? newfile, string outDir)
        {
            // If we don't have a file to move to, just delete the temp file
            if (string.IsNullOrEmpty(newfile))
            {
                File.Delete(tempfile);
                return;
            }

            // If the file already exists, don't overwrite it
            if (File.Exists(Path.Combine(outDir, newfile)))
                File.Delete(tempfile);
            else
                File.Move(tempfile, Path.Combine(outDir, newfile));
        }

        /// <summary>
        /// Move a tempfile to a new name unless it aleady exists, in which case, delete the tempfile
        /// </summary>
        /// <param name="tempfile">Path to existing temporary file</param>
        /// <param name="newfile">Path to new output file</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Optional subfolder to append to the path</param>
        private static void MoveOrDelete(string tempfile, string? newfile, string outDir, string? subfolder)
        {
            // If we don't have a file to move to, just delete the temp file
            if (string.IsNullOrEmpty(newfile))
            {
                File.Delete(tempfile);
                return;
            }

            // If we have a subfolder, create it and update the newfile name
            if (!string.IsNullOrEmpty(subfolder))
            {
                if (!Directory.Exists(Path.Combine(outDir, subfolder)))
                    Directory.CreateDirectory(Path.Combine(outDir, subfolder));

                newfile = Path.Combine(subfolder, newfile);
            }

            // If the file already exists, don't overwrite it
            if (File.Exists(Path.Combine(outDir, newfile)))
                File.Delete(tempfile);
            else
                File.Move(tempfile, Path.Combine(outDir, newfile));
        }

        /// <summary>
        /// Convert the pack type to a base URL
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <returns>Base URL for downloading a pack type, null on error</returns>
        private static string? PackTypeToBaseUrl(PackType packType)
        {
            return packType switch
            {
                PackType.Cuesheets => Constants.PackCuesUrl,
                PackType.Datfile => Constants.PackDatfileUrl,
                PackType.DecryptedKeys => Constants.PackDkeysUrl,
                PackType.Gdis => Constants.PackGdiUrl,
                PackType.Keys => Constants.PackKeysUrl,
                PackType.Lsds => Constants.PackLsdUrl,
                PackType.Sbis => Constants.PackSbiUrl,
                _ => null,
            };
        }

        /// <summary>
        /// Determine if a pack is available for a given system
        /// </summary>
        /// <param name="packType">Pack type to use to determine the support status</param>
        /// <param name="system">Systems to determine pack availability for</param>
        /// <returns>True if the pack is available for a system, false otherwise</returns>
        private static bool PackTypeToAvailable(PackType packType, RedumpSystem system)
        {
            return packType switch
            {
                PackType.Cuesheets => system.HasCues(),
                PackType.Datfile => system.HasDat(),
                PackType.DecryptedKeys => system.HasDkeys(),
                PackType.Gdis => system.HasGdi(),
                PackType.Keys => system.HasKeys(),
                PackType.Lsds => system.HasLsd(),
                PackType.Sbis => system.HasSbi(),
                _ => false,
            };
        }

        #endregion
    }
}
