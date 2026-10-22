using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
#if NETCOREAPP
using System.Net.Http;
#else
using System.Text;
#endif
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
    public class Client
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
        /// Optional progress callback for logging
        /// </summary>
        private readonly IProgress<WebClientEventArgs> _progress;

        /// <summary>
        /// Cookie container for persistence
        /// </summary>
        private readonly CookieContainer _cookieContainer;

        /// <summary>
        /// Indicates if user is logged into Redump
        /// </summary>
        /// <remarks>Modifying to set as true does not change actual logged-in status</remarks>
        private bool _loggedIn = false;

        /// <summary>
        /// Indicates if the user is a staff member
        /// </summary>
        /// <remarks>Modifying to set as true does not change actual staff status</remarks>
#pragma warning disable CS0414 // The private field 'field' is assigned but its value is never used
#pragma warning disable IDE0052 // Remove unread private members
        private bool _staffMember = false;
#pragma warning restore CS0414 // The private field 'field' is assigned but its value is never used
#pragma warning restore IDE0052 // Remove unread private members

        #endregion

        #region Constants

        /// <summary>
        /// Login page URL
        /// </summary>
        public const string LoginUrl = "https://forum.redump.info/ucp.php?mode=login&redirect=index.php";

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="progressHandler">Optional progress callback for logging</param>
        public Client(EventHandler<WebClientEventArgs>? progressHandler = null)
        {
            _cookieContainer = new CookieContainer();
#if NETCOREAPP
            var clientHandler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true,
            };
            _internalClient = new HttpClient(clientHandler);
#else
            _internalClient = new CookieWebClient(_cookieContainer);
