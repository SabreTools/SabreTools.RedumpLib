using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib
{
    /// <summary>
    /// Contains logic for dealing with downloads
    /// </summary>
    public class Downloader
    {
        #region Properties

        /// <summary>
        /// Which Redump feature is being used
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// Minimum ID for downloading page information (Feature.Site, Feature.WIP only)
        /// </summary>
        public int MinimumId { get; set; }

        /// <summary>
        /// Maximum ID for downloading page information (Feature.Site, Feature.WIP only)
        /// </summary>
        public int MaximumId { get; set; }

        /// <summary>
        /// Quicksearch text for downloading
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// Directory to save all outputted files to
        /// </summary>
        public string? OutDir { get; set; }

        /// <summary>
        /// Use named subfolders for discrete download sets (Feature.Packs only)
        /// </summary>
        public bool UseSubfolders { get; set; }

        /// <summary>
        /// Use the last modified page to try to grab all new discs (Feature.Site, Feature.WIP only)
        /// </summary>
        public bool OnlyNew { get; set; }

        /// <summary>
        /// Only list the page IDs but don't download
        /// </summary>
        public bool OnlyList { get; set; }

        /// <summary>
        /// Force continuing downloads until user cancels or pages run out
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Redump username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Redump password
        /// </summary>
        public string? Password { get; set; }

        #endregion

        #region Private Vars

        /// <summary>
        /// Current HTTP rc to use
        /// </summary>
        private readonly RedumpClient _client;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Downloader()
        {
            _client = new RedumpClient();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Preconfigured client</param>
        public Downloader(RedumpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Run the downloads that should go
        /// </summary>
        /// <returns>True if there was a valid download type, false otherwise</returns>
        public async Task<bool> Download()
        {
            // Login to Redump, if possible
            if (!_client.LoggedIn)
                await _client.Login(Username ?? string.Empty, Password ?? string.Empty);

            switch (Feature)
            {
                case Feature.Site:
                    if (OnlyNew)
                        await Discs.DownloadLastModified(_client, OutDir, Force);
                    else
                        await Discs.DownloadSiteRange(_client, OutDir, MinimumId, MaximumId);
                    break;
                case Feature.WIP:
                    if (OnlyNew)
                        await WIP.DownloadLastSubmitted(_client, OutDir);
                    else
                        await WIP.DownloadWIPRange(_client, OutDir, MinimumId, MaximumId);
                    break;
                case Feature.Packs:
                    await Packs.DownloadPacks(_client, OutDir, UseSubfolders);
                    break;
                case Feature.User:
                    if (OnlyList)
                        await User.ListUser(_client, Username);
                    else if (OnlyNew)
                        await User.DownloadUserLastModified(_client, Username, OutDir);
                    else
                        await User.DownloadUser(_client, Username, OutDir);
                    break;
                case Feature.Quicksearch:
                    if (OnlyList)
                        await Search.ListSearchResults(_client, QueryString);
                    else
                        await Search.DownloadSearchResults(_client, QueryString, OutDir);
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
