using System;
using System.Collections.Generic;
using System.IO;
#if NET40_OR_GREATER || NETCOREAPP
using System.Linq;
#endif
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib
{
    public static class Builder
    {
        #region Creation

        /// <summary>
        /// Create a SubmissionInfo from a JSON file path
        /// </summary>
        /// <param name="path">Path to the SubmissionInfo JSON</param>
        /// <returns>Filled SubmissionInfo on success, null on error</returns>
        public static SubmissionInfo? CreateFromFile(string? path)
        {
            // If the path is invalid
            if (string.IsNullOrEmpty(path))
                return null;

            // If the file doesn't exist
            if (!File.Exists(path))
                return null;

            // Try to open and deserialize the file
            try
            {
                byte[] data = File.ReadAllBytes(path);
                string dataString = Encoding.UTF8.GetString(data);
                return JsonConvert.DeserializeObject<SubmissionInfo>(dataString);
            }
            catch
            {
                // We don't care what the exception was
                return null;
            }
        }

        /// <summary>
        /// Create a new SubmissionInfo object from a disc page
        /// </summary>
        /// <param name="discData">String containing the HTML disc data</param>
        /// <returns>Filled SubmissionInfo object on success, null on error</returns>
        /// <remarks>Not currently working</remarks>
        private static SubmissionInfo? CreateFromID(string discData)
        {
            var info = new SubmissionInfo()
            {
                CommonDiscInfo = new CommonDiscInfoSection(),
                VersionAndEditions = new VersionAndEditionsSection(),
            };

            // No disc data means we can't parse it
            if (string.IsNullOrEmpty(discData))
                return null;

            try
            {
                // Load the current disc page into an XML document
                var redumpPage = new XmlDocument() { PreserveWhitespace = true };
                redumpPage.LoadXml(discData);

                // If the current page isn't valid, we can't parse it
                if (!redumpPage.HasChildNodes)
                    return null;

                // Get the body node, if possible
                var bodyNode = redumpPage["html"]?["body"];
                if (bodyNode == null || !bodyNode.HasChildNodes)
                    return null;

                // Loop through and get the main node, if possible
                XmlNode? mainNode = null;
                foreach (XmlNode? tempNode in bodyNode.ChildNodes)
                {
                    // Invalid nodes are skipped
                    if (tempNode == null)
                        continue;

                    // We only care about div elements
                    if (!string.Equals(tempNode.Name, "div", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // We only care if it has attributes
                    if (tempNode.Attributes == null)
                        continue;

                    // The main node has a class of "main"
                    if (string.Equals(tempNode.Attributes["class"]?.Value, "main", StringComparison.OrdinalIgnoreCase))
                    {
                        mainNode = tempNode;
                        break;
                    }
                }

                // If the main node is invalid, we can't do anything
                if (mainNode == null || !mainNode.HasChildNodes)
                    return null;

                // Try to find elements as we're going
                foreach (XmlNode? childNode in mainNode.ChildNodes)
                {
                    // Invalid nodes are skipped
                    if (childNode == null)
                        continue;

                    // The title is the only thing in h1 tags
                    if (string.Equals(childNode.Name, "h1", StringComparison.OrdinalIgnoreCase))
                        info.CommonDiscInfo.Title = childNode.InnerText;

                    // Most things are div elements but can be hard to parse out
                    else if (!string.Equals(childNode.Name, "div", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Only 2 of the internal divs have classes attached and one is not used here
                    if (childNode.Attributes != null && string.Equals(childNode.Attributes["class"]?.Value, "game",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        // If we don't have children nodes, skip this one over
                        if (!childNode.HasChildNodes)
                            continue;

                        // The game node contains multiple other elements
                        foreach (XmlNode? gameNode in childNode.ChildNodes)
                        {
                            // Invalid nodes are skipped
                            if (gameNode == null)
                                continue;

                            // Table elements contain multiple other parts of information
                            if (string.Equals(gameNode.Name, "table", StringComparison.OrdinalIgnoreCase))
                            {
                                // All tables have some attribute we can use
                                if (gameNode.Attributes == null)
                                    continue;

                                // The gameinfo node contains most of the major information
                                if (string.Equals(gameNode.Attributes["class"]?.Value, "gameinfo",
                                        StringComparison.OrdinalIgnoreCase))
                                {
                                    // If we don't have children nodes, skip this one over
                                    if (!gameNode.HasChildNodes)
                                        continue;

                                    // Loop through each of the rows
                                    foreach (XmlNode? gameInfoNode in gameNode.ChildNodes)
                                    {
                                        // Invalid nodes are skipped
                                        if (gameInfoNode == null)
                                            continue;

                                        // If we run into anything not a row, ignore it
                                        if (!string.Equals(gameInfoNode.Name, "tr", StringComparison.OrdinalIgnoreCase))
                                            continue;

                                        // If we don't have the required nodes, ignore it
                                        if (gameInfoNode["th"] == null || gameInfoNode["td"] == null)
                                            continue;

                                        var gameInfoNodeHeader = gameInfoNode["th"];
                                        var gameInfoNodeData = gameInfoNode["td"];

                                        if (gameInfoNodeHeader == null || gameInfoNodeData == null)
                                        {
                                            // No-op for invalid data
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "System", StringComparison.OrdinalIgnoreCase))
                                        {
                                            info.CommonDiscInfo.System = Extensions.ToRedumpSystem(gameInfoNodeData["a"]?.InnerText ?? string.Empty);
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Media", StringComparison.OrdinalIgnoreCase))
                                        {
                                            info.CommonDiscInfo.Media = Extensions.ToDiscType(gameInfoNodeData.InnerText);
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Category", StringComparison.OrdinalIgnoreCase))
                                        {
                                            info.CommonDiscInfo.Category = Extensions.ToDiscCategory(gameInfoNodeData.InnerText);
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Region", StringComparison.OrdinalIgnoreCase))
                                        {
                                            // TODO: COMPLETE
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Languages", StringComparison.OrdinalIgnoreCase))
                                        {
                                            // TODO: COMPLETE
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Edition", StringComparison.OrdinalIgnoreCase))
                                        {
                                            info.VersionAndEditions.OtherEditions = gameInfoNodeData.InnerText;
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Added", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (DateTime.TryParse(gameInfoNodeData.InnerText, out DateTime added))
                                                info.Added = added;
                                        }
                                        else if (string.Equals(gameInfoNodeHeader.InnerText, "Last modified", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (DateTime.TryParse(gameInfoNodeData.InnerText, out DateTime lastModified))
                                                info.LastModified = lastModified;
                                        }
                                    }
                                }

                                // The gamecomments node contains way more than it implies
                                if (string.Equals(gameNode.Attributes["class"]?.Value, "gamecomments", StringComparison.OrdinalIgnoreCase))
                                {
                                    // TODO: COMPLETE
                                }

                                // TODO: COMPLETE
                            }

                            // The only other supported elements are divs
                            else if (!string.Equals(gameNode.Name, "div", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            // Check the div for dumper info
                            // TODO: COMPLETE
                        }
                    }

                    // Figure out what the div contains, if possible
                    // TODO: COMPLETE
                }
            }
            catch
            {
                return null;
            }

            return info;
        }

        /// <summary>
        /// Fill out an existing SubmissionInfo object based on a disc page
        /// </summary>
        /// <param name="rc">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="includeAllData">True to include all pullable information, false to do bare minimum</param>
        public async static Task<bool> FillFromId(RedumpClient rc, SubmissionInfo info, int id, bool includeAllData)
        {
            // Ensure that required sections exist
            info = EnsureAllSections(info);
            var discData = await rc.DownloadSingleSiteID(id);
            if (string.IsNullOrEmpty(discData))
                return false;

            // Title, Disc Number/Letter, Disc Title
            var match = Constants.TitleRegex.Match(discData);
            if (match.Success)
            {
                string? title = WebUtility.HtmlDecode(match.Groups[1].Value);

                // If we have parenthesis, title is everything before the first one
                int firstParenLocation = title?.IndexOf(" (") ?? -1;
                if (title != null && firstParenLocation >= 0)
                {
                    info.CommonDiscInfo!.Title = title.Substring(0, firstParenLocation);
                    var submatches = Constants.DiscNumberLetterRegex.Matches(title);
#if NET20 || NET35
                    foreach (Match submatch in submatches)
#else
                    foreach (Match submatch in submatches.Cast<Match>())
#endif
                    {
                        var submatchValue = submatch.Groups[1].Value;

                        // Disc number or letter
                        if (submatchValue.StartsWith("Disc"))
                            info.CommonDiscInfo.DiscNumberLetter = submatchValue.Remove(0, "Disc ".Length);

                        // Issue number
#if NET20 || NET35
                        else if (long.TryParse(submatchValue, out _))
#else
                        else if (submatchValue.All(c => char.IsNumber(c)))
#endif
                            info.CommonDiscInfo.Title += $" ({submatchValue})";

                        // Disc title
                        else
                            info.CommonDiscInfo.DiscTitle = submatchValue;
                    }
                }
                // Otherwise, leave the title as-is
                else
                {
                    info.CommonDiscInfo!.Title = title;
                }
            }

            // Foreign Title
            match = Constants.ForeignTitleRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo!.ForeignTitleNonLatin = WebUtility.HtmlDecode(match.Groups[1].Value);

            // Category
            match = Constants.CategoryRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo!.Category = Extensions.ToDiscCategory(match.Groups[1].Value);
            else
                info.CommonDiscInfo!.Category = DiscCategory.Games;

            // Region
            if (info.CommonDiscInfo.Region == null)
            {
                match = Constants.RegionRegex.Match(discData);
                if (match.Success)
                    info.CommonDiscInfo.Region = Extensions.ToRegion(match.Groups[1].Value);
            }

            // Languages
            var matches = Constants.LanguagesRegex.Matches(discData);
            if (matches.Count > 0)
            {
                var tempLanguages = new List<Language?>();
#if NET20 || NET35
                foreach (Match submatch in matches)
#else
                foreach (Match submatch in matches.Cast<Match>())
#endif
                {
                    var language = Extensions.ToLanguage(submatch.Groups[1].Value);
                    if (language != null)
                        tempLanguages.Add(language);
                }

                info.CommonDiscInfo.Languages = [.. tempLanguages];
            }

            // Serial
            if (includeAllData)
            {
                // TODO: Re-enable if there's a way of verifying against a disc
                //match = Constants.SerialRegex.Match(discData);
                //if (match.Success)
                //    info.CommonDiscInfo.Serial = $"(VERIFY THIS) {WebUtility.HtmlDecode(match.Groups[1].Value)}";
            }

            // Error count
            if (string.IsNullOrEmpty(info.CommonDiscInfo.ErrorsCount))
            {
                match = Constants.ErrorCountRegex.Match(discData);
                if (match.Success)
                    info.CommonDiscInfo.ErrorsCount = match.Groups[1].Value;
            }

            // Version
            if (info.VersionAndEditions!.Version == null)
            {
                match = Constants.VersionRegex.Match(discData);
                if (match.Success)
                    info.VersionAndEditions.Version = $"(VERIFY THIS) {WebUtility.HtmlDecode(match.Groups[1].Value)}";
            }

            // Dumpers
            matches = Constants.DumpersRegex.Matches(discData);
            if (matches.Count > 0)
            {
                // Start with any currently listed dumpers
                var tempDumpers = new List<string>();
                if (info.DumpersAndStatus!.Dumpers != null && info.DumpersAndStatus.Dumpers.Length > 0)
                {
                    foreach (string dumper in info.DumpersAndStatus.Dumpers)
                        tempDumpers.Add(dumper);
                }

#if NET20 || NET35
                foreach (Match submatch in matches)
#else
                foreach (Match submatch in matches.Cast<Match>())
#endif
                {
                    string? dumper = WebUtility.HtmlDecode(submatch.Groups[1].Value);
                    if (dumper != null)
                        tempDumpers.Add(dumper);
                }

                info.DumpersAndStatus.Dumpers = [.. tempDumpers];
            }

            // PS3 DiscKey
            if (string.IsNullOrEmpty(info.Extras!.DiscKey))
            {
                // Validate key is not NULL
                match = Constants.PS3DiscKey.Match(discData);
                if (match.Success && match.Groups[1].Value != "<span class=\"null\">NULL</span>")
                    info.Extras.DiscKey = match.Groups[1].Value;
            }

            // TODO: Unify handling of fields that can include site codes (Comments/Contents)

            // Comments
            if (includeAllData)
            {
                match = Constants.CommentsRegex.Match(discData);
                if (match.Success)
                {
                    // Process the old comments block
                    string oldComments = info.CommonDiscInfo.Comments
                        + (string.IsNullOrEmpty(info.CommonDiscInfo.Comments) ? string.Empty : "\n")
                        + (WebUtility.HtmlDecode(match.Groups[1].Value) ?? string.Empty)
                            .Replace("\r\n", "\n")
                            .Replace("<br />\n", "\n")
                            .Replace("<br />", string.Empty)
                            .Replace("</div>", string.Empty)
                            .Replace("[+]", string.Empty)
                            .ReplaceHtmlWithSiteCodes();
                    oldComments = Regex.Replace(oldComments, @"<div .*?>", string.Empty, RegexOptions.Compiled);

                    // Create state variables
                    bool addToLast = false;
                    SiteCode? lastSiteCode = null;
                    string newComments = string.Empty;

                    // Process the comments block line-by-line
                    string[] commentsSeparated = oldComments.Split('\n');
                    for (int i = 0; i < commentsSeparated.Length; i++)
                    {
                        string commentLine = commentsSeparated[i].Trim();

                        // If we have an empty line, we want to treat this as intentional
                        if (string.IsNullOrEmpty(commentLine))
                        {
                            addToLast = false;
                            lastSiteCode = null;
                            newComments += $"{commentLine}\n";
                            continue;
                        }

                        // Otherwise, we need to find what tag is in use
                        bool foundTag = false;
                        foreach (SiteCode? siteCode in Enum.GetValues(typeof(SiteCode)))
                        {
                            // If we have a null site code, just skip
                            if (siteCode == null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            var shortName = siteCode.ShortName();
                            if (shortName == null || !commentLine.Contains(shortName))
                                continue;

                            // Mark as having found a tag
                            foundTag = true;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // A subset of tags can be multiline
                            addToLast = siteCode.IsMultiLine();

                            // Skip certain site codes because of data issues
                            switch (siteCode)
                            {
                                // Multiple
                                case SiteCode.InternalSerialName:
                                case SiteCode.Multisession:
                                case SiteCode.VolumeLabel:
                                    continue;

                                // Audio CD
                                case SiteCode.RingNonZeroDataStart:
                                case SiteCode.UniversalHash:
                                    continue;

                                // Microsoft Xbox and Xbox 360
                                case SiteCode.DMIHash:
                                case SiteCode.PFIHash:
                                case SiteCode.SSHash:
                                case SiteCode.SSVersion:
                                case SiteCode.XMID:
                                case SiteCode.XeMID:
                                    continue;

                                // Microsoft Xbox One and Series X/S
                                case SiteCode.Filename:
                                    continue;

                                // Nintendo Gamecube
                                case SiteCode.InternalName:
                                    continue;
                            }

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.CommonDiscInfo.CommentsSpecialFields!.ContainsKey(siteCode.Value))
                                info.CommonDiscInfo.CommentsSpecialFields[siteCode.Value] = $"(VERIFY THIS) {commentLine.Replace(shortName, string.Empty).Trim()}";

                            // Otherwise, append the value to the existing key
                            else
                                info.CommonDiscInfo.CommentsSpecialFields[siteCode.Value] += $", {commentLine.Replace(shortName, string.Empty).Trim()}";

                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode != null)
                            {
                                if (!string.IsNullOrEmpty(info.CommonDiscInfo.CommentsSpecialFields![lastSiteCode.Value]))
                                    info.CommonDiscInfo.CommentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.CommonDiscInfo.CommentsSpecialFields[lastSiteCode.Value] += commentLine;
                            }
                            else
                            {
                                newComments += $"{commentLine}\n";
                            }
                        }
                    }

                    // Set the new comments field
                    info.CommonDiscInfo.Comments = newComments;
                }
            }

            // Contents
            if (includeAllData)
            {
                match = Constants.ContentsRegex.Match(discData);
                if (match.Success)
                {
                    // Process the old contents block
                    string oldContents = info.CommonDiscInfo.Contents
                        + (string.IsNullOrEmpty(info.CommonDiscInfo.Contents) ? string.Empty : "\n")
                        + (WebUtility.HtmlDecode(match.Groups[1].Value) ?? string.Empty)
                            .Replace("\r\n", "\n")
                            .Replace("<br />\n", "\n")
                            .Replace("<br />", string.Empty)
                            .Replace("</div>", string.Empty)
                            .Replace("[+]", string.Empty)
                            .ReplaceHtmlWithSiteCodes();
                    oldContents = Regex.Replace(oldContents, @"<div .*?>", string.Empty, RegexOptions.Compiled);

                    // Create state variables
                    bool addToLast = false;
                    SiteCode? lastSiteCode = null;
                    string newContents = string.Empty;

                    // Process the contents block line-by-line
                    string[] contentsSeparated = oldContents.Split('\n');
                    for (int i = 0; i < contentsSeparated.Length; i++)
                    {
                        string contentLine = contentsSeparated[i].Trim();

                        // If we have an empty line, we want to treat this as intentional
                        if (string.IsNullOrEmpty(contentLine))
                        {
                            addToLast = false;
                            lastSiteCode = null;
                            newContents += $"{contentLine}\n";
                            continue;
                        }

                        // Otherwise, we need to find what tag is in use
                        bool foundTag = false;
                        foreach (SiteCode? siteCode in Enum.GetValues(typeof(SiteCode)))
                        {
                            // If we have a null site code, just skip
                            if (siteCode == null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            var shortName = siteCode.ShortName();
                            if (shortName == null || !contentLine.Contains(shortName))
                                continue;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.CommonDiscInfo.ContentsSpecialFields!.ContainsKey(siteCode.Value))
                                info.CommonDiscInfo.ContentsSpecialFields[siteCode.Value] = $"(VERIFY THIS) {contentLine.Replace(shortName, string.Empty).Trim()}";

                            // A subset of tags can be multiline
                            addToLast = siteCode.IsMultiLine();

                            // Mark as having found a tag
                            foundTag = true;
                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode != null)
                            {
                                if (!string.IsNullOrEmpty(info.CommonDiscInfo.ContentsSpecialFields![lastSiteCode.Value]))
                                    info.CommonDiscInfo.ContentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.CommonDiscInfo.ContentsSpecialFields[lastSiteCode.Value] += contentLine;
                            }
                            else
                            {
                                newContents += $"{contentLine}\n";
                            }
                        }
                    }

                    // Set the new contents field
                    info.CommonDiscInfo.Contents = newContents;
                }
            }

            // Added
            match = Constants.AddedRegex.Match(discData);
            if (match.Success)
            {
                if (DateTime.TryParse(match.Groups[1].Value, out DateTime added))
                    info.Added = added;
                else
                    info.Added = null;
            }

            // Last Modified
            match = Constants.LastModifiedRegex.Match(discData);
            if (match.Success)
            {
                if (DateTime.TryParse(match.Groups[1].Value, out DateTime lastModified))
                    info.LastModified = lastModified;
                else
                    info.LastModified = null;
            }

            return true;
        }

        /// <summary>
        /// Ensure all required sections in a submission info exist
        /// </summary>
        /// <param name="info">SubmissionInfo object to verify</param>
        public static SubmissionInfo EnsureAllSections(SubmissionInfo? info)
        {
            // If there's no info, create one
            info ??= new SubmissionInfo();

            // Ensure all sections
            info.CommonDiscInfo ??= new CommonDiscInfoSection();
            info.VersionAndEditions ??= new VersionAndEditionsSection();
            info.EDC ??= new EDCSection();
            info.ParentCloneRelationship ??= new ParentCloneRelationshipSection();
            info.Extras ??= new ExtrasSection();
            info.CopyProtection ??= new CopyProtectionSection();
            info.DumpersAndStatus ??= new DumpersAndStatusSection();
            info.TracksAndWriteOffsets ??= new TracksAndWriteOffsetsSection();
            info.SizeAndChecksums ??= new SizeAndChecksumsSection();
            info.DumpingInfo ??= new DumpingInfoSection();

            // Ensure special dictionaries
            info.CommonDiscInfo.CommentsSpecialFields ??= [];
            info.CommonDiscInfo.ContentsSpecialFields ??= [];

            return info;
        }

        /// <summary>
        /// Inject information from a seed SubmissionInfo into the existing one
        /// </summary>
        /// <param name="info">Existing submission information</param>
        /// <param name="seed">User-supplied submission information</param>
        public static void InjectSubmissionInformation(SubmissionInfo? info, SubmissionInfo? seed)
        {
            // If we have any invalid info
            if (seed == null)
                return;

            // Ensure that required sections exist
            info = EnsureAllSections(info);

            // Otherwise, inject information as necessary
            if (info.CommonDiscInfo != null && seed.CommonDiscInfo != null)
            {
                // Info that only overwrites if supplied
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.Title)) info.CommonDiscInfo.Title = seed.CommonDiscInfo.Title;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.ForeignTitleNonLatin)) info.CommonDiscInfo.ForeignTitleNonLatin = seed.CommonDiscInfo.ForeignTitleNonLatin;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.DiscNumberLetter)) info.CommonDiscInfo.DiscNumberLetter = seed.CommonDiscInfo.DiscNumberLetter;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.DiscTitle)) info.CommonDiscInfo.DiscTitle = seed.CommonDiscInfo.DiscTitle;
                if (seed.CommonDiscInfo.Category != null) info.CommonDiscInfo.Category = seed.CommonDiscInfo.Category;
                if (seed.CommonDiscInfo.Region != null) info.CommonDiscInfo.Region = seed.CommonDiscInfo.Region;
                if (seed.CommonDiscInfo.Languages != null) info.CommonDiscInfo.Languages = seed.CommonDiscInfo.Languages;
                if (seed.CommonDiscInfo.LanguageSelection != null) info.CommonDiscInfo.LanguageSelection = seed.CommonDiscInfo.LanguageSelection;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.Serial)) info.CommonDiscInfo.Serial = seed.CommonDiscInfo.Serial;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.Barcode)) info.CommonDiscInfo.Barcode = seed.CommonDiscInfo.Barcode;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.Comments)) info.CommonDiscInfo.Comments = seed.CommonDiscInfo.Comments;
                if (seed.CommonDiscInfo.CommentsSpecialFields != null) info.CommonDiscInfo.CommentsSpecialFields = seed.CommonDiscInfo.CommentsSpecialFields;
                if (!string.IsNullOrEmpty(seed.CommonDiscInfo.Contents)) info.CommonDiscInfo.Contents = seed.CommonDiscInfo.Contents;
                if (seed.CommonDiscInfo.ContentsSpecialFields != null) info.CommonDiscInfo.ContentsSpecialFields = seed.CommonDiscInfo.ContentsSpecialFields;

                // Info that always overwrites
                info.CommonDiscInfo.Layer0MasteringRing = seed.CommonDiscInfo.Layer0MasteringRing;
                info.CommonDiscInfo.Layer0MasteringSID = seed.CommonDiscInfo.Layer0MasteringSID;
                info.CommonDiscInfo.Layer0ToolstampMasteringCode = seed.CommonDiscInfo.Layer0ToolstampMasteringCode;
                info.CommonDiscInfo.Layer0MouldSID = seed.CommonDiscInfo.Layer0MouldSID;
                info.CommonDiscInfo.Layer0AdditionalMould = seed.CommonDiscInfo.Layer0AdditionalMould;

                info.CommonDiscInfo.Layer1MasteringRing = seed.CommonDiscInfo.Layer1MasteringRing;
                info.CommonDiscInfo.Layer1MasteringSID = seed.CommonDiscInfo.Layer1MasteringSID;
                info.CommonDiscInfo.Layer1ToolstampMasteringCode = seed.CommonDiscInfo.Layer1ToolstampMasteringCode;
                info.CommonDiscInfo.Layer1MouldSID = seed.CommonDiscInfo.Layer1MouldSID;
                info.CommonDiscInfo.Layer1AdditionalMould = seed.CommonDiscInfo.Layer1AdditionalMould;

                info.CommonDiscInfo.Layer2MasteringRing = seed.CommonDiscInfo.Layer2MasteringRing;
                info.CommonDiscInfo.Layer2MasteringSID = seed.CommonDiscInfo.Layer2MasteringSID;
                info.CommonDiscInfo.Layer2ToolstampMasteringCode = seed.CommonDiscInfo.Layer2ToolstampMasteringCode;

                info.CommonDiscInfo.Layer3MasteringRing = seed.CommonDiscInfo.Layer3MasteringRing;
                info.CommonDiscInfo.Layer3MasteringSID = seed.CommonDiscInfo.Layer3MasteringSID;
                info.CommonDiscInfo.Layer3ToolstampMasteringCode = seed.CommonDiscInfo.Layer3ToolstampMasteringCode;
            }

            if (info.VersionAndEditions != null && seed.VersionAndEditions != null)
            {
                // Info that only overwrites if supplied
                if (!string.IsNullOrEmpty(seed.VersionAndEditions.Version)) info.VersionAndEditions.Version = seed.VersionAndEditions.Version;
                if (!string.IsNullOrEmpty(seed.VersionAndEditions.OtherEditions)) info.VersionAndEditions.OtherEditions = seed.VersionAndEditions.OtherEditions;
            }

            if (info.CopyProtection != null && seed.CopyProtection != null)
            {
                // Info that only overwrites if supplied
                if (!string.IsNullOrEmpty(seed.CopyProtection.Protection)) info.CopyProtection.Protection = seed.CopyProtection.Protection;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Process a text block and replace with internal identifiers
        /// </summary>
        /// <param name="text">Text block to process</param>
        /// <returns>Processed text block, if possible</returns>
        private static string ReplaceHtmlWithSiteCodes(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            foreach (SiteCode? siteCode in Enum.GetValues(typeof(SiteCode)))
            {
                var longname = siteCode.LongName();
                if (!string.IsNullOrEmpty(longname))
                    text = text.Replace(longname, siteCode.ShortName());
            }

            // For some outdated tags, we need to use alternate names
            text = text.Replace("<b>Demos</b>:", ((SiteCode?)SiteCode.PlayableDemos).ShortName());
            text = text.Replace("DMI:", ((SiteCode?)SiteCode.DMIHash).ShortName());
            text = text.Replace("<b>LucasArts ID</b>:", ((SiteCode?)SiteCode.LucasArtsID).ShortName());
            text = text.Replace("PFI:", ((SiteCode?)SiteCode.PFIHash).ShortName());
            text = text.Replace("SS:", ((SiteCode?)SiteCode.SSHash).ShortName());
            text = text.Replace("SSv1:", ((SiteCode?)SiteCode.SSHash).ShortName());
            text = text.Replace("<b>SSv1</b>:", ((SiteCode?)SiteCode.SSHash).ShortName());
            text = text.Replace("SSv2:", ((SiteCode?)SiteCode.SSHash).ShortName());
            text = text.Replace("<b>SSv2</b>:", ((SiteCode?)SiteCode.SSHash).ShortName());
            text = text.Replace("SS version:", ((SiteCode?)SiteCode.SSVersion).ShortName());
            text = text.Replace("Universal Hash (SHA-1):", ((SiteCode?)SiteCode.UniversalHash).ShortName());
            text = text.Replace("XeMID:", ((SiteCode?)SiteCode.XeMID).ShortName());
            text = text.Replace("XMID:", ((SiteCode?)SiteCode.XMID).ShortName());

            return text;
        }

        #endregion
    }
}
