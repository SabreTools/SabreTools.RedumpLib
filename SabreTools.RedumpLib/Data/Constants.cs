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
        /// Regex matching the barcodes field on a disc page
        /// </summary>
        public static readonly Regex BarcodesRegex = new(@"<tr><td><strong>Barcodes</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the BCA field on a disc page
        /// </summary>
        public static readonly Regex BcaRegex = new(@"<h3>BCA</h3>"
            + @"<div class=""table-scroll table-scroll-compact"">"
            + @"<table class=""table-nowrap binary-table"">"
            + "<thead><tr><th>Row</th><th>Contents</th><th>ASCII</th></tr></thead>"
            + "<tbody>"
            + "<tr><td><code>(?<row1number>.*?)</code></td><td><code>(?<row1contents>.*?)</code></td><td><code>(?<row1ascii>.*?)</code></td></tr>"
            + "<tr><td><code>(?<row2number>.*?)</code></td><td><code>(?<row2contents>.*?)</code></td><td><code>(?<row2ascii>.*?)</code></td></tr>"
            + "<tr><td><code>(?<row3number>.*?)</code></td><td><code>(?<row3contents>.*?)</code></td><td><code>(?<row3ascii>.*?)</code></td></tr>"
            + "<tr><td><code>(?<row4number>.*?)</code></td><td><code>(?<row4contents>.*?)</code></td><td><code>(?<row4ascii>.*?)</code></td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

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
        /// Regex matching the disc number or letter field on a disc page
        /// </summary>
        public static readonly Regex DiscNumberLetterRegex = new(@"\((.*?)\)", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the dumpers on a disc page
        /// </summary>
        public static readonly Regex DumpersRegex = new(@"<a href=""/discs/?dumper=(.*?)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the edition field on a disc page
        /// </summary>
        public static readonly Regex EditionRegex = new(@"<tr><td><strong>Edition</strong></td><td class=""disc-edition-cell""><span class=""disc-edition-value"">(.*?)</span></td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the error count field on a disc page
        /// </summary>
        public static readonly Regex ErrorCountRegex = new(@"<tr><td><strong>Errors</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the foreign title field on a disc page
        /// </summary>
        public static readonly Regex ForeignTitleRegex = new(@"<h2 class=""foreign-title"">(.*?)</h2>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the "full match" ID list from a WIP disc page
        /// </summary>
        /// TODO: Determine if this has a parallel in redump.info
        public static readonly Regex FullMatchRegex = new(@"<td class=""static"">full match ids: (.*?)</td>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the languages field on a disc page
        /// </summary>
        public static readonly Regex LanguagesRegex = new(@"<img class=""flag-icon"" src=""/static/flags/(.*?)\.svg"" title="".*?"" alt="".*?"" />\s*", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the last modified field on a disc page
        /// </summary>
        public static readonly Regex LastModifiedRegex = new(@"<tr><td><strong>Modified</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current creation time for login
        /// </summary>
        public static readonly Regex LoginCreationTimeRegex = new(@"<input type=""hidden"" name=""form_token"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current nonce token for login
        /// </summary>
        public static readonly Regex LoginTokenRegex = new(@"<input type=""hidden"" name=""form_token"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the media field on a disc page
        /// </summary>
        public static readonly Regex MediaRegex = new(@"<tr><td><strong>Media</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching individual WIP disc links on a results page
        /// </summary>
        /// TODO: Determine if this has a parallel in redump.info, maybe the queue page?
        public static readonly Regex NewDiscRegex = new(@"<a (style=.*)?href=""/newdisc/(\d+)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the "partial match" ID list from a WIP disc page
        /// </summary>
        /// TODO: Determine if this has a parallel in redump.info
        public static readonly Regex PartialMatchRegex = new(@"<td class=""static"">partial match ids: (.*?)</td>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the disc key on a PS3 disc page
        /// </summary>
        public static readonly Regex PS3DiscKey = new(@"<tr><td><strong>Disc Key</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the PVD field on a disc page
        /// </summary>
        public static readonly Regex PvdRegex = new("<h3>PVD</h3>"
            + @"<div class=""table-scroll table-scroll-compact"">"
            + @"<table class=""table-nowrap binary-table"">"
            + "<thead><tr><th></th><th>Contents</th><th>Date</th><th>Time</th><th>GMT</th></tr></thead>"
            + "<tbody>"
            + "<tr><td>Creation</td><td><code>(?<creationbytes>.*?)</code></td><td><code>(?<creationdate>.*?)</code></td><td><code>(?<creationtime>.*?)</code></td><td><code>(?<creationtimezone>.*?)</code></td></tr>"
            + "<tr><td>Modification</td><td><code>(?<modificationbytes>.*?)</code></td><td><code>(?<modificationdate>.*?)</code></td><td><code>(?<modificationtime>.*?)</code></td><td><code>(?<modificationtimezone>.*?)</code></td></tr>"
            + "<tr><td>Expiration</td><td><code>(?<expirationbytes>.*?)</code></td><td><code>(?<expirationdate>.*?)</code></td><td><code>(?<expirationtime>.*?)</code></td><td><code>(?<expirationtimezone>.*?)</code></td></tr>"
            + "<tr><td>Effective</td><td><code>(?<effectivebytes>.*?)</code></td><td><code>(?<effectivedate>.*?)</code></td><td><code>(?<effectivetime>.*?)</code></td><td><code>(?<effectivetimezone>.*?)</code></td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the region field on a disc page
        /// </summary>
        public static readonly Regex RegionRegex = new(@"<tr><td><strong>Region</strong></td><td class=""flags-inline flags-region"">"
            + @"<a href=""/discs/?region=(.*?)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching a double-layer disc ringcode information
        /// </summary>
        /// TODO: Determine if this has a parallel in redump.info, maybe the queue page?
        public static readonly Regex RingCodeDoubleRegex = new(@"", RegexOptions.Compiled | RegexOptions.Singleline); // Varies based on available fields, like Addtional Mould

        /// <summary>
        /// Regex matching a single-layer disc ringcode information
        /// </summary>
        /// TODO: Determine if this has a parallel in redump.info, maybe the queue page?
        public static readonly Regex RingCodeSingleRegex = new(@"", RegexOptions.Compiled | RegexOptions.Singleline); // Varies based on available fields, like Addtional Mould

        /// <summary>
        /// Regex matching the serial field on a disc page
        /// </summary>
        public static readonly Regex SerialRegex = new(@"<<tr><td><strong>Disc Serial</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the system field on a disc page
        /// </summary>
        public static readonly Regex SystemRegex = new(@"<tr><td><strong>System</strong></td><td><a href=""/discs/?system=(.*?)"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the title field on a disc page
        /// </summary>
        public static readonly Regex TitleRegex = new(@"<h2>(.*?)</h2>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching a single track on a disc page
        /// </summary>
        /// TODO: Figure out how to translate the new page to this
        public static readonly Regex TrackRegex = new(@"<tr><td>(?<number>.*?)</td><td>(?<type>.*?)</td><td>(?<pregap>.*?)</td><td>(?<length>.*?)</td><td>(?<sectors>.*?)</td><td>(?<size>.*?)</td><td>(?<crc32>.*?)</td><td>(?<md5>.*?)</td><td>(?<sha1>.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the version field on a disc page
        /// </summary>
        public static readonly Regex VersionRegex = new(@"<tr><td><strong>Version</strong></td><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the write offset field on a disc page
        /// </summary>
        /// TODO: There doesn't seem to be a write offset anymore
        public static readonly Regex WriteOffsetRegex = new(@"<tr><th>Write offset</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

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
        ];

        #endregion
    }
}
