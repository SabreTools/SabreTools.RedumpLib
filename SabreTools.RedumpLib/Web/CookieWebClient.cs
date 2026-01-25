using System;
using System.Net;

namespace SabreTools.RedumpLib.Web
{
    internal class CookieWebClient : WebClient
    {
        /// <summary>
        /// The timespan to wait before the request times out.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        // https://stackoverflow.com/questions/1777221/using-cookiecontainer-with-webclient-class
        private readonly CookieContainer _container = new();

        /// <summary>
        /// Get the last downloaded filename, if possible
        /// </summary>
        public string? GetLastFilename()
        {
            // If the response headers are null or empty
            if (ResponseHeaders == null || ResponseHeaders.Count == 0)
                return null;

            // If we don't have the response header we care about
            string? headerValue = ResponseHeaders.Get("Content-Disposition");
            if (string.IsNullOrEmpty(headerValue))
                return null;

            // Extract the filename from the value
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
            return headerValue[(headerValue.IndexOf("filename=") + 9)..].Replace("\"", "");
#else
            return headerValue.Substring(headerValue.IndexOf("filename=") + 9).Replace("\"", "");
#endif
        }

        /// <inheritdoc/>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest webRequest)
            {
                webRequest.Timeout = (int)Timeout.TotalMilliseconds;
                webRequest.CookieContainer = _container;
            }

            return request;
        }
    }
}