#endif

            Timeout = TimeSpan.FromSeconds(30);

            _progress = new Progress<WebClientEventArgs>();
            if (progressHandler is not null)
                ((Progress<WebClientEventArgs>)_progress).ProgressChanged += progressHandler;
        }

        #region Cookies

        /// <summary>
        /// Export cookies to JSON format
        /// </summary>
        /// <returns>Cookies in JSON format on success, null on error</returns>
        public string? ExportCookies()
        {
            try
            {
                var cookies = _cookieContainer.GetCookies(new Uri("https://forum.redump.info"));
                return JsonConvert.SerializeObject(cookies);
            }
            catch
            {
                // TODO: Report this error somehow
                return null;
            }
        }

        /// <summary>
        /// Save cookies to an external file
        /// </summary>
        /// <param name="file">File path to save cookies to</param>
        /// <returns>True if the file could be saved, false otherwise</returns>
        public bool SaveCookies(string file)
        {
            try
            {
                // Create the output directory
                string? directory = Path.GetDirectoryName(file);
                if (directory is not null)
                    Directory.CreateDirectory(directory);

                // Serialize the cookies and write
                string? cookiesJson = ExportCookies();
                if (cookiesJson is null)
                    return false;

                File.WriteAllText(file, cookiesJson);

                return true;
            }
            catch
            {
                // TODO: Report this error somehow
                return false;
            }
        }

        /// <summary>
        /// Import cookies from JSON format
        /// </summary>
        /// <returns>True if the JSON could be loaded, false otherwise</returns>
        public bool ImportCookies(string json)
        {
            try
            {
                // Deserialize the cookies to a cookie collection
                var cookies = JsonConvert.DeserializeObject<CookieCollection>(json);
                if (cookies is null)
                    return false;

                // Write the cookies to the container
                _cookieContainer.Add(new Uri("https://forum.redump.info"), cookies);

                // Assume that this indicates a valid login
                _loggedIn = true;
                return true;
            }
            catch
            {
                // TODO: Report this error somehow
                return false;
            }
        }

        /// <summary>
        /// Load cookies from an external file
        /// </summary>
        /// <param name="file">File path to load cookies from</param>
        /// <returns>True if the file could be loaded, false otherwise</returns>
        public bool LoadCookies(string file)
        {
            try
            {
                // If the file does not exist
                if (!File.Exists(file))
                    return false;

                // Read the cookies from the file
                string cookiesJson = File.ReadAllText(file);
                return ImportCookies(cookiesJson);
            }
            catch
            {
                // TODO: Report this error somehow
                return false;
            }
        }

        #endregion

        #region Credentials

        /// <summary>
        /// Validate supplied credentials
        /// </summary>
        /// <param name="username">redump.info username</param>
        /// <param name="password">redump.info password</param>
        /// <param name="progressHandler">Optional progress callback for logging</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
        public static async Task<bool?> ValidateCredentials(string? username,
            string? password,
            EventHandler<WebClientEventArgs>? progressHandler = null)
        {
            // If options are invalid or we're missing something key, just return
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            // Try logging in with the supplied credentials otherwise
            var redumpClient = new Client(progressHandler);
            return await redumpClient.Login(username, password);
        }

        /// <summary>
        /// Login to edump.info, if possible
        /// </summary>
        /// <param name="username">redump.info username</param>
        /// <param name="password">redump.info password</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
        public async Task<bool?> Login(string? username, string? password)
        {
            // Check for already logged in
            if (_loggedIn)
            {
                ReportGeneric("Already logged in!");
                return true;
            }

            // Credentials verification
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                ReportGeneric("Credentials entered, will attempt redump.info login...");
            }
            else if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                ReportError("Only a username was specified, will not attempt redump.info login...");
                return false;
            }
            else if (string.IsNullOrEmpty(username))
            {
                ReportGeneric("No credentials entered, will not attempt redump.info login...");
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
                    ReportGeneric($"Login attempt {i + 1} of {AttemptCount}");

                    // Get the current information from the login page
                    var loginPage = await DownloadString(LoginUrl);
                    string creationTime = Constants.LoginCreationTimeRegex.Match(loginPage ?? string.Empty).Groups[1].Value;
                    string formToken = Constants.FormTokenRegex.Match(loginPage ?? string.Empty).Groups[1].Value;
                    string sessionId = Constants.SessionIDRegex.Match(loginPage ?? string.Empty).Groups[1].Value;

#if NETCOREAPP
                    // Construct the login request
#if NET5_0
                    var postContent = new FormUrlEncodedContent(new List<KeyValuePair<string?, string?>>
#else
                    var postContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
#endif
                    {
                        new("username", username),
                        new("password", password!),
                        // new("autologin", null), // Ignore parameter as it is unneeded
                        // new("viewonline", null), // Ignore parameter as it is unneeded
                        new("redirect", "./ucp.php?mode=login&amp;redirect=index.php"),
                        new("creation_time", creationTime),
                        new("form_token", formToken),
                        new("sid", sessionId),
                        new("redirect", "index.php"),
                        new("login", "Login"),
                    });

                    // Send the login request and get the result
                    var response = await _internalClient.PostAsync(LoginUrl, postContent);
                    string? responseContent = await response.Content.ReadAsStringAsync();
#else
                    // Generate the fields that are needed
                    var postFields = new StringBuilder();
                    postFields.Append($"username={username}");
                    postFields.Append($"&password={password}");
                    // postFields.Append("&autologin="); // Ignore parameter as it is unneeded
                    // postFields.Append("&viewonline="); // Ignore parameter as it is unneeded
                    postFields.Append("&redirect=./ucp.php?mode=login&amp;redirect=index.php");
                    postFields.Append($"&creation_time={creationTime}");
                    postFields.Append($"&form_token={formToken}");
                    postFields.Append($"&sid={sessionId}");
                    postFields.Append("&redirect=index.php");
                    postFields.Append("&login=Login");

                    // Construct the login request
                    _internalClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    _internalClient.Encoding = Encoding.UTF8;

                    // Send the login request and get the result
                    string? responseContent = _internalClient.UploadString(LoginUrl, postFields.ToString());
#endif

                    // An empty response indicates an error
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        ReportError($"An error occurred while trying to log in on attempt {i + 1}: No response");
                        DelayHelper.DelayRandom();
                        continue;
                    }

                    // A malformed form indicicates a retry is needed
                    if (responseContent.Contains("The submitted form was invalid. Try submitting again."))
                    {
                        ReportError($"An error occurred while trying to log in on attempt {i + 1}: Invalid result");
                        DelayHelper.DelayRandom();
                        continue;
                    }

                    // "Your posts" indicates login was successful
                    if (!responseContent.Contains("./search.php?search_id=egosearch"))
                    {
                        ReportError("Invalid credentials entered, continuing without logging in...");
                        return false;
                    }

                    // The user was able to be logged in
                    ReportGeneric("Credentials accepted! Logged into Redump...");
                    _loggedIn = true;

                    // If the user is a moderator or staff, set accordingly
                    if (responseContent.Contains("https://forum.redump.info/viewforum.php?f=4"))
                        _staffMember = true;

                    return true;
                }
                catch (Exception ex)
                {
                    ReportError($"An exception occurred while trying to log in on attempt {i + 1}: {ex}");
                }
            }

            ReportError($"Could not login to Redump in {AttemptCount} attempts, continuing without logging in...");
            return false;
        }

        #endregion

        #region Generic Downloads

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
                ReportError("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) ReportDebug($"DownloadData(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
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

            ReportError($"Could not download \"{uri}\" after {AttemptCount} attempts");
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
                ReportError("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) ReportDebug($"DownloadFile(\"{uri}\", \"{fileName}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    // Make the call to get the file
                    var response = await _internalClient.GetAsync(uri);
                    if (response?.Content?.Headers is null || !response.IsSuccessStatusCode)
                    {
                        if (Debug) ReportDebug($"DownloadFile failed, continuing...");
                        continue;
                    }

                    // Copy the data to a local temp file
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    using (var tempFileStream = File.OpenWrite(fileName))
                    {
                        responseStream.CopyTo(tempFileStream);
                    }

                    // If the filename can be derived from the headers
                    string? filename = response.Content.Headers.ContentDisposition?.FileName?.Replace("\"", "");
                    if (filename is not null)
                        return filename;

                    // Otherwise, derive from the URL itself
                    Uri filenameUri = new Uri(uri);
                    return Path.GetFileName(filenameUri.LocalPath);
#elif NET40
                    await Task.Factory.StartNew(() => { _internalClient.DownloadFile(uri, fileName); return true; });
                    string? lastFilename = _internalClient.GetLastFilename();
                    if (lastFilename is null)
                    {
                        if (Debug) ReportDebug($"DownloadFile failed, continuing...");
                        continue;
                    }

                    return lastFilename;
#else
                    await Task.Run(() => _internalClient.DownloadFile(uri, fileName));
                    string? lastFilename = _internalClient.GetLastFilename();
                    if (lastFilename is null)
                    {
                        if (Debug) ReportDebug($"DownloadFile failed, continuing...");
                        continue;
                    }

                    return lastFilename;
#endif
                }
                catch { }

                // Intentional delay here so we don't flood the server
                DelayHelper.DelayRandom();
            }

            ReportError($"Could not download \"{uri}\" after {AttemptCount} attempts");
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
                ReportError("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) ReportDebug($"DownloadString(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
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

            ReportError($"Could not download \"{uri}\" after {AttemptCount} attempts");
            return null;
        }

        /// <summary>
        /// Get the resolved URL for a given URI
        /// </summary>
        /// <param name="uri">Remote URI to retrieve</param>
        /// <returns>Final URL, null on error</returns>
        public async Task<string?> GetResolvedURL(string uri)
        {
            // Only retry a positive number of times
            if (AttemptCount <= 0)
            {
                ReportError("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) ReportDebug($"GetResolvedURL(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    // Make the call to get the file
                    var response = await _internalClient.GetAsync(uri);
                    if (response?.Content?.Headers is null || !response.IsSuccessStatusCode)
                    {
                        if (Debug) ReportDebug($"GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return response.RequestMessage?.RequestUri?.AbsoluteUri;
#elif NET40
                    await Task.Factory.StartNew(() => { _internalClient.DownloadData(uri); return true; });
                    string? lastUrl = _internalClient.LastUrl?.AbsoluteUri;
                    if (lastUrl is null)
                    {
                        if (Debug) ReportDebug($"GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return lastUrl;
#else
                    await Task.Run(() => _internalClient.DownloadData(uri));
                    string? lastUrl = _internalClient.LastUrl?.AbsoluteUri;
                    if (lastUrl is null)
                    {
                        if (Debug) ReportDebug($"GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return lastUrl;
#endif
                }
                catch { }

                // Intentional delay here so we don't flood the server
                DelayHelper.DelayRandom();
            }

            ReportError($"Could not download \"{uri}\" after {AttemptCount} attempts");
            return null;
        }

        #endregion

        #region Single Endpoint

        /// <summary>
        /// Process a Redump discs endpoint as a list of possible IDs or disc page
        /// </summary>
        /// <param name="endpoint">Discs endpoint to process</param>
        /// <returns>List of IDs from the endpoint, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsEndpoint(Endpoints.Discs endpoint)
        {
            string url = endpoint.BuildUrl();

            List<int> ids = [];

            // Try to retrieve the data
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) ReportDebug($"CheckSingleDiscsPage(\"{url}\") - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) ReportDebug($"CheckSingleDiscsPage(\"{url}\") - No discs found");
                return ids;
            }

            // If we have a single disc page already
            if (dumpsPage.Contains("<h3>Disc</h3>"))
            {
                if (Debug) ReportDebug($"CheckSingleDiscsPage(\"{url}\") - Single disc page");
                string? lastUrl = await GetResolvedURL(url);
                if (lastUrl is not null)
                {
                    var value = Regex.Match(lastUrl, pattern: @"/disc/([0-9]+)").Groups[1].Value;
                    if (int.TryParse(value, out int id))
                        ids.Add(id);

                    return ids;
                }
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
                    ReportError($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return ids;
        }

        /// <summary>
        /// Process a Redump discs endpoint as a list of possible IDs or disc page
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="endpoint">Discs endpoint to process</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>List of IDs from the endpoint, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsEndpoint(
            string? outDir,
            Endpoints.Discs endpoint,
            DiscSubpath[]? discSubpaths = null)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleDiscsEndpoint(endpoint);
            if (ids is null)
            {
                if (Debug) ReportDebug($"CheckSingleDiscsPage(\"{outDir}\") - Client failure");
                return null;
            }

            // Try to download all IDs
            List<int> processed = [];
            foreach (int id in ids)
            {
                try
                {
                    bool downloaded = await DownloadSingleDiscPage(id, outDir, rename: false, discSubpaths);
                    if (!downloaded && !IgnoreErrors)
                        return processed;

                    processed.Add(id);
                }
                catch (Exception ex)
                {
                    ReportError($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return processed;
        }

        /// <summary>
        /// Process a Redump queue endpoint as a list of possible IDs or queue page
        /// </summary>
        /// <param name="endpoint">Queue endpoint to process</param>
        /// <returns>List of IDs from the endpoint, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleQueueEndpoint(Endpoints.Queue endpoint)
        {
            List<int> ids = [];

            // Try to retrieve the data
            string url = endpoint.BuildUrl();
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) ReportDebug($"CheckSingleQueuePage() - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) ReportDebug($"CheckSingleQueuePage() - No discs found");
                return ids;
            }

            // Otherwise, traverse each item in the queue on the page
            var matches = Constants.QueueDiscRegex.Matches(dumpsPage);
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
                    ReportGeneric($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return ids;
        }

        /// <summary>
        /// Process a Redump queue endpoint as a list of possible IDs or queue page
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="endpoint">Queue endpoint to process</param>
        /// <returns>List of IDs from the endpoint, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleQueueEndpoint(
            string? outDir,
            Endpoints.Queue endpoint)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleQueueEndpoint(endpoint);
            if (ids is null)
            {
                if (Debug) ReportDebug($"CheckSingleQueuePage(\"{outDir}\") - Client failure");
                return null;
            }

            // Try to download all IDs
            List<int> processed = [];
            foreach (int id in ids)
            {
                try
                {
                    bool downloaded = await DownloadSingleQueuePage(id, outDir, rename: false);
                    if (!downloaded && !IgnoreErrors)
                        return processed;

                    processed.Add(id);
                }
                catch (Exception ex)
                {
                    ReportError($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return processed;
        }

        #endregion

        #region Single Download

        /// <summary>
        /// Download the database
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadDatabase(string? outDir, string? subfolder = null)
        {
            try
            {
                if (Debug) ReportDebug($"DownloadDatabase(\"{outDir}\", {subfolder})");

                // If the user is not logged in, this is invalid
                if (!_loggedIn)
                {
                    ReportError("Database download requires valid login credentials!");
                    return false;
                }

                // Determine the database URL
                var endpoint = new Endpoints.Downloads { Database = true };
                string dbUri = endpoint.BuildUrl();

                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                {
                    if (Debug) ReportDebug("Output directory was not provided, setting to current directory");
                    outDir = Environment.CurrentDirectory;
                }

                // Make the call to get the pack
                string tempfile = Path.Combine(outDir, "tmp" + Guid.NewGuid().ToString());
                string? remoteFileName = await DownloadFile(dbUri, tempfile);
                if (remoteFileName is null)
                    return false;

                MoveOrDelete(tempfile, remoteFileName, outDir!, subfolder);
                return true;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">System to download packs for</param>
        /// <returns>Byte array containing the downloaded pack, null on error</returns>
        public async Task<byte[]?> DownloadSinglePack(PackType packType, PhysicalSystem? system)
        {
            try
            {
                if (Debug) ReportDebug($"DownloadSinglePack(\"{packType}\", {system})");

                // If the system is invalid, we can't do anything
                if (system is null || !system.Available || string.IsNullOrEmpty(system.Code))
                {
                    if (Debug) ReportDebug($"{system} is not marked as available on Redump, skipping...");
                    return null;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) ReportDebug($"{packType} is not available for {system}, skipping...");
                    return null;
                }

                // Determine the pack URL
                var endpoint = new Endpoints.Downloads
                {
                    Pack = packType,
                    System = system,
                };
                string packUri = endpoint.BuildUrl();

                return await DownloadData(packUri);
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download a single pack
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="system">System to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadSinglePack(PackType packType,
            PhysicalSystem? system,
            string? outDir,
            string? subfolder = null)
        {
            try
            {
                if (Debug) ReportDebug($"DownloadSinglePack(\"{packType}\", {system}, \"{outDir}\")");

                // If the system is invalid, we can't do anything
                if (system is null || !system.Available || string.IsNullOrEmpty(system.Code))
                {
                    if (Debug) ReportDebug($"{system} is not marked as available on Redump, skipping...");
                    return false;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) ReportDebug($"{packType} is not available for {system}, skipping...");
                    return false;
                }

                // Determine the pack URL
                var endpoint = new Endpoints.Downloads
                {
                    Pack = packType,
                    System = system,
                };
                string packUri = endpoint.BuildUrl();

                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                {
                    if (Debug) ReportDebug("Output directory was not provided, setting to current directory");
                    outDir = Environment.CurrentDirectory;
                }

                // Make the call to get the pack
                string tempfile = Path.Combine(outDir, "tmp" + Guid.NewGuid().ToString());
                string? remoteFileName = await DownloadFile(packUri, tempfile);
                if (remoteFileName is null)
                    return false;

                MoveOrDelete(tempfile, remoteFileName, outDir!, subfolder);
                return true;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual disc ID page, if possible
        /// </summary>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
        /// <remarks>This only includes the site page itself</remarks>
        public async Task<string?> DownloadSingleDiscPage(int id)
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            ReportGeneric($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                var endpoint = new Endpoints.Disc { Id = id };
                string discPageUri = endpoint.BuildUrl();
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    ReportError($"An error occurred retrieving ID {paddedId}!");
                    return null;
                }
                else if (discPage.Contains($"Disc with ID \"{id}\" doesn't exist"))
                {
                    ReportError($"ID {paddedId} could not be found!");
                    return null;
                }

                ReportGeneric($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download an individual disc ID data, if possible
        /// </summary>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="rename">True to rename deleted entries, false otherwise</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>True if all data was downloaded, false otherwise</returns>
        /// <remarks>This includes all subpages and attached files</remarks>
        public async Task<bool> DownloadSingleDiscPage(int id,
            string? outDir,
            bool rename,
            DiscSubpath[]? discSubpaths = null)
        {
            // If no output directory is defined, use the current directory instead
            if (string.IsNullOrEmpty(outDir))
                outDir = Environment.CurrentDirectory;

            string paddedId = id.ToString().PadLeft(6, '0');
            string paddedIdDir = Path.Combine(outDir, paddedId);
            ReportGeneric($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                var endpoint = new Endpoints.Disc { Id = id };
                string discPageUri = endpoint.BuildUrl();
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    ReportError($"An error occurred retrieving ID {paddedId}!");
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

                    ReportError($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (!Overwrite && File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Check for the last modified date in both pages
                    var oldResult = Constants.ModifiedRegex.Match(oldDiscPage);
                    var newResult = Constants.ModifiedRegex.Match(discPage);

                    // If both pages contain the same modified date, skip it
                    if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    {
                        ReportGeneric($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }

                    // If neither page contains a modified date, skip it
                    else if (!oldResult.Success && !newResult.Success)
                    {
                        ReportGeneric($"ID {paddedId} has not been changed since last download, skipping...");
                        return false;
                    }
                }

                // If the downloaded data is invalid or otherwise empty, skip it
                var hasAddedDate = Constants.AddedRegex.Match(discPage);
                var hasModifiedDate = Constants.ModifiedRegex.Match(discPage);
                if (!hasAddedDate.Success && !hasModifiedDate.Success)
                {
                    ReportGeneric($"ID {paddedId} retieved an empty page, skipping...");
                    return false;
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

                #region Pages

                // View Edit History
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.History)) && discPage.Contains($"<a href=\"/queue/?disc_id=/{id}"))
                {
                    var subEndpoint = new Endpoints.Disc
                    {
                        Id = id,
                        Subpath = DiscSubpath.History,
                    };
                    string uri = endpoint.BuildUrl();
                    string? changesPage = await DownloadString(uri);
                    if (!IgnoreErrors && changesPage is null)
                    {
                        if (Debug) ReportDebug($"Downloading changes page for {id} failed!");
                        return false;
                    }

                    using var sw = File.CreateText(Path.Combine(paddedIdDir, "changes.html"));
                    sw.Write(changesPage);
                    sw.Flush();
                }

                // Edit disc
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.Edit)) && discPage.Contains($"<a href=\"/disc/{id}/edit/\""))
                {
                    var subEndpoint = new Endpoints.Disc
                    {
                        Id = id,
                        Subpath = DiscSubpath.Edit,
                    };
                    string uri = endpoint.BuildUrl();
                    string? editPage = await DownloadString(uri);
                    if (!IgnoreErrors && editPage is null)
                    {
                        if (Debug) ReportDebug($"Downloading edit page for {id} failed!");
                        return false;
                    }

                    using var sw = File.CreateText(Path.Combine(paddedIdDir, "edit.html"));
                    sw.Write(editPage);
                    sw.Flush();
                }

                #endregion

                #region Files

                // CUE
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.Cuesheet)) && discPage.Contains($"<a href=\"/disc/{id}/cue\""))
                {
                    var subEndpoint = new Endpoints.Disc
                    {
                        Id = id,
                        Subpath = DiscSubpath.Cuesheet,
                    };
                    string uri = endpoint.BuildUrl();
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.cue"));
                    if (!IgnoreErrors && remoteName is null)
                    {
                        if (Debug) ReportDebug($"Downloading cuesheet for {id} failed!");
                        return false;
                    }
                }

                // SBI
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.SBI)) && discPage.Contains($"<a href=\"/disc/{id}/sbi\""))
                {
                    var subEndpoint = new Endpoints.Disc
                    {
                        Id = id,
                        Subpath = DiscSubpath.SBI,
                    };
                    string uri = endpoint.BuildUrl();
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.sbi"));
                    if (!IgnoreErrors && remoteName is null)
                    {
                        if (Debug) ReportDebug($"Downloading SBI for {id} failed!");
                        return false;
                    }
                }

                // LSD
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.LSD)) && discPage.Contains($"<a href=\"/disc/{id}/lsd\""))
                {
                    var subEndpoint = new Endpoints.Disc
                    {
                        Id = id,
                        Subpath = DiscSubpath.LSD,
                    };
                    string uri = endpoint.BuildUrl();
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.lsd"));
                    if (!IgnoreErrors && remoteName is null)
                    {
                        if (Debug) ReportDebug($"Downloading LSD for {id} failed!");
                        return false;
                    }
                }

                #endregion

                // HTML (Last in case of errors)
                using (var discStreamWriter = File.CreateText(Path.Combine(paddedIdDir, "disc.html")))
                {
                    discStreamWriter.Write(discPage);
                    discStreamWriter.Flush();
                }

                ReportGeneric($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Download an individual queue ID data, if possible
        /// </summary>
        /// <param name="id">Redump queue disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
        public async Task<string?> DownloadSingleQueuePage(int id)
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            ReportGeneric($"Processing queue ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                var endpoint = new Endpoints.Queue { Id = id };
                string discPageUri = endpoint.BuildUrl();
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    ReportError($"An error occurred retrieving ID {paddedId}!");
                    return null;
                }
                else if (discPage.Contains("Not found"))
                {
                    ReportError($"ID {paddedId} could not be found!");
                    return null;
                }

                ReportGeneric($"ID {paddedId} has been successfully downloaded");
                return discPage;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return null;
            }
        }

        /// <summary>
        /// Download an individual queue disc ID data, if possible
        /// </summary>
        /// <param name="id">Redump queue disc ID to retrieve</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="rename">True to rename deleted entries, false otherwise</param>
        /// <returns>True if all data was downloaded, false otherwise</returns>
        public async Task<bool> DownloadSingleQueuePage(int id, string? outDir, bool rename)
        {
            // If no output directory is defined, use the current directory instead
            if (string.IsNullOrEmpty(outDir))
            {
                if (Debug) ReportDebug("Output directory was not provided, setting to current directory");
                outDir = Environment.CurrentDirectory;
            }

            string paddedId = id.ToString().PadLeft(6, '0');
            string paddedIdDir = Path.Combine(outDir, paddedId);
            ReportGeneric($"Processing queue ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                var endpoint = new Endpoints.Queue { Id = id };
                string discPageUri = endpoint.BuildUrl();
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    ReportError($"An error occurred retrieving ID {paddedId}!");
                    return false;
                }
                else if (discPage.Contains("Not found"))
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

                    ReportError($"ID {paddedId} could not be found!");
                    return false;
                }

                // Check if the page has been updated since the last time it was downloaded, if possible
                if (!Overwrite && File.Exists(Path.Combine(paddedIdDir, "disc.html")))
                {
                    // Read in the cached file
                    var oldDiscPage = File.ReadAllText(Path.Combine(paddedIdDir, "disc.html"));

                    // Figure out how to reenable this check for a queue page

                    // // Check for the last modified date in both pages
                    // var oldResult = Constants.LastModifiedRegex.Match(oldDiscPage);
                    // var newResult = Constants.LastModifiedRegex.Match(discPage);

                    // // If both pages contain the same ID, skip it
                    // if (oldResult.Success && newResult.Success && oldResult.Groups[1].Value == newResult.Groups[1].Value)
                    // {
                    //     Progress.Report($"ID {paddedId} has not been changed since last download, skipping...");
                    //     return false;
                    // }

                    // // If neither page contains an ID, skip it
                    // else if (!oldResult.Success && !newResult.Success)
                    // {
                    //     Progress.Report($"ID {paddedId} has not been changed since last download, skipping...");
                    //     return false;
                    // }

                    // Check the added date as a backup
                    var oldResult = Constants.AddedRegex.Match(oldDiscPage);
                    var newResult = Constants.AddedRegex.Match(discPage);

                    // If the downloaded data is invalid or otherwise empty, skip it
                    if (oldResult.Success && !newResult.Success)
                    {
                        ReportGeneric($"ID {paddedId} retieved an empty page, skipping...");
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

                ReportGeneric($"ID {paddedId} has been successfully downloaded");
                return true;
            }
            catch (Exception ex)
            {
                ReportError($"An exception has occurred: {ex}");
                return false;
            }
        }

        #endregion

        #region Multiple Download

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="systems">Systems to download packs for</param>
        public async Task<Dictionary<PhysicalSystem, byte[]>> DownloadPacks(PackType packType, PhysicalSystem[] systems)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
            {
                if (Debug) ReportDebug($"{packType} is not a recognized pack type, skipping...");
                return [];
            }

            var packsDictionary = new Dictionary<PhysicalSystem, byte[]>();
            foreach (var system in systems)
            {
                ReportGeneric(system.Name);
                byte[]? pack = await DownloadSinglePack(packType, system);
                if (pack is not null)
                    packsDictionary.Add(system, pack);
            }

            // Report final completion
            ReportGeneric("Complete!");

            // Empty report for padding
            ReportGeneric("");
            return packsDictionary;
        }

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        public async Task<bool> DownloadPacks(PackType packType, PhysicalSystem[] systems, string? outDir)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
            {
                if (Debug) ReportDebug($"{packType} is not a recognized pack type, skipping...");
                return false;
            }

            foreach (var system in systems)
            {
                ReportGeneric(system.Name);
                await DownloadSinglePack(packType, system, outDir);
            }

            // Report final completion
            ReportGeneric("Complete!");

            // Empty report for padding
            ReportGeneric("");
            return true;
        }

        /// <summary>
        /// Download a set of packs
        /// </summary>
        /// <param name="packType">Pack type to use to determine the download URL</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadPacks(PackType packType, PhysicalSystem[] systems, string? outDir, string? subfolder)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
            {
                if (Debug) ReportDebug($"{packType} is not a recognized pack type, skipping...");
                return false;
            }

            foreach (var system in systems)
            {
                // If the system is invalid, we can't do anything
                if (!system.Available)
                {
                    if (Debug) ReportDebug($"{system} is not marked as available on Redump, skipping...");
                    continue;
                }

                // If the system is unknown, we can't do anything
                if (string.IsNullOrEmpty(system.Name))
                {
                    if (Debug) ReportDebug($"{system} is not a recognized system, skipping...");
                    continue;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system))
                {
                    if (Debug) ReportDebug($"{packType} is not available for {system}, skipping...");
                    continue;
                }

                ReportGeneric(system.Name);
                await DownloadSinglePack(packType, system, outDir, subfolder);
            }

            // Report final completion
            ReportGeneric("Complete!");

            // Empty report for padding
            ReportGeneric("");
            return true;
        }

        #endregion

        #region Private Download Helpers

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
        /// Determine if a pack is available for a given system
        /// </summary>
        /// <param name="packType">Pack type to use to determine the support status</param>
        /// <param name="system">Systems to determine pack availability for</param>
        /// <returns>True if the pack is available for a system, false otherwise</returns>
        private static bool PackTypeToAvailable(PackType packType, PhysicalSystem system)
        {
            return packType switch
            {
                PackType.Cuesheets => system.HasCues,
                PackType.Datfile => system.HasDat,
                PackType.Sbis => system.HasSbi,
                PackType.BiosDatfile => system.HasBios,
                _ => false,
            };
        }

        #endregion

        #region Progress

        /// <summary>
        /// Report a debug progress statement
        /// </summary>
        /// <param name="value">Value to report</param>
        private void ReportDebug(string value) => _progress.Report(new WebClientEventArgs(value, debug: true));

        /// <summary>
        /// Report an error progress statement
        /// </summary>
        /// <param name="value">Value to report</param>
        private void ReportError(string value) => _progress.Report(new WebClientEventArgs(value, error: true));

        /// <summary>
        /// Report a generic progress statement
        /// </summary>
        /// <param name="value">Value to report</param>
        private void ReportGeneric(string value) => _progress.Report(value);

        #endregion
    }
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
}
