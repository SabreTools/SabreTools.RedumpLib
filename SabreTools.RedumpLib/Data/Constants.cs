using System.Text.RegularExpressions;

namespace SabreTools.RedumpLib.Data
{
    public static class Constants
    {
        #region Regular Expressions

        /// <summary>
        /// Regex matching the added field on a disc page
        /// </summary>
        public static readonly Regex AddedRegex = new(@"<tr><th>Added</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the barcode field on a disc page
        /// </summary>
        public static readonly Regex BarcodeRegex = new(@"<tr><th>Barcode</th></tr><tr><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the BCA field on a disc page
        /// </summary>
        public static readonly Regex BcaRegex = new(@"<h3>BCA .*?/></h3></td><td .*?></td></tr>"
            + "<tr><th>Row</th><th>Contents</th><th>ASCII</th></tr>"
            + "<tr><td>(?<row1number>.*?)</td><td>(?<row1contents>.*?)</td><td>(?<row1ascii>.*?)</td></tr>"
            + "<tr><td>(?<row2number>.*?)</td><td>(?<row2contents>.*?)</td><td>(?<row2ascii>.*?)</td></tr>"
            + "<tr><td>(?<row3number>.*?)</td><td>(?<row3contents>.*?)</td><td>(?<row3ascii>.*?)</td></tr>"
            + "<tr><td>(?<row4number>.*?)</td><td>(?<row4contents>.*?)</td><td>(?<row4ascii>.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the category field on a disc page
        /// </summary>
        public static readonly Regex CategoryRegex = new(@"<tr><th>Category</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the comments field on a disc page
        /// </summary>
        public static readonly Regex CommentsRegex = new(@"<tr><th>Comments</th></tr><tr><td>(.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the contents field on a disc page
        /// </summary>
        public static readonly Regex ContentsRegex = new(@"<tr><th>Contents</th></tr><tr .*?><td>(.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching individual disc links on a results page
        /// </summary>
        public static readonly Regex DiscRegex = new(@"<a href=""/disc/(\d+)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the disc number or letter field on a disc page
        /// </summary>
        public static readonly Regex DiscNumberLetterRegex = new(@"\((.*?)\)", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the dumpers on a disc page
        /// </summary>
        public static readonly Regex DumpersRegex = new(@"<a href=""/discs/dumper/(.*?)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the edition field on a disc page
        /// </summary>
        public static readonly Regex EditionRegex = new(@"<tr><th>Edition</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the error count field on a disc page
        /// </summary>
        public static readonly Regex ErrorCountRegex = new(@"<tr><th>Errors count</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the foreign title field on a disc page
        /// </summary>
        public static readonly Regex ForeignTitleRegex = new(@"<h2>(.*?)</h2>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the "full match" ID list from a WIP disc page
        /// </summary>
        public static readonly Regex FullMatchRegex = new(@"<td class=""static"">full match ids: (.*?)</td>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the languages field on a disc page
        /// </summary>
        public static readonly Regex LanguagesRegex = new(@"<img src=""/images/languages/(.*?)\.png"" alt="".*?"" title="".*?"" />\s*", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the last modified field on a disc page
        /// </summary>
        public static readonly Regex LastModifiedRegex = new(@"<tr><th>Last modified</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the media field on a disc page
        /// </summary>
        public static readonly Regex MediaRegex = new(@"<tr><th>Media</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching individual WIP disc links on a results page
        /// </summary>
        public static readonly Regex NewDiscRegex = new(@"<a (style=.*)?href=""/newdisc/(\d+)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the "partial match" ID list from a WIP disc page
        /// </summary>
        public static readonly Regex PartialMatchRegex = new(@"<td class=""static"">partial match ids: (.*?)</td>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the disc key on a PS3 disc page
        /// </summary>
        public static readonly Regex PS3DiscKey = new(@"<th>Disc Key</th><th>Disc ID</th><th>Permanent Information & Control \(PIC\)</th></tr><tr><td>(.*?)</td><td>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the PVD field on a disc page
        /// </summary>
        public static readonly Regex PvdRegex = new(@"<h3>Primary Volume Descriptor (PVD) <img .*?/></h3></td><td .*?></td></tr>"
            + @"<tr><th>Record / Entry</th><th>Contents</th><th>Date</th><th>Time</th><th>GMT</th></tr>"
            + @"<tr><td>Creation</td><td>(?<creationbytes>.*?)</td><td>(?<creationdate>.*?)</td><td>(?<creationtime>.*?)</td><td>(?<creationtimezone>.*?)</td></tr>"
            + @"<tr><td>Modification</td><td>(?<modificationbytes>.*?)</td><td>(?<modificationdate>.*?)</td><td>(?<modificationtime>.*?)</td><td>(?<modificationtimezone>.*?)</td></tr>"
            + @"<tr><td>Expiration</td><td>(?<expirationbytes>.*?)</td><td>(?<expirationdate>.*?)</td><td>(?<expirationtime>.*?)</td><td>(?<expirationtimezone>.*?)</td></tr>"
            + @"<tr><td>Effective</td><td>(?<effectivebytes>.*?)</td><td>(?<effectivedate>.*?)</td><td>(?<effectivetime>.*?)</td><td>(?<effectivetimezone>.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the region field on a disc page
        /// </summary>
        public static readonly Regex RegionRegex = new(@"<tr><th>Region</th><td><a href=""/discs/region/(.*?)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching a double-layer disc ringcode information
        /// </summary>
        public static readonly Regex RingCodeDoubleRegex = new(@"", RegexOptions.Compiled | RegexOptions.Singleline); // Varies based on available fields, like Addtional Mould

        /// <summary>
        /// Regex matching a single-layer disc ringcode information
        /// </summary>
        public static readonly Regex RingCodeSingleRegex = new(@"", RegexOptions.Compiled | RegexOptions.Singleline); // Varies based on available fields, like Addtional Mould

        /// <summary>
        /// Regex matching the serial field on a disc page
        /// </summary>
        public static readonly Regex SerialRegex = new(@"<tr><th>Serial</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the system field on a disc page
        /// </summary>
        public static readonly Regex SystemRegex = new(@"<tr><th>System</th><td><a href=""/discs/system/(.*?)/"">", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the title field on a disc page
        /// </summary>
        public static readonly Regex TitleRegex = new(@"<h1>(.*?)</h1>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the current nonce token for login
        /// </summary>
        public static readonly Regex TokenRegex = new(@"<input type=""hidden"" name=""csrf_token"" value=""(.*?)"" />", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching a single track on a disc page
        /// </summary>
        public static readonly Regex TrackRegex = new(@"<tr><td>(?<number>.*?)</td><td>(?<type>.*?)</td><td>(?<pregap>.*?)</td><td>(?<length>.*?)</td><td>(?<sectors>.*?)</td><td>(?<size>.*?)</td><td>(?<crc32>.*?)</td><td>(?<md5>.*?)</td><td>(?<sha1>.*?)</td></tr>", RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Regex matching the track count on a disc page
        /// </summary>
        public static readonly Regex TrackCountRegex = new(@"<tr><th>Number of tracks</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the version field on a disc page
        /// </summary>
        public static readonly Regex VersionRegex = new(@"<tr><th>Version</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        /// <summary>
        /// Regex matching the write offset field on a disc page
        /// </summary>
        public static readonly Regex WriteOffsetRegex = new(@"<tr><th>Write offset</th><td>(.*?)</td></tr>", RegexOptions.Compiled);

        #endregion

        #region URLs

        /// <summary>
        /// Redump disc page URL template
        /// </summary>
        public const string DiscPageUrl = @"http://redump.org/disc/{0}/";

        /// <summary>
        /// Redump discs path URL template
        /// </summary>
        public const string DiscsUrl = @"http://redump.org/discs/{0}/?page={1}";

        /// <summary>
        /// Redump last modified search URL template
        /// </summary>
        public const string LastModifiedUrl = @"http://redump.org/discs/sort/modified/dir/desc?page={0}";

        /// <summary>
        /// Redump login page URL template
        /// </summary>
        public const string LoginUrl = "http://forum.redump.org/login/";

        /// <summary>
        /// Redump CUE pack URL template
        /// </summary>
        public const string PackCuesUrl = @"http://redump.org/cues/{0}/";

        /// <summary>
        /// Redump DAT pack URL template
        /// </summary>
        public const string PackDatfileUrl = @"http://redump.org/datfile/{0}/";

        /// <summary>
        /// Redump DKEYS pack URL template
        /// </summary>
        public const string PackDkeysUrl = @"http://redump.org/dkeys/{0}/";

        /// <summary>
        /// Redump GDI pack URL template
        /// </summary>
        public const string PackGdiUrl = @"http://redump.org/gdi/{0}/";

        /// <summary>
        /// Redump KEYS pack URL template
        /// </summary>
        public const string PackKeysUrl = @"http://redump.org/keys/{0}/";

        /// <summary>
        /// Redump LSD pack URL template
        /// </summary>
        public const string PackLsdUrl = @"http://redump.org/lsd/{0}/";

        /// <summary>
        /// Redump SBI pack URL template
        /// </summary>
        public const string PackSbiUrl = @"http://redump.org/sbi/{0}/";

        /// <summary>
        /// Redump quicksearch URL template
        /// </summary>
        public const string QuickSearchUrl = @"http://redump.org/discs/quicksearch/{0}/?page={1}";

        /// <summary>
        /// Redump user dumps URL template
        /// </summary>
        public const string UserDumpsUrl = @"http://redump.org/discs/dumper/{0}/?page={1}";

        /// <summary>
        /// Redump last modified user dumps URL template
        /// </summary>
        public const string UserDumpsLastModifiedUrl = @"http://redump.org/discs/sort/modified/dir/desc/dumper/{0}?page={1}";

        /// <summary>
        /// Redump WIP disc page URL template
        /// </summary>
        public const string WipDiscPageUrl = @"http://redump.org/newdisc/{0}/";

        /// <summary>
        /// Redump WIP dumps queue URL
        /// </summary>
        public const string WipDumpsUrl = @"http://redump.org/discs-wip/";

        #endregion

        #region URL Extensions

        /// <summary>
        /// Changes page subpath
        /// </summary>
        public const string ChangesExt = "changes/";

        /// <summary>
        /// Cuesheet download subpath
        /// </summary>
        public const string CueExt = "cue/";

        /// <summary>
        /// Edit page subpath
        /// </summary>
        public const string EditExt = "edit/";

        /// <summary>
        /// GDI download subpath
        /// </summary>
        public const string GdiExt = "gdi/";

        /// <summary>
        /// Key download subpath
        /// </summary>
        public const string KeyExt = "key/";

        /// <summary>
        /// LSD download subpath
        /// </summary>
        public const string LsdExt = "lsd/";

        /// <summary>
        /// MD5 download subpath
        /// </summary>
        public const string Md5Ext = "md5/";

        /// <summary>
        /// SBI download subpath
        /// </summary>
        public const string SbiExt = "sbi/";

        /// <summary>
        /// SFV download subpath
        /// </summary>
        public const string SfvExt = "sfv/";

        /// <summary>
        /// SHA1 download subpath
        /// </summary>
        public const string Sha1Ext = "sha1/";

        #endregion
    }
}
