using System;
using System.Collections.Generic;
#if NET40_OR_GREATER || NETCOREAPP || NETSTANDARD2_0_OR_GREATER
using System.Net;
#endif
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Legacy.Web;
using Constants = SabreTools.RedumpLib.Legacy.Data.Constants;
using Language = SabreTools.RedumpLib.Legacy.Data.Language;
using SubmissionInfo = SabreTools.RedumpLib.Legacy.Data.SubmissionInfo;

namespace SabreTools.RedumpLib.Legacy.Tools
{
    public static class Builder
    {
        #region Creation

        /// <summary>
        /// Fill out an existing SubmissionInfo object based on a disc page
        /// </summary>
        /// <param name="client">RedumpClient for making the connection</param>
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
                    info.CommonDiscInfo.Title = title[..firstParenLocation];
#else
                    info.CommonDiscInfo.Title = title.Substring(0, firstParenLocation);
#endif
                    var submatches = Constants.DiscNumberLetterRegex.Matches(title);
                    foreach (Match? submatch in submatches)
                    {
                        if (submatch is null)
                            continue;

                        var submatchValue = submatch.Groups[1].Value;

                        // Disc number or letter
                        if (submatchValue.StartsWith("Disc"))
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                            info.CommonDiscInfo.DiscNumberLetter = submatchValue["Disc ".Length..];
#else
                            info.CommonDiscInfo.DiscNumberLetter = submatchValue.Remove(0, "Disc ".Length);
#endif

                        // Issue number
                        else if (ulong.TryParse(submatchValue, out _))
                            info.CommonDiscInfo.Title += $" ({submatchValue})";

                        // Disc title
                        else
                            info.CommonDiscInfo.DiscTitle = submatchValue;
                    }
                }
                // Otherwise, leave the title as-is
                else
                {
                    info.CommonDiscInfo.Title = title;
                }
            }

            // Foreign Title
            match = Constants.ForeignTitleRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo.ForeignTitleNonLatin = WebUtility.HtmlDecode(match.Groups[1].Value);

            // Category
            match = Constants.CategoryRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo.Category = match.Groups[1].Value.ToDiscCategory();
            else
                info.CommonDiscInfo.Category = DiscCategory.Games;

            // Region
            if (info.CommonDiscInfo.Region is null)
            {
                match = Constants.RegionRegex.Match(discData);
                if (match.Success)
                    info.CommonDiscInfo.Region = Data.Extensions.ToRegion(match.Groups[1].Value);
            }

            // Languages
            var matches = Constants.LanguagesRegex.Matches(discData);
            if (matches.Count > 0)
            {
                var tempLanguages = new List<Language?>();
                foreach (Match? submatch in matches)
                {
                    if (submatch is null)
                        continue;

                    var language = Data.Extensions.ToLanguage(submatch.Groups[1].Value);
                    if (language is not null)
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
            if (info.VersionAndEditions.Version is null)
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
                if (info.DumpersAndStatus.Dumpers is not null && info.DumpersAndStatus.Dumpers.Length > 0)
                {
                    foreach (string dumper in info.DumpersAndStatus.Dumpers)
                        tempDumpers.Add(dumper);
                }

                foreach (Match? submatch in matches)
                {
                    if (submatch is null)
                        continue;

                    string? dumper = WebUtility.HtmlDecode(submatch.Groups[1].Value);
                    if (dumper is not null)
                        tempDumpers.Add(dumper);
                }

                info.DumpersAndStatus.Dumpers = [.. tempDumpers];
            }

            // PS3 DiscKey
            if (string.IsNullOrEmpty(info.Extras.DiscKey))
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
                            if (siteCode is null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            var shortName = siteCode.ShortName();
                            if (shortName is null || !commentLine.Contains(shortName))
                                continue;

                            // Mark as having found a tag
                            foundTag = true;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // A subset of tags can be multiline
                            addToLast = siteCode.IsMultiLine();

                            // Skip certain site codes because of data issues
                            if (ShouldSkipSiteCode(siteCode))
                                continue;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.CommonDiscInfo.CommentsSpecialFields.ContainsKey(siteCode.Value))
                                info.CommonDiscInfo.CommentsSpecialFields[siteCode.Value] = $"(VERIFY THIS) {commentLine.Replace(shortName, string.Empty).Trim()}";

                            // Otherwise, append the value to the existing key
                            else
                                info.CommonDiscInfo.CommentsSpecialFields[siteCode.Value] += $", {commentLine.Replace(shortName, string.Empty).Trim()}";

                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode is not null && !ShouldSkipSiteCode(lastSiteCode))
                            {
                                if (!string.IsNullOrEmpty(info.CommonDiscInfo.CommentsSpecialFields![lastSiteCode.Value]))
                                    info.CommonDiscInfo.CommentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.CommonDiscInfo.CommentsSpecialFields[lastSiteCode.Value] += commentLine;
                            }
                            else if (!addToLast || lastSiteCode is null)
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
                            if (siteCode is null)
                                continue;

                            // If the line doesn't contain this tag, just skip
                            var shortName = siteCode.ShortName();
                            if (shortName is null || !contentLine.Contains(shortName))
                                continue;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.CommonDiscInfo.ContentsSpecialFields.ContainsKey(siteCode.Value))
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
                            if (addToLast && lastSiteCode is not null && !ShouldSkipSiteCode(lastSiteCode))
                            {
                                if (!string.IsNullOrEmpty(info.CommonDiscInfo.ContentsSpecialFields![lastSiteCode.Value]))
                                    info.CommonDiscInfo.ContentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.CommonDiscInfo.ContentsSpecialFields[lastSiteCode.Value] += contentLine;
                            }
                            else if (!addToLast || lastSiteCode is null)
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
        /// Determine if a site code should be skipped on pulling
        /// </summary>
        private static bool ShouldSkipSiteCode(SiteCode? siteCode)
        {
#pragma warning disable IDE0072
            return siteCode switch
            {
                // Multiple
                SiteCode.HighSierraVolumeDescriptor
                    or SiteCode.InternalSerialName
                    or SiteCode.Multisession
                    or SiteCode.VolumeLabel => true,

                // Audio CD
                SiteCode.RingNonZeroDataStart
                    or SiteCode.RingPerfectAudioOffset
                    or SiteCode.UniversalHash => true,

                // Microsoft Xbox and Xbox 360
                SiteCode.DMIHash
                    or SiteCode.PFIHash
                    or SiteCode.SSHash
                    or SiteCode.SSVersion
                    or SiteCode.XMID
                    or SiteCode.XeMID => true,

                // Microsoft Xbox One and Series X/S
                SiteCode.Filename => true,
                SiteCode.TitleID => true,

                // Nintendo Gamecube
                SiteCode.InternalName => true,

                // Protection
                SiteCode.Protection => true,

                _ => false,
            };
#pragma warning restore IDE0072
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

            foreach (SiteCode? siteCode in Enum.GetValues(typeof(SiteCode)))
            {
                var longname = siteCode.LongName();
                if (!string.IsNullOrEmpty(longname))
                    text = text.Replace(longname, siteCode.ShortName());
            }

            // For some outdated tags, we need to use alternate names
            text = text.Replace("<b>Demos</b>:", ((SiteCode?)SiteCode.PlayableDemos).ShortName());
            text = text.Replace("<b>Disney ID</b>:", ((SiteCode?)SiteCode.DisneyInteractiveID).ShortName());
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
