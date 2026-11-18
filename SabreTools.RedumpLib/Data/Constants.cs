using System.Text.RegularExpressions;

namespace SabreTools.RedumpLib.Data
{
    public static class Constants
    {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.
        #region Regular Expressions

        /// <summary>
        /// Regex matching the added field on a disc page
        /// </summary>
        public static readonly Regex AddedRegex = new(@"<tr><td><strong>Added</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching individual disc links on a results page
        /// </summary>
        public static readonly Regex DiscRegex = new(@"<a href=""/disc/(\d+)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current nonce token for login
        /// </summary>
        public static readonly Regex FormTokenRegex = new(@"<input type=""hidden"" name=""form_token"" value=""(.*?)"" />", RegexOptions.Compiled);

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
        /// Regex matching the current session ID for login
        /// </summary>
        public static readonly Regex SessionIDRegex = new(@"<input type=""hidden"" name=""sid"" value=""(.*?)"" />", RegexOptions.Compiled);

        #endregion
#pragma warning restore SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

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
