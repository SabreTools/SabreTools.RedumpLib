using System;
using System.Collections.Generic;
using System.IO;
#if NET40_OR_GREATER || NETCOREAPP || NETSTANDARD2_0_OR_GREATER
using System.Net;
#endif
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace SabreTools.RedumpLib.Tools
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
        /// Fill out an existing SubmissionInfo object based on a disc page
        /// </summary>
        /// <param name="client">Client for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="includeAllData">True to include all pullable information, false to do bare minimum</param>
        public static async Task<bool> FillFromId(Client client, SubmissionInfo info, int id, bool includeAllData)
        {
            var discData = await client.DownloadSingleDiscPage(id);
            if (string.IsNullOrEmpty(discData))
                return false;

            // Title, Disc Number/Letter, Disc Title
            var match = Constants.TitleRegex.Match(discData);
            if (match.Success)
            {
                string? title = WebUtility.HtmlDecode(match.Groups[1].Value);

                // If we have parenthesis, title is everything before the first one
                int firstParenLocation = title?.IndexOf(" (") ?? -1;
                if (title is not null && firstParenLocation >= 0)
                {
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                    info.DiscIdentity.Title = title[..firstParenLocation];
#else
                    info.DiscIdentity.Title = title.Substring(0, firstParenLocation);
#endif
                    var submatches = Constants.DiscNumberRegex.Matches(title);
                    foreach (Match? submatch in submatches)
                    {
                        if (submatch is null)
                            continue;

                        var submatchValue = submatch.Groups[1].Value;

                        // Disc number or letter
                        if (submatchValue.StartsWith("Disc"))
                        {
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                            info.DiscIdentity.DiscNumber = submatchValue["Disc ".Length..];
#else
                            info.DiscIdentity.DiscNumber = submatchValue.Remove(0, "Disc ".Length);
#endif
                        }

                        // TODO: Figure out how to determine disc title vs. version (e.g. Alt)
                        // At the moment, if the original splitting logic is used, then things like
                        // "(Alt)" would be marked as the disc title instead of it properly going into the
                        // filename suffix. There may not be a reasonable way of splitting this, so
                        // only the disc number is being populated for now.
                    }
                }
                // Otherwise, leave the title as-is
                else
                {
                    info.DiscIdentity.Title = title;
                }
            }

            // Foreign Title
            match = Constants.ForeignTitleRegex.Match(discData);
            if (match.Success)
                info.DiscIdentity.ForeignTitle = WebUtility.HtmlDecode(match.Groups[1].Value);

            // Category
            match = Constants.CategoryRegex.Match(discData);
            if (match.Success)
                info.DiscIdentity.Category = match.Groups[1].Value.ToDiscCategory();
            else
                info.DiscIdentity.Category = DiscCategory.Games;

            // Region
            var regionMatches = Constants.RegionRegex.Matches(discData);
            if (regionMatches.Count > 0)
            {
                var tempRegions = new List<RegionCode?>();
                foreach (Match? regionMatch in regionMatches)
                {
                    if (regionMatch is null)
                        continue;

                    var region = regionMatch.Groups[1].Value.ToRegionCode();
                    if (region is not null)
                        tempRegions.Add(region);
                }

                info.RegionsAndLanguages.Regions = [.. tempRegions];
            }

            // Languages
            var languageMatches = Constants.LanguagesRegex.Matches(discData);
            if (languageMatches.Count > 0)
            {
                var tempLanguages = new List<LanguageCode?>();
                foreach (Match? languageMatch in languageMatches)
                {
                    if (languageMatch is null)
                        continue;

                    var language = languageMatch.Groups[1].Value.ToLanguageCode();
                    if (language is not null)
                        tempLanguages.Add(language);
                }

                info.RegionsAndLanguages.Languages = [.. tempLanguages];
            }

            // Disc Serials
            if (includeAllData && string.IsNullOrEmpty(info.DiscIdentifiers.DiscSerials))
            {
                match = Constants.SerialRegex.Match(discData);
                if (match.Success)
                    info.DiscIdentifiers.DiscSerials = $"(VERIFY THIS) {WebUtility.HtmlDecode(match.Groups[1].Value)}";
            }

            // Error Count
            if (string.IsNullOrEmpty(info.DiscIdentifiers.ErrorCount))
            {
                match = Constants.ErrorCountRegex.Match(discData);
                if (match.Success)
                    info.DiscIdentifiers.ErrorCount = match.Groups[1].Value;
            }

            // Version
            if (string.IsNullOrEmpty(info.DiscIdentifiers.Version))
            {
                match = Constants.VersionRegex.Match(discData);
                if (match.Success)
                    info.DiscIdentifiers.Version = $"(VERIFY THIS) {WebUtility.HtmlDecode(match.Groups[1].Value)}";
            }

            // Disc Key
            if (string.IsNullOrEmpty(info.DiscIdentifiers.DiscKey))
            {
                // Validate key is not NULL
                match = Constants.DiscKeyRegex.Match(discData);
                if (match.Success && match.Groups[1].Value != "<span class=\"null\">NULL</span>")
                    info.DiscIdentifiers.DiscKey = match.Groups[1].Value;
            }

            // TODO: Unify handling of fields that can include site codes (Comments/Contents)

            // Comments
            if (includeAllData)
            {
                match = Constants.CommentsRegex.Match(discData);
                if (match.Success)
                {
                    // Process the old comments block
                    string oldComments = info.DumpMetadata.Comments
                        + (string.IsNullOrEmpty(info.DumpMetadata.Comments) ? string.Empty : "\n")
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
                            if (siteCode is null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            if (siteCode.Code is null || !commentLine.Contains(siteCode.Code))
                                continue;

                            // Mark as having found a tag
                            foundTag = true;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // A subset of tags can be multiline
                            addToLast = siteCode.IsMultiLine;

                            // Skip certain site codes because of data issues
                            if (ShouldSkipSiteCode(siteCode))
                                continue;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.DumpMetadata.CommentsSpecialFields.ContainsKey(siteCode))
                                info.DumpMetadata.CommentsSpecialFields[siteCode] = $"(VERIFY THIS) {commentLine.Replace(siteCode.Code, string.Empty).Trim()}";

                            // Otherwise, append the value to the existing key
                            else
                                info.DumpMetadata.CommentsSpecialFields[siteCode] += $", {commentLine.Replace(siteCode.Code, string.Empty).Trim()}";

                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode is not null && !ShouldSkipSiteCode(lastSiteCode))
                            {
                                if (!string.IsNullOrEmpty(info.DumpMetadata.CommentsSpecialFields![lastSiteCode]))
                                    info.DumpMetadata.CommentsSpecialFields[lastSiteCode] += "\n";

                                info.DumpMetadata.CommentsSpecialFields[lastSiteCode] += commentLine;
                            }
                            else if (!addToLast || lastSiteCode is null)
                            {
                                newComments += $"{commentLine}\n";
                            }
                        }
                    }

                    // Set the new comments field
                    info.DumpMetadata.Comments = newComments;
                }
            }

            // Contents
            if (includeAllData)
            {
                match = Constants.ContentsRegex.Match(discData);
                if (match.Success)
                {
                    // Process the old contents block
                    string oldContents = info.DumpMetadata.Contents
                        + (string.IsNullOrEmpty(info.DumpMetadata.Contents) ? string.Empty : "\n")
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
                            if (siteCode is null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            if (siteCode.Code is null || !contentLine.Contains(siteCode.Code))
                                continue;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.DumpMetadata.ContentsSpecialFields.ContainsKey(siteCode))
                                info.DumpMetadata.ContentsSpecialFields[siteCode] = $"(VERIFY THIS) {contentLine.Replace(siteCode.Code, string.Empty).Trim()}";

                            // A subset of tags can be multiline
                            addToLast = siteCode.IsMultiLine;

                            // Mark as having found a tag
                            foundTag = true;
                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode is not null && !ShouldSkipSiteCode(lastSiteCode))
                            {
                                if (!string.IsNullOrEmpty(info.DumpMetadata.ContentsSpecialFields![lastSiteCode]))
                                    info.DumpMetadata.ContentsSpecialFields[lastSiteCode] += "\n";

                                info.DumpMetadata.ContentsSpecialFields[lastSiteCode] += contentLine;
                            }
                            else if (!addToLast || lastSiteCode is null)
                            {
                                newContents += $"{contentLine}\n";
                            }
                        }
                    }

                    // Set the new contents field
                    info.DumpMetadata.Contents = newContents;
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
            match = Constants.ModifiedRegex.Match(discData);
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
        /// Inject information from a seed SubmissionInfo into the existing one
        /// </summary>
        /// <param name="info">Existing submission information</param>
        /// <param name="seed">User-supplied submission information</param>
        public static SubmissionInfo? InjectSubmissionInformation(SubmissionInfo? info, SubmissionInfo? seed)
        {
            // If we have any invalid info
            if (seed is null)
                return info;

            // Otherwise, inject information as necessary
            info ??= new SubmissionInfo();

            // Info that only overwrites if supplied
            if (seed.DiscIdentity.Category is not null) info.DiscIdentity.Category = seed.DiscIdentity.Category;
            if (!string.IsNullOrEmpty(seed.DiscIdentity.Title)) info.DiscIdentity.Title = seed.DiscIdentity.Title;
            if (!string.IsNullOrEmpty(seed.DiscIdentity.ForeignTitle)) info.DiscIdentity.ForeignTitle = seed.DiscIdentity.ForeignTitle;
            if (!string.IsNullOrEmpty(seed.DiscIdentity.DiscNumber)) info.DiscIdentity.DiscNumber = seed.DiscIdentity.DiscNumber;
            if (!string.IsNullOrEmpty(seed.DiscIdentity.DiscTitle)) info.DiscIdentity.DiscTitle = seed.DiscIdentity.DiscTitle;

            if (seed.RegionsAndLanguages.Regions is not null) info.RegionsAndLanguages.Regions = seed.RegionsAndLanguages.Regions;
            if (seed.RegionsAndLanguages.Languages is not null) info.RegionsAndLanguages.Languages = seed.RegionsAndLanguages.Languages;

            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.DiscSerials)) info.DiscIdentifiers.DiscSerials = seed.DiscIdentifiers.DiscSerials;
            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.Editions)) info.DiscIdentifiers.Editions = seed.DiscIdentifiers.Editions;
            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.Barcodes)) info.DiscIdentifiers.Barcodes = seed.DiscIdentifiers.Barcodes;
            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.Version)) info.DiscIdentifiers.Version = seed.DiscIdentifiers.Version;
            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.DiscID)) info.DiscIdentifiers.DiscID = seed.DiscIdentifiers.DiscID;
            if (!string.IsNullOrEmpty(seed.DiscIdentifiers.DiscKey)) info.DiscIdentifiers.DiscKey = seed.DiscIdentifiers.DiscKey;

            if (!string.IsNullOrEmpty(seed.DumpMetadata.Comments)) info.DumpMetadata.Comments = seed.DumpMetadata.Comments;
            if (seed.DumpMetadata.CommentsSpecialFields.Count > 0) info.DumpMetadata.CommentsSpecialFields = seed.DumpMetadata.CommentsSpecialFields;
            if (!string.IsNullOrEmpty(seed.DumpMetadata.Contents)) info.DumpMetadata.Contents = seed.DumpMetadata.Contents;
            if (seed.DumpMetadata.ContentsSpecialFields.Count > 0) info.DumpMetadata.ContentsSpecialFields = seed.DumpMetadata.ContentsSpecialFields;

            if (!string.IsNullOrEmpty(seed.SubmissionControls.LogsArchiveURL)) info.SubmissionControls.LogsArchiveURL = seed.SubmissionControls.LogsArchiveURL;
            if (!string.IsNullOrEmpty(seed.SubmissionControls.ReviewComment)) info.SubmissionControls.ReviewComment = seed.SubmissionControls.ReviewComment;
            if (!string.IsNullOrEmpty(seed.SubmissionControls.SubmissionComment)) info.SubmissionControls.SubmissionComment = seed.SubmissionControls.SubmissionComment;

            // Info that always overwrites
            info.RingCodes.Layer0MasteringCode = seed.RingCodes.Layer0MasteringCode;
            info.RingCodes.Layer0MasteringSID = seed.RingCodes.Layer0MasteringSID;
            info.RingCodes.Layer0Toolstamps = seed.RingCodes.Layer0Toolstamps;
            info.RingCodes.Layer0MouldSIDs = seed.RingCodes.Layer0MouldSIDs;
            info.RingCodes.Layer0AdditionalMoulds = seed.RingCodes.Layer0AdditionalMoulds;

            info.RingCodes.Layer1MasteringCode = seed.RingCodes.Layer1MasteringCode;
            info.RingCodes.Layer1MasteringSID = seed.RingCodes.Layer1MasteringSID;
            info.RingCodes.Layer1Toolstamps = seed.RingCodes.Layer1Toolstamps;

            info.RingCodes.Layer2MasteringCode = seed.RingCodes.Layer2MasteringCode;
            info.RingCodes.Layer2MasteringSID = seed.RingCodes.Layer2MasteringSID;
            info.RingCodes.Layer2Toolstamps = seed.RingCodes.Layer2Toolstamps;

            info.RingCodes.Layer3MasteringCode = seed.RingCodes.Layer3MasteringCode;
            info.RingCodes.Layer3MasteringSID = seed.RingCodes.Layer3MasteringSID;
            info.RingCodes.Layer3Toolstamps = seed.RingCodes.Layer3Toolstamps;

            info.RingCodes.LabelSideMasteringCode = seed.RingCodes.LabelSideMasteringCode;
            info.RingCodes.LabelSideMasteringSID = seed.RingCodes.LabelSideMasteringSID;
            info.RingCodes.LabelSideToolstamps = seed.RingCodes.LabelSideToolstamps;
            info.RingCodes.LabelSideMouldSIDs = seed.RingCodes.LabelSideMouldSIDs;
            info.RingCodes.LabelSideAdditionalMoulds = seed.RingCodes.LabelSideAdditionalMoulds;

            return info;
        }

        /// <summary>
        /// Determine if a site code should be skipped on pulling
        /// </summary>
        private static bool ShouldSkipSiteCode(SiteCode? siteCode)
        {
            // Multiple
            if (siteCode == SiteCode.HighSierraVolumeDescriptor)
                return true;
            else if (siteCode == SiteCode.InternalSerialName)
                return true;
            else if (siteCode == SiteCode.Multisession)
                return true;
            else if (siteCode == SiteCode.VolumeLabel)
                return true;

            // Audio CD
            if (siteCode == SiteCode.RingPerfectAudioOffset)
                return true;

            // Microsoft Xbox and Xbox 360
            if (siteCode == SiteCode.DMIHash)
                return true;
            else if (siteCode == SiteCode.PFIHash)
                return true;
            else if (siteCode == SiteCode.SSHash)
                return true;
            else if (siteCode == SiteCode.SSVersion)
                return true;
            else if (siteCode == SiteCode.XMID)
                return true;
            else if (siteCode == SiteCode.XeMID)
                return true;

            // Microsoft Xbox One and Series X/S
            if (siteCode == SiteCode.Filename)
                return true;
            else if (siteCode == SiteCode.TitleID)
                return true;

            // Nintendo Gamecube
            if (siteCode == SiteCode.InternalName)
                return true;

            // Protection
            if (siteCode == SiteCode.Protection)
                return true;

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Process a text block and replace with internal identifiers
        /// </summary>
        /// <param name="text">Text block to process</param>
        /// <returns>Processed text block, if possible</returns>
        internal static string ReplaceHtmlWithSiteCodes(this string text)
        {
            // Empty strings are ignored
            if (text.Length == 0)
                return text;

            foreach (SiteCode? siteCode in SiteCode.AllSiteCodes)
            {
                if (!string.IsNullOrEmpty(siteCode.HTML))
                    text = text.Replace(siteCode.HTML, siteCode.Code);
            }

            // For some outdated tags, we need to use alternate names
            text = text.Replace("<b>Demos</b>:", SiteCode.PlayableDemos.Code);
            text = text.Replace("<b>Disney ID</b>:", SiteCode.DisneyInteractiveID.Code);
            text = text.Replace("DMI:", SiteCode.DMIHash.Code);
            text = text.Replace("<b>LucasArts ID</b>:", SiteCode.LucasArtsID.Code);
            text = text.Replace("PFI:", SiteCode.PFIHash.Code);
            text = text.Replace("SS:", SiteCode.SSHash.Code);
            text = text.Replace("SSv1:", SiteCode.SSHash.Code);
            text = text.Replace("<b>SSv1</b>:", SiteCode.SSHash.Code);
            text = text.Replace("SSv2:", SiteCode.SSHash.Code);
            text = text.Replace("<b>SSv2</b>:", SiteCode.SSHash.Code);
            text = text.Replace("SS version:", SiteCode.SSVersion.Code);
            text = text.Replace("XeMID:", SiteCode.XeMID.Code);
            text = text.Replace("XMID:", SiteCode.XMID.Code);

            return text;
        }

        #endregion
    }
}
