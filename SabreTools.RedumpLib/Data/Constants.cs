using System.Text.RegularExpressions;

namespace SabreTools.RedumpLib.Data
{
    public static class Constants
    {
        #region Regular Expressions

        /// <summary>
        /// Regex matching the added field on a disc page
        /// </summary>
        public static readonly Regex AddedRegex = new(@"<tr><td><strong>Added</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the category field on a disc page
        /// </summary>
        public static readonly Regex CategoryRegex = new(@"<tr><td><strong>Category</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the comments field on a disc page
        /// </summary>
        public static readonly Regex CommentsRegex = new(@"<h3>Comments</h3><p class=""pre-wrap disc-comments"">(.*?)</p>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the contents field on a disc page
        /// </summary>
        public static readonly Regex ContentsRegex = new(@"<summary><h3>Contents</h3></summary><p class=""pre-wrap disc-contents"">(.*?)</p>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching individual disc links on a results page
        /// </summary>
        public static readonly Regex DiscRegex = new(@"<a href=""/disc/(\d+)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the disc number field on a disc page
        /// </summary>
        public static readonly Regex DiscNumberRegex = new(@"\((.*?)\)", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the error count field on a disc page
        /// </summary>
        public static readonly Regex ErrorCountRegex = new(@"<tr><td><strong>Errors</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the foreign title field on a disc page
        /// </summary>
        public static readonly Regex ForeignTitleRegex = new(@"<h2 class=""foreign-title"">(.*?)</h2>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current nonce token for login
        /// </summary>
        public static readonly Regex FormTokenRegex = new(@"<input type=""hidden"" name=""form_token"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the languages field on a disc page
        /// </summary>
        public static readonly Regex LanguagesRegex = new(@"<img class=""flag-icon"" src=""/static/flags/(.*?)\.svg"" title="".*?"" alt="".*?""", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current creation time for login
        /// </summary>
        public static readonly Regex LoginCreationTimeRegex = new(@"<input type=""hidden"" name=""creation_time"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the last modified field on a disc page
        /// </summary>
        public static readonly Regex ModifiedRegex = new(@"<tr><td><strong>Modified</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching individual queue disc links on a results page
        /// </summary>
        public static readonly Regex QueueDiscRegex = new(@"<a href=""/queue/(.*?)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the region field on a disc page
        /// </summary>
        public static readonly Regex RegionRegex = new(@"<a href=""/discs\?region=(.*?)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the serial field on a disc page
        /// </summary>
        public static readonly Regex SerialRegex = new(@"<<tr><td><strong>Disc Serial</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current session ID for login
        /// </summary>
        public static readonly Regex SessionIDRegex = new(@"<input type=""hidden"" name=""sid"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the title field on a disc page
        /// </summary>
        public static readonly Regex TitleRegex = new(@"<h2>(.*?)</h2>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the version field on a disc page
        /// </summary>
        public static readonly Regex VersionRegex = new(@"<tr><td><strong>Version</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        #endregion

        #region Subpath Sets

        /// <summary>
        /// All disc page subpaths as a set
        /// </summary>
        public static readonly DiscSubpath[] AllDiscSubpaths =
        [
            DiscSubpath.Cuesheet,
            DiscSubpath.Edit,
            DiscSubpath.History,
            DiscSubpath.SBI,
            DiscSubpath.LSD,
            DiscSubpath.DMI,
        ];

        #endregion
    }
}
