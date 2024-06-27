using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
#if NETCOREAPP
using System.Net.Http;
using System.Net.Http.Headers;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
#if NETCOREAPP
using System.Threading.Tasks;
#endif
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    public class RedumpClient
    {
        #region Properties

        /// <summary>
        /// Determines if user is logged into Redump
        /// </summary>
        public bool LoggedIn { get; private set; } = false;

        /// <summary>
        /// Determines if the user is a staff member
        /// </summary>
        public bool IsStaff { get; private set; } = false;

        /// <summary>
        /// Maximum retry count for any operation
        /// </summary>
        public int RetryCount { get; private set; } = 3;

        /// <summary>
        /// Internal client for interaction
        /// </summary>
#if NETFRAMEWORK
        private CookieWebClient _internalClient;
#else
        private HttpClient _internalClient;
#endif

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RedumpClient()
        {
#if NETFRAMEWORK
            _internalClient = new CookieWebClient();
#else
            _internalClient = new HttpClient(new HttpClientHandler { UseCookies = true });
#endif
        }

        #region Credentials

        /// <summary>
        /// Validate supplied credentials
        /// </summary>
#if NETFRAMEWORK
        public static (bool?, string?) ValidateCredentials(string username, string password)
#else
        public async static Task<(bool?, string?)> ValidateCredentials(string username, string password)
#endif
        {
            // If options are invalid or we're missing something key, just return
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return (false, null);

            // Try logging in with the supplied credentials otherwise
            var redumpClient = new RedumpClient();

#if NETFRAMEWORK
            bool? loggedIn = redumpClient.Login(username, password);
#else
            bool? loggedIn = await redumpClient.Login(username, password);
#endif
            if (loggedIn == true)
                return (true, "Redump username and password accepted!");
            else if (loggedIn == false)
                return (false, "Redump username and password denied!");
            else
                return (null, "An error occurred validating your credentials!");
        }

        /// <summary>
        /// Login to Redump, if possible
        /// </summary>
        /// <param name="username">Redump username</param>
        /// <param name="password">Redump password</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
#if NETFRAMEWORK
        public bool? Login(string username, string password)
#else
        public async Task<bool?> Login(string username, string password)
#endif
        {
            // Credentials verification
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Credentials entered, will attempt Redump login...");
            }
            else if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Only a username was specified, will not attempt Redump login...");
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

            // Attempt to login up to 3 times
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    // Get the current token from the login page
#if NETFRAMEWORK
                    var loginPage = DownloadStringWithRetries(Constants.LoginUrl);
#else
                    var loginPage = await DownloadStringWithRetries(Constants.LoginUrl);
#endif
                    string token = Constants.TokenRegex.Match(loginPage ?? string.Empty).Groups[1].Value;

#if NETFRAMEWORK
                    // Construct the login request
                    _internalClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    _internalClient.Encoding = Encoding.UTF8;

                    // Send the login request and get the result
                    string? responseContent = _internalClient.UploadString(Constants.LoginUrl, $"form_sent=1&redirect_url=&csrf_token={token}&req_username={username}&req_password={password}&save_pass=0");
#else
                    // Construct the login request
                    var postContent = new StringContent($"form_sent=1&redirect_url=&csrf_token={token}&req_username={username}&req_password={password}&save_pass=0", Encoding.UTF8);
                    postContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    // Send the login request and get the result
                    var response = await _internalClient.PostAsync(Constants.LoginUrl, postContent);
                    string? responseContent = null;
                    if (response?.Content != null)
                        responseContent = await response.Content.ReadAsStringAsync();
#endif

                    // An empty response indicates an error
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        Console.WriteLine($"An error occurred while trying to log in on attempt {i}: No response");
                        continue;
                    }

                    // Explcit confirmation the login was wrong
                    if (responseContent.Contains("Incorrect username and/or password."))
                    {
                        Console.WriteLine("Invalid credentials entered, continuing without logging in...");
                        return false;
                    }

                    // The user was able to be logged in
                    Console.WriteLine("Credentials accepted! Logged into Redump...");
                    LoggedIn = true;

                    // If the user is a moderator or staff, set accordingly
                    if (responseContent.Contains("http://forum.redump.org/forum/9/staff/"))
                        IsStaff = true;

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred while trying to log in on attempt {i}: {ex}");
                }
            }

            Console.WriteLine("Could not login to Redump in 3 attempts, continuing without logging in...");
            return false;
        }

        #endregion

        #region Single Page Helpers

        /// <summary>
        /// Process a Redump site page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <returns>List of IDs from the page, empty on error</returns>
