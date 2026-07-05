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
        public Client()
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
        /// <param name="username">redump.info username</param>
        /// <param name="password">redump.info password</param>
        /// <returns>True if the user could be logged in, false otherwise, null on error</returns>
        public static async Task<bool?> ValidateCredentials(string? username, string? password)
        {
            // If options are invalid or we're missing something key, just return
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            // Try logging in with the supplied credentials otherwise
            var redumpClient = new Client();
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
                Console.WriteLine("Already logged in!");
                return true;
            }

            // Credentials verification
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Credentials entered, will attempt redump.info login...");
            }
            else if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                Console.Error.WriteLine("Only a username was specified, will not attempt redump.info login...");
                return false;
            }
            else if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("No credentials entered, will not attempt redump.info login...");
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

                    // Get the current information from the login page
                    var loginPage = await DownloadString(LoginUrl);
                    string creationTime = Constants.LoginCreationTimeRegex.Match(loginPage ?? string.Empty).Groups[1].Value;
                    string formToken = Constants.FormTokenRegex.Match(loginPage ?? string.Empty).Groups[1].Value;
                    string sessionId = Constants.SessionIDRegex.Match(loginPage ?? string.Empty).Groups[1].Value;

                    // TODO: Determine which of the redirect items is needed, if either

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
                        Console.Error.WriteLine($"An error occurred while trying to log in on attempt {i}: No response");
                        continue;
                    }

                    // "Your posts" indicates login was successful
                    if (!responseContent.Contains(@"<a href=""./search.php?search_id=egosearch"" role=""menuitem"">"))
                    {
                        Console.Error.WriteLine("Invalid credentials entered, continuing without logging in...");
                        return false;
                    }

                    // The user was able to be logged in
                    Console.WriteLine("Credentials accepted! Logged into Redump...");
                    _loggedIn = true;

                    // If the user is a moderator or staff, set accordingly
                    if (responseContent.Contains("https://forum.redump.info/viewforum.php?f=4"))
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
                Console.Error.WriteLine("Invalid number of attempts provided, must be at least 1");
                return null;
            }

            for (int i = 0; i < AttemptCount; i++)
            {
                try
                {
                    if (Debug) Console.WriteLine($"DEBUG: GetResolvedURL(\"{uri}\"), Attempt {i + 1} of {AttemptCount}");
#if NETCOREAPP
                    // Make the call to get the file
                    var response = await _internalClient.GetAsync(uri);
                    if (response?.Content?.Headers is null || !response.IsSuccessStatusCode)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return response.RequestMessage?.RequestUri?.AbsoluteUri;
#elif NET40
                    await Task.Factory.StartNew(() => { _internalClient.DownloadData(uri); return true; });
                    string? lastUrl = _internalClient.LastUrl?.AbsoluteUri;
                    if (lastUrl is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return lastUrl;
#else
                    await Task.Run(() => _internalClient.DownloadData(uri));
                    string? lastUrl = _internalClient.LastUrl?.AbsoluteUri;
                    if (lastUrl is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: GetResolvedURL failed, continuing...");
                        continue;
                    }

                    return lastUrl;
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
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeExact">Set exact barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionExact">Set exact edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="offset">Add offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialExact">Set exact serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleExact">Set exact title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignExact">Set exact foreign title handling, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsPage(
            bool? advanced = null,
            string? barcode = null,
            bool? barcodeExact = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            bool? editionExact = null,
            long? errorsMax = null,
            long? errorsMin = null,
            Language? language = null,
            char? letter = null,
            MediaType? media = null,
            long? offset = null,
            SortDirection? order = null,
            long? page = null,
            string? protection = null,
            string? query = null,
            Region? region = null,
            string? ringcode = null,
            string? serial = null,
            bool? serialExact = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? title = null,
            bool? titleExact = null,
            string? titleForeign = null,
            bool? titleForeignExact = null,
            long? tracksMax = null,
            long? tracksMin = null)
        {
            // Normalize the search query, if needed
            if (query is not null)
                query = NormalizeQuery(query);

            string url = UrlBuilder.BuildDiscsUrl(
                advanced,
                barcode,
                barcodeExact,
                category,
                comments,
                contents,
                dumper,
                edc,
                edition,
                editionExact,
                errorsMax,
                errorsMin,
                language,
                letter,
                media,
                offset,
                order,
                page,
                protection,
                query,
                region,
                ringcode,
                serial,
                serialExact,
                sort,
                status,
                system,
                title,
                titleExact,
                titleForeign,
                titleForeignExact,
                tracksMax,
                tracksMin);

            List<int> ids = [];

            // Try to retrieve the data
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleDiscsPage(\"{url}\") - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleDiscsPage(\"{url}\") - No discs found");
                return ids;
            }

            // If we have a single disc page already
            if (dumpsPage.Contains("<h3>Disc</h3>"))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleDiscsPage(\"{url}\") - Single disc page");
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
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return ids;
        }

        /// <summary>
        /// Process a Redump discs page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="advanced">Set advanced search status, null to omit</param>
        /// <param name="barcode">Add barcode to filter, null to omit</param>
        /// <param name="barcodeExact">Set exact barcode handling, null to omit</param>
        /// <param name="category">Add category to filter, null to omit</param>
        /// <param name="comments">Add comments to filter, null to omit</param>
        /// <param name="contents">Add contents to filter, null to omit</param>
        /// <param name="dumper">Add dumper name to filter, null to omit</param>
        /// <param name="edc">Add EDC status to filter, null to omit</param>
        /// <param name="edition">Add edition to filter, null to omit</param>
        /// <param name="editionExact">Set exact edition handling, null to omit</param>
        /// <param name="errorsMax">Add maximum error count to filter, null to omit</param>
        /// <param name="errorsMin">Add minimum error count to filter, null to omit</param>
        /// <param name="language">Add language to filter, null to omit</param>
        /// <param name="letter">Starts with upper-case letter or '#' for numbers, null to omit</param>
        /// <param name="media">Add media type to filter, null to omit</param>
        /// <param name="offset">Add offset to filter, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="protection">Add protection to filter, null to omit</param>
        /// <param name="query">Generic text query to filter, null to omit</param>
        /// <param name="region">Add region to filter, null to omit</param>
        /// <param name="ringcode">Add ringcode to filter, null to omit</param>
        /// <param name="serial">Add serial to filter, null to omit</param>
        /// <param name="serialExact">Set exact serial handling, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <param name="title">Add title to filter, null to omit</param>
        /// <param name="titleExact">Set exact title handling, null to omit</param>
        /// <param name="titleForeign">Add foreign title to filter, null to omit</param>
        /// <param name="titleForeignExact">Set exact foreign title handling, null to omit</param>
        /// <param name="tracksMax">Add maximum track count to filter, null to omit</param>
        /// <param name="tracksMin">Add minimum track count to filter, null to omit</param>
        /// <param name="discSubpaths">Set of subpaths to download if available, null for all</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleDiscsPage(
            string? outDir,
            bool? advanced = null,
            string? barcode = null,
            bool? barcodeExact = null,
            DiscCategory? category = null,
            string? comments = null,
            string? contents = null,
            string? dumper = null,
            YesNo? edc = null,
            string? edition = null,
            bool? editionExact = null,
            long? errorsMax = null,
            long? errorsMin = null,
            Language? language = null,
            char? letter = null,
            MediaType? media = null,
            long? offset = null,
            SortDirection? order = null,
            long? page = null,
            string? protection = null,
            string? query = null,
            Region? region = null,
            string? ringcode = null,
            string? serial = null,
            bool? serialExact = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            PhysicalSystem? system = null,
            string? title = null,
            bool? titleExact = null,
            string? titleForeign = null,
            bool? titleForeignExact = null,
            long? tracksMax = null,
            long? tracksMin = null,
            DiscSubpath[]? discSubpaths = null)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleDiscsPage(
                advanced,
                barcode,
                barcodeExact,
                category,
                comments,
                contents,
                dumper,
                edc,
                edition,
                editionExact,
                errorsMax,
                errorsMin,
                language,
                letter,
                media,
                offset,
                order,
                page,
                protection,
                query,
                region,
                ringcode,
                serial,
                serialExact,
                sort,
                status,
                system,
                title,
                titleExact,
                titleForeign,
                titleForeignExact,
                tracksMax,
                tracksMin);
            if (ids is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleDiscsPage(\"{outDir}\") - Client failure");
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
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    continue;
                }
            }

            return processed;
        }

        /// <summary>
        /// Process a Redump queue page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="discId">Add disc ID to filter, null to omit</param>
        /// <param name="isDiscHistory">Set disc history status, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="submitter">Add submitter name to filter, null to omit</param>
        /// <param name="subType">Add submission type to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <returns>List of IDs from the page, empty on none, null on error</returns>
        public async Task<List<int>?> CheckSingleQueuePage(
            long? discId = null,
            bool? isDiscHistory = null,
            SortDirection? order = null,
            long? page = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            string? submitter = null,
            SubmissionType? subType = null,
            PhysicalSystem? system = null)
        {
            List<int> ids = [];

            // Try to retrieve the data
            string url = UrlBuilder.BuildQueueUrl(
                discId,
                isDiscHistory,
                order,
                page,
                sort,
                status,
                submitter,
                subType,
                system);
            string? dumpsPage = await DownloadString(url);

            // If the web client failed, return null
            if (dumpsPage is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleQueuePage() - Client failure");
                return null;
            }

            // If we have no dumps left
            if (dumpsPage.Contains("No discs found."))
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleQueuePage() - No discs found");
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
        /// Process a Redump queue page as a list of possible IDs or disc page
        /// </summary>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="discId">Add disc ID to filter, null to omit</param>
        /// <param name="isDiscHistory">Set disc history status, null to omit</param>
        /// <param name="order">Add sorting direction, null to omit</param>
        /// <param name="page">Page number, null to omit</param>
        /// <param name="sort">Add sorting type, null to omit</param>
        /// <param name="status">Add status to filter, null to omit</param>
        /// <param name="submitter">Add submitter name to filter, null to omit</param>
        /// <param name="subType">Add submission type to filter, null to omit</param>
        /// <param name="system">Add system to filter, null to omit</param>
        /// <returns>List of IDs that were found on success, empty on error</returns>
        public async Task<List<int>?> CheckSingleQueuePage(
            string? outDir,
            long? discId = null,
            bool? isDiscHistory = null,
            SortDirection? order = null,
            long? page = null,
            SortCategory? sort = null,
            DumpStatus? status = null,
            string? submitter = null,
            SubmissionType? subType = null,
            PhysicalSystem? system = null)
        {
            // Get all IDs from the page
            List<int>? ids = await CheckSingleQueuePage(
                discId,
                isDiscHistory,
                order,
                page,
                sort,
                status,
                submitter,
                subType,
                system);
            if (ids is null)
            {
                if (Debug) Console.WriteLine($"DEBUG: CheckSingleQueuePage(\"{outDir}\") - Client failure");
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
        /// <returns>Normalized query</returns>
        private static string NormalizeQuery(string query)
        {
            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            query = query.Replace('\\', '-');
            query = query.Replace('/', '-');

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
        public async Task<byte[]?> DownloadSinglePack(PackType packType, PhysicalSystem? system)
        {
            try
            {
                if (Debug) Console.WriteLine($"DEBUG: DownloadSinglePack(\"{packType}\", {system})");

                // If the system is invalid, we can't do anything
                string? shortName = system.ShortName();
                if (system is null || !system.IsAvailable() || string.IsNullOrEmpty(shortName))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not marked as available on Redump, skipping...");
                    return null;
                }

                // If we didn't have credentials
                if (!_loggedIn && packType == PackType.Keys)
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} requires a user login to access keys, skipping...");
                    return null;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system.Value))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {packType} is not available for {system}, skipping...");
                    return null;
                }

                // Determine the pack URL
                string packUri = UrlBuilder.BuildPackUrl(packType, system.Value);
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
        /// <param name="subfolder">Named subfolder for the pack, used optionally</param>
        public async Task<bool> DownloadSinglePack(PackType packType,
            PhysicalSystem? system,
            string? outDir,
            string? subfolder = null)
        {
            try
            {
                if (Debug) Console.WriteLine($"DEBUG: DownloadSinglePack(\"{packType}\", {system}, \"{outDir}\")");

                // If the system is invalid, we can't do anything
                string? shortName = system.ShortName();
                if (system is null || !system.IsAvailable() || string.IsNullOrEmpty(shortName))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} is not marked as available on Redump, skipping...");
                    return false;
                }

                // If we didn't have credentials
                if (!_loggedIn && packType == PackType.Keys)
                {
                    if (Debug) Console.WriteLine($"DEBUG: {system} requires a user login to access keys, skipping...");
                    return false;
                }

                // If the pack is not supported for the system
                if (!PackTypeToAvailable(packType, system.Value))
                {
                    if (Debug) Console.WriteLine($"DEBUG: {packType} is not available for {system}, skipping...");
                    return false;
                }

                // Determine the pack URL
                string packUri = UrlBuilder.BuildPackUrl(packType, system.Value);

                // If no output directory is defined, use the current directory instead
                if (string.IsNullOrEmpty(outDir))
                {
                    if (Debug) Console.WriteLine("DEBUG: Output directory was not provided, setting to current directory");
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
                Console.Error.WriteLine($"An exception has occurred: {ex}");
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
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = UrlBuilder.BuildDiscUrl(id);
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
            Console.WriteLine($"Processing ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = UrlBuilder.BuildDiscUrl(id);
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
                    var oldResult = Constants.ModifiedRegex.Match(oldDiscPage);
                    var newResult = Constants.ModifiedRegex.Match(discPage);

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
                }

                // If the downloaded data is invalid or otherwise empty, skip it
                var hasAddedDate = Constants.AddedRegex.Match(discPage);
                var hasModifiedDate = Constants.ModifiedRegex.Match(discPage);
                if (!hasAddedDate.Success && !hasModifiedDate.Success)
                {
                    Console.WriteLine($"ID {paddedId} retieved an empty page, skipping...");
                    return false;
                }

                // Create ID subdirectory
                Directory.CreateDirectory(paddedIdDir);

                #region Pages

                // View Edit History
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.History)) && discPage.Contains($"<a href=\"/queue/?disc_id=/{id}"))
                {
                    string uri = UrlBuilder.BuildDiscUrl(id, DiscSubpath.History);
                    string? changesPage = await DownloadString(uri);
                    if (!IgnoreErrors && changesPage is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: Downloading changes page for {id} failed!");
                        return false;
                    }

                    using var sw = File.CreateText(Path.Combine(paddedIdDir, "changes.html"));
                    sw.Write(changesPage);
                    sw.Flush();
                }

                // Edit disc
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.Edit)) && discPage.Contains($"<a href=\"/disc/{id}/edit/\""))
                {
                    string uri = UrlBuilder.BuildDiscUrl(id, DiscSubpath.Edit);
                    string? editPage = await DownloadString(uri);
                    if (!IgnoreErrors && editPage is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: Downloading edit page for {id} failed!");
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
                    string uri = UrlBuilder.BuildDiscUrl(id, DiscSubpath.Cuesheet);
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.cue"));
                    if (!IgnoreErrors && remoteName is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: Downloading cuesheet for {id} failed!");
                        return false;
                    }
                }

                // SBI
                if ((discSubpaths is null || Array.Exists(discSubpaths, s => s is DiscSubpath.SBI)) && discPage.Contains($"<a href=\"/disc/{id}/sbi\""))
                {
                    string uri = UrlBuilder.BuildDiscUrl(id, DiscSubpath.SBI);
                    string? remoteName = await DownloadFile(uri, Path.Combine(paddedIdDir, $"{paddedId}.sbi"));
                    if (!IgnoreErrors && remoteName is null)
                    {
                        if (Debug) Console.Error.WriteLine($"DEBUG: Downloading SBI for {id} failed!");
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
        /// Download an individual queue ID data, if possible
        /// </summary>
        /// <param name="id">Redump queue disc ID to retrieve</param>
        /// <returns>String containing the page contents if successful, null on error</returns>
        public async Task<string?> DownloadSingleQueuePage(int id)
        {
            string paddedId = id.ToString().PadLeft(6, '0');
            Console.WriteLine($"Processing queue ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = UrlBuilder.BuildQueueDiscUrl(id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
                    return null;
                }
                else if (discPage.Contains("Not found"))
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
                if (Debug) Console.WriteLine("DEBUG: Output directory was not provided, setting to current directory");
                outDir = Environment.CurrentDirectory;
            }

            string paddedId = id.ToString().PadLeft(6, '0');
            string paddedIdDir = Path.Combine(outDir, paddedId);
            Console.WriteLine($"Processing queue ID: {paddedId}");
            try
            {
                // Try to retrieve the data
                string discPageUri = UrlBuilder.BuildQueueDiscUrl(id);
                string? discPage = await DownloadString(discPageUri);

                if (discPage is null)
                {
                    Console.Error.WriteLine($"An error occurred retrieving ID {paddedId}!");
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

                    Console.Error.WriteLine($"ID {paddedId} could not be found!");
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
                    //     Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                    //     return false;
                    // }

                    // // If neither page contains an ID, skip it
                    // else if (!oldResult.Success && !newResult.Success)
                    // {
                    //     Console.WriteLine($"ID {paddedId} has not been changed since last download, skipping...");
                    //     return false;
                    // }

                    // Check the added date as a backup
                    var oldResult = Constants.AddedRegex.Match(oldDiscPage);
                    var newResult = Constants.AddedRegex.Match(discPage);

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
        public async Task<Dictionary<PhysicalSystem, byte[]>> DownloadPacks(PackType packType, PhysicalSystem[] systems)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
            {
                if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                return [];
            }

            var packsDictionary = new Dictionary<PhysicalSystem, byte[]>();
            foreach (var system in systems)
            {
                string longName = system.LongName() ?? $"UNKNOWN_{system}";
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
        public async Task<bool> DownloadPacks(PackType packType, PhysicalSystem[] systems, string? outDir)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
            {
                if (Debug) Console.Error.WriteLine($"DEBUG: {packType} is not a recognized pack type, skipping...");
                return false;
            }

            foreach (var system in systems)
            {
                string longName = system.LongName() ?? $"UNKNOWN_{system}";
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
        public async Task<bool> DownloadPacks(PackType packType, PhysicalSystem[] systems, string? outDir, string? subfolder)
        {
            // Determine if the pack type is valid
            if (!Enum.IsDefined(typeof(PackType), packType))
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
                PackType.Cuesheets => system.HasCues(),
                PackType.Datfile => system.HasDat(),
                PackType.DecryptedKeys => false,
                PackType.Gdis => false,
                PackType.Keys => system.HasKeys(),
                PackType.Lsds => false,
                PackType.Sbis => system.HasSbi(),
                _ => false,
            };
        }

        #endregion
    }
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
}