#if NETFRAMEWORK
        public List<int> CheckSingleSitePage(string url)
#else
        public async Task<List<int>> CheckSingleSitePage(string url)
#endif
        {
            List<int> ids = [];

            // Try to retrieve the data
#if NETFRAMEWORK
            string? dumpsPage = DownloadStringWithRetries(url);
#else
            string? dumpsPage = await DownloadStringWithRetries(url);
#endif

            // If we have no dumps left
            if (dumpsPage == null || dumpsPage.Contains("No discs found."))
                return ids;

            // If we have a single disc page already
            if (dumpsPage.Contains("<b>Download:</b>"))
            {
                var value = Regex.Match(dumpsPage, @"/disc/(\d+)/sfv/").Groups[1].Value;
                if (int.TryParse(value, out int id))
                    ids.Add(id);

                return ids;
            }

            // Otherwise, traverse each dump on the page
            var matches = Constants.DiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches.Cast<Match?>())
            {
                if (match == null)
                    continue;

                try
                {
                    if (int.TryParse(match.Groups[1].Value, out int value))
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
        /// Process a Redump site page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="failOnSingle">True to return on first error, false otherwise</param>
        /// <returns>True if the page could be downloaded, false otherwise</returns>
#if NETFRAMEWORK
        public bool CheckSingleSitePage(string url, string? outDir, bool failOnSingle)
#else
        public async Task<bool> CheckSingleSitePage(string url, string? outDir, bool failOnSingle)
#endif
        {
            // Try to retrieve the data
#if NETFRAMEWORK
            string? dumpsPage = DownloadStringWithRetries(url);
#else
            string? dumpsPage = await DownloadStringWithRetries(url);
#endif

            // If we have no dumps left
            if (dumpsPage == null || dumpsPage.Contains("No discs found."))
                return false;

            // If we have a single disc page already
            if (dumpsPage.Contains("<b>Download:</b>"))
            {
                var value = Regex.Match(dumpsPage, @"/disc/(\d+)/sfv/").Groups[1].Value;
                if (int.TryParse(value, out int id))
                {
#if NETFRAMEWORK
                    bool downloaded = DownloadSingleSiteID(id, outDir, false);
#else
                    bool downloaded = await DownloadSingleSiteID(id, outDir, false);
#endif
                    if (!downloaded && failOnSingle)
                        return false;
                }

                return false;
            }

            // Otherwise, traverse each dump on the page
            var matches = Constants.DiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches.Cast<Match?>())
            {
                if (match == null)
                    continue;

                try
                {
                    if (int.TryParse(match.Groups[1].Value, out int value))
                    {
#if NETFRAMEWORK
                        bool downloaded = DownloadSingleSiteID(value, outDir, false);
#else
                        bool downloaded = await DownloadSingleSiteID(value, outDir, false);
#endif
                        if (!downloaded && failOnSingle)
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return true;
        }

        /// <summary>
        /// Process a Redump WIP page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="wc">RedumpWebClient to access the packs</param>
        /// <returns>List of IDs from the page, empty on error</returns>
#if NETFRAMEWORK
        public List<int> CheckSingleWIPPage(string url)
#else
        public async Task<List<int>> CheckSingleWIPPage(string url)
#endif
        {
            List<int> ids = [];

            // Try to retrieve the data
#if NETFRAMEWORK
            string? dumpsPage = DownloadStringWithRetries(url);
#else
            string? dumpsPage = await DownloadStringWithRetries(url);
#endif

            // If we have no dumps left
            if (dumpsPage == null || dumpsPage.Contains("No discs found."))
                return ids;

            // Otherwise, traverse each dump on the page
            var matches = Constants.NewDiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches.Cast<Match?>())
            {
                if (match == null)
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
        /// <param name="wc">RedumpWebClient to access the packs</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="failOnSingle">True to return on first error, false otherwise</param>
        /// <returns>True if the page could be downloaded, false otherwise</returns>
#if NETFRAMEWORK
        public bool CheckSingleWIPPage(string url, string? outDir, bool failOnSingle)
#else
        public async Task<bool> CheckSingleWIPPage(string url, string? outDir, bool failOnSingle)
#endif
        {
            // Try to retrieve the data
#if NETFRAMEWORK
            string? dumpsPage = DownloadStringWithRetries(url);
#else
            string? dumpsPage = await DownloadStringWithRetries(url);
#endif

            // If we have no dumps left
            if (dumpsPage == null || dumpsPage.Contains("No discs found."))
                return false;

            // Otherwise, traverse each dump on the page
            var matches = Constants.NewDiscRegex.Matches(dumpsPage);
            foreach (Match? match in matches.Cast<Match?>())
            {
                if (match == null)
                    continue;

                try
                {
                    if (int.TryParse(match.Groups[2].Value, out int value))
                    {
#if NETFRAMEWORK
                        bool downloaded = DownloadSingleWIPID(value, outDir, false);
#else
                        bool downloaded = await DownloadSingleWIPID(value, outDir, false);
#endif
                        if (!downloaded && failOnSingle)
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return true;
        }

        #endregion

        #region Download Helpers

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <param name="system">System to download packs for</param>
        /// <returns>Byte array containing the downloaded pack, null on error</returns>
#if NETFRAMEWORK
        public byte[]? DownloadSinglePack(string url, RedumpSystem? system)
#else
        public async Task<byte[]?> DownloadSinglePack(string url, RedumpSystem? system)
#endif
        {
            try
            {
#if NETFRAMEWORK
                return _internalClient.DownloadData(string.Format(url, system.ShortName()));
#else
                return await _internalClient.GetByteArrayAsync(string.Format(url, system.ShortName()));
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <param name="system">System to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
#if NETFRAMEWORK
        public bool DownloadSinglePack(string url, RedumpSystem? system, string? outDir, string? subfolder)
#else
        public async Task<bool> DownloadSinglePack(string url, RedumpSystem? system, string? outDir, string? subfolder)
#endif
        {
            try
            {
                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                    outDir = Environment.CurrentDirectory;

                string tempfile = Path.Combine(outDir, "tmp" + Guid.NewGuid().ToString());
                string packUri = string.Format(url, system.ShortName());

                // Make the call to get the pack
#if NETFRAMEWORK
                _internalClient.DownloadFile(packUri, tempfile);
                MoveOrDelete(tempfile, _internalClient.GetLastFilename(), outDir!, subfolder);
#else
                string? remoteFileName = await DownloadFile(packUri, tempfile);
                MoveOrDelete(tempfile, remoteFileName, outDir!, subfolder);
#endif
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual site ID data, if possible
        /// </summary>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
#if NETFRAMEWORK
        public string? DownloadSingleSiteID(int id)
#else
        public async Task<string?> DownloadSingleSiteID(int id)
#endif
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.DiscPageUrl, +id);
#if NETFRAMEWORK
                string? discPage = DownloadStringWithRetries(discPageUri);
#else
                string? discPage = await DownloadStringWithRetries(discPageUri);
#endif

                if (discPage == null || discPage.Contains($"Disc with ID \"{id}\" doesn't exist"))
                {
                    Console.WriteLine($"ID {paddedId} could not be found!");
                    return null;
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
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
#if NETFRAMEWORK
        public bool DownloadSingleSiteID(int id, string? outDir, bool rename)
#else
        public async Task<bool> DownloadSingleSiteID(int id, string? outDir, bool rename)
#endif
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
                string discPageUri = string.Format(Constants.DiscPageUrl, +id);
#if NETFRAMEWORK
                string? discPage = DownloadStringWithRetries(discPageUri);
#else
                string? discPage = await DownloadStringWithRetries(discPageUri);
#endif

                if (discPage == null || discPage.Contains($"Disc with ID \"{id}\" doesn't exist"))
                {
                    try
                    {
                        if (rename)
                        {
                            if (Directory.Exists(paddedIdDir) && rename)
                                Directory.Move(paddedIdDir, paddedIdDir + "-deleted");
                            else
                                Directory.CreateDirectory(paddedIdDir + "-deleted");
                        }
                    }
                    catch { }

                    Console.WriteLine($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Check for the last modified date in both pages
                    var oldResult = Constants.LastModifiedRegex.Match(oldDiscPage);
                    var newResult = Constants.LastModifiedRegex.Match(discPage);

                    // If both pages contain the same modified date, skip it
                    if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download");
                        return false;
                    }

                    // If neither page contains a modified date, skip it
                    else if (!oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download");
                        return false;
                    }
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

#if NETFRAMEWORK
                // View Edit History
                if (discPage.Contains($"<a href=\"/disc/{id}/changes/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.ChangesExt, Path.Combine(paddedIdDir, "changes.html"));

                // CUE
                if (discPage.Contains($"<a href=\"/disc/{id}/cue/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.CueExt, Path.Combine(paddedIdDir, paddedId + ".cue"));

                // Edit disc
                if (discPage.Contains($"<a href=\"/disc/{id}/edit/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.EditExt, Path.Combine(paddedIdDir, "edit.html"));

                // GDI
                if (discPage.Contains($"<a href=\"/disc/{id}/gdi/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.GdiExt, Path.Combine(paddedIdDir, paddedId + ".gdi"));

                // KEYS
                if (discPage.Contains($"<a href=\"/disc/{id}/key/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.KeyExt, Path.Combine(paddedIdDir, paddedId + ".key"));

                // LSD
                if (discPage.Contains($"<a href=\"/disc/{id}/lsd/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.LsdExt, Path.Combine(paddedIdDir, paddedId + ".lsd"));

                // MD5
                if (discPage.Contains($"<a href=\"/disc/{id}/md5/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.Md5Ext, Path.Combine(paddedIdDir, paddedId + ".md5"));

                // Review WIP entry
                if (Constants.NewDiscRegex.IsMatch(discPage))
                {
                    var match = Constants.NewDiscRegex.Match(discPage);
                    _internalClient.DownloadFile(string.Format(Constants.WipDiscPageUrl, match.Groups[2].Value), Path.Combine(paddedIdDir, "newdisc.html"));
                }

                // SBI
                if (discPage.Contains($"<a href=\"/disc/{id}/sbi/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.SbiExt, Path.Combine(paddedIdDir, paddedId + ".sbi"));

                // SFV
                if (discPage.Contains($"<a href=\"/disc/{id}/sfv/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.SfvExt, Path.Combine(paddedIdDir, paddedId + ".sfv"));

                // SHA1
                if (discPage.Contains($"<a href=\"/disc/{id}/sha1/\""))
                    _internalClient.DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.Sha1Ext, Path.Combine(paddedIdDir, paddedId + ".sha1"));
#else
                // View Edit History
                if (discPage.Contains($"<a href=\"/disc/{id}/changes/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.ChangesExt, Path.Combine(paddedIdDir, "changes.html"));

                // CUE
                if (discPage.Contains($"<a href=\"/disc/{id}/cue/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.CueExt, Path.Combine(paddedIdDir, paddedId + ".cue"));

                // Edit disc
                if (discPage.Contains($"<a href=\"/disc/{id}/edit/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.EditExt, Path.Combine(paddedIdDir, "edit.html"));

                // GDI
                if (discPage.Contains($"<a href=\"/disc/{id}/gdi/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.GdiExt, Path.Combine(paddedIdDir, paddedId + ".gdi"));

                // KEYS
                if (discPage.Contains($"<a href=\"/disc/{id}/key/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.KeyExt, Path.Combine(paddedIdDir, paddedId + ".key"));

                // LSD
                if (discPage.Contains($"<a href=\"/disc/{id}/lsd/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.LsdExt, Path.Combine(paddedIdDir, paddedId + ".lsd"));

                // MD5
                if (discPage.Contains($"<a href=\"/disc/{id}/md5/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.Md5Ext, Path.Combine(paddedIdDir, paddedId + ".md5"));

                // Review WIP entry
                if (Constants.NewDiscRegex.IsMatch(discPage))
                {
                    var match = Constants.NewDiscRegex.Match(discPage);
                    _ = await DownloadFile(string.Format(Constants.WipDiscPageUrl, match.Groups[2].Value), Path.Combine(paddedIdDir, "newdisc.html"));
                }

                // SBI
                if (discPage.Contains($"<a href=\"/disc/{id}/sbi/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.SbiExt, Path.Combine(paddedIdDir, paddedId + ".sbi"));

                // SFV
                if (discPage.Contains($"<a href=\"/disc/{id}/sfv/\""))
                    await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.SfvExt, Path.Combine(paddedIdDir, paddedId + ".sfv"));

                // SHA1
                if (discPage.Contains($"<a href=\"/disc/{id}/sha1/\""))
                    _ = await DownloadFile(string.Format(Constants.DiscPageUrl, +id) + Constants.Sha1Ext, Path.Combine(paddedIdDir, paddedId + ".sha1"));
#endif

                // HTML (Last in case of errors)
                using (var discStreamWriter = File.CreateText(Path.Combine(paddedIdDir, "disc.html")))
                {
                    discStreamWriter.Write(discPage);
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual WIP ID data, if possible
        /// </summary>
        /// <param name="id">Redump WIP disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
#if NETFRAMEWORK
        public string? DownloadSingleWIPID(int id)
#else
        public async Task<string?> DownloadSingleWIPID(int id)
#endif
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = string.Format(Constants.WipDiscPageUrl, +id);
#if NETFRAMEWORK
                string? discPage = DownloadStringWithRetries(discPageUri);
#else
                string? discPage = await DownloadStringWithRetries(discPageUri);
#endif

                if (discPage == null || discPage.Contains($"WIP disc with ID \"{id}\" doesn't exist"))
                {
                    Console.WriteLine($"ID {paddedId} could not be found!");
                    return null;
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
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
#if NETFRAMEWORK
        public bool DownloadSingleWIPID(int id, string? outDir, bool rename)
#else
        public async Task<bool> DownloadSingleWIPID(int id, string? outDir, bool rename)
#endif
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
                string discPageUri = string.Format(Constants.WipDiscPageUrl, +id);
#if NETFRAMEWORK
                string? discPage = DownloadStringWithRetries(discPageUri);
#else
                string? discPage = await DownloadStringWithRetries(discPageUri);
#endif

                if (discPage == null || discPage.Contains($"WIP disc with ID \"{id}\" doesn't exist"))
                {
                    try
                    {
                        if (rename)
                        {
                            if (Directory.Exists(paddedIdDir) && rename)
                                Directory.Move(paddedIdDir, paddedIdDir + "-deleted");
                            else
                                Directory.CreateDirectory(paddedIdDir + "-deleted");
                        }
                    }
                    catch { }

                    Console.WriteLine($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Check for the full match ID in both pages
                    var oldResult = Constants.FullMatchRegex.Match(oldDiscPage);
                    var newResult = Constants.FullMatchRegex.Match(discPage);

                    // If both pages contain the same ID, skip it
                    if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download");
                        return false;
                    }

                    // If neither page contains an ID, skip it
                    else if (!oldResult.Success && !newResult.Success)
                    {
                        Console.WriteLine($"ID {paddedId} has not been changed since last download");
                        return false;
                    }
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

                // HTML
                using (var discStreamWriter = File.CreateText(Path.Combine(paddedIdDir, "disc.html")))
                {
                    discStreamWriter.Write(discPage);
                }

                Console.WriteLine($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
                return false;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="url">Base URL to download using</param>
        /// <param name="system">Systems to download packs for</param>
        /// <param name="title">Name of the pack that is downloading</param>
#if NETFRAMEWORK
        public Dictionary<RedumpSystem, byte[]> DownloadPacks(string url, RedumpSystem?[] systems, string title)
#else
        public async Task<Dictionary<RedumpSystem, byte[]>> DownloadPacks(string url, RedumpSystem?[] systems, string title)
#endif
        {
            var packsDictionary = new Dictionary<RedumpSystem, byte[]>();

            Console.WriteLine($"Downloading {title}");
            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (system == null || !system.IsAvailable())
                    continue;

                // If we didn't have credentials
                if (!LoggedIn && system.IsBanned())
                    continue;

                // If the system is unknown, we can't do anything
                string? longName = system.LongName();
                if (string.IsNullOrEmpty(longName))
                    continue;

                Console.Write($"\r{longName}{new string(' ', Console.BufferWidth - longName!.Length - 1)}");
#if NETFRAMEWORK
                byte[]? pack = DownloadSinglePack(url, system);
#else
                byte[]? pack = await DownloadSinglePack(url, system);
#endif
                if (pack != null)
                    packsDictionary.Add(system.Value, pack);
            }

            Console.Write($"\rComplete!{new string(' ', Console.BufferWidth - 10)}");
            Console.WriteLine();

            return packsDictionary;
        }

        /// <summary>
        /// Download a set of packs
        /// </summary> 
        /// <param name="url">Base URL to download using</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="title">Name of the pack that is downloading</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
#if NETFRAMEWORK
        public bool DownloadPacks(string url, RedumpSystem?[] systems, string title, string? outDir, string? subfolder)
#else
        public async Task<bool> DownloadPacks(string url, RedumpSystem?[] systems, string title, string? outDir, string? subfolder)
#endif
        {
            Console.WriteLine($"Downloading {title}");
            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (system == null || !system.IsAvailable())
                    continue;

                // If we didn't have credentials
                if (!LoggedIn && system.IsBanned())
                    continue;

                // If the system is unknown, we can't do anything
                string? longName = system.LongName();
                if (string.IsNullOrEmpty(longName))
                    continue;

                Console.Write($"\r{longName}{new string(' ', Console.BufferWidth - longName!.Length - 1)}");
#if NETFRAMEWORK
                DownloadSinglePack(url, system, outDir, subfolder);
#else
                await DownloadSinglePack(url, system, outDir, subfolder);
#endif
            }

            Console.Write($"\rComplete!{new string(' ', Console.BufferWidth - 10)}");
            Console.WriteLine();
            return true;
        }

#if NETCOREAPP
        /// <summary>
        /// Download from a URI to a local file
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <param name="fileName">Filename to write to</param>
        /// <returns>The remote filename from the URI, null on error</returns>
        private async Task<string?> DownloadFile(string uri, string fileName)
        {
            // Make the call to get the file
            var response = await _internalClient.GetAsync(uri);
            if (response?.Content?.Headers == null || !response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Could not download {uri}");
                return null;
            }

            // Copy the data to a local temp file
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var tempFileStream = File.OpenWrite(fileName))
            {
                responseStream.CopyTo(tempFileStream);
            }

            return response.Content.Headers.ContentDisposition?.FileName?.Replace("\"", "");
        }
#endif

        /// <summary>
        /// Download from a URI to a string
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <returns>String from the URI, null on error</returns>
#if NETFRAMEWORK
        private string? DownloadStringWithRetries(string uri)
#else
        private async Task<string?> DownloadStringWithRetries(string uri)
#endif
        {
            // Only retry a positive number of times
            if (RetryCount <= 0)
                return null;

            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
#if NETFRAMEWORK
                    return _internalClient.DownloadString(uri);
#else
                    return await _internalClient.GetStringAsync(uri);
#endif
                }
                catch { }

                // Sleep for 100ms if the last attempt failed
                Thread.Sleep(100);
            }

            return null;
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

        #endregion
    }
}