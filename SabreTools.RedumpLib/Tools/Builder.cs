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
                var tempRegions = new List<Region?>();
                foreach (Match? regionMatch in regionMatches)
                {
                    if (regionMatch is null)
                        continue;

                    var region = regionMatch.Groups[1].Value.ToRegion();
                    if (region is not null)
                        tempRegions.Add(region);
                }

                info.RegionsAndLanguages.Regions = [.. tempRegions];
            }

            // Languages
            var languageMatches = Constants.LanguagesRegex.Matches(discData);
            if (languageMatches.Count > 0)
            {
                var tempLanguages = new List<Language?>();
                foreach (Match? languageMatch in languageMatches)
                {
                    if (languageMatch is null)
                        continue;

                    var language = languageMatch.Groups[1].Value.ToLanguage();
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
                            if (!info.DumpMetadata.CommentsSpecialFields.ContainsKey(siteCode.Value))
                                info.DumpMetadata.CommentsSpecialFields[siteCode.Value] = $"(VERIFY THIS) {commentLine.Replace(shortName, string.Empty).Trim()}";

                            // Otherwise, append the value to the existing key
                            else
                                info.DumpMetadata.CommentsSpecialFields[siteCode.Value] += $", {commentLine.Replace(shortName, string.Empty).Trim()}";

                            break;
                        }

                        // If we didn't find a known tag, just add the line, just in case
                        if (!foundTag)
                        {
                            if (addToLast && lastSiteCode is not null && !ShouldSkipSiteCode(lastSiteCode))
                            {
                                if (!string.IsNullOrEmpty(info.DumpMetadata.CommentsSpecialFields![lastSiteCode.Value]))
                                    info.DumpMetadata.CommentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.DumpMetadata.CommentsSpecialFields[lastSiteCode.Value] += commentLine;
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
                            var shortName = siteCode.ShortName();
                            if (shortName is null || !contentLine.Contains(shortName))
                                continue;

                            // Cache the current site code
                            lastSiteCode = siteCode;

                            // If we don't already have this site code, add it to the dictionary
                            if (!info.DumpMetadata.ContentsSpecialFields.ContainsKey(siteCode.Value))
                                info.DumpMetadata.ContentsSpecialFields[siteCode.Value] = $"(VERIFY THIS) {contentLine.Replace(shortName, string.Empty).Trim()}";

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
                                if (!string.IsNullOrEmpty(info.DumpMetadata.ContentsSpecialFields![lastSiteCode.Value]))
                                    info.DumpMetadata.ContentsSpecialFields[lastSiteCode.Value] += "\n";

                                info.DumpMetadata.ContentsSpecialFields[lastSiteCode.Value] += contentLine;
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
        /// Fill out an existing SubmissionInfo object based on a disc page
        /// </summary>
        /// <param name="client">RedumpClient for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="includeAllData">True to include all pullable information, false to do bare minimum</param>
        public static async Task<bool> FillFromId(RedumpOrg.Client client, RedumpOrg.SubmissionInfo info, int id, bool includeAllData)
        {
            var discData = await client.DownloadSingleDiscPage(id);
            if (string.IsNullOrEmpty(discData))
                return false;

            // Title, Disc Number/Letter, Disc Title
            var match = RedumpOrg.Constants.TitleRegex.Match(discData);
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
                    var submatches = RedumpOrg.Constants.DiscNumberLetterRegex.Matches(title);
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
            match = RedumpOrg.Constants.ForeignTitleRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo.ForeignTitleNonLatin = WebUtility.HtmlDecode(match.Groups[1].Value);

            // Category
            match = RedumpOrg.Constants.CategoryRegex.Match(discData);
            if (match.Success)
                info.CommonDiscInfo.Category = match.Groups[1].Value.ToDiscCategory();
            else
                info.CommonDiscInfo.Category = DiscCategory.Games;

            // Region
            if (info.CommonDiscInfo.Region is null)
            {
                match = RedumpOrg.Constants.RegionRegex.Match(discData);
                if (match.Success)
                    info.CommonDiscInfo.Region = RedumpOrg.Extensions.ToRegion(match.Groups[1].Value);
            }

            // Languages
            var matches = RedumpOrg.Constants.LanguagesRegex.Matches(discData);
            if (matches.Count > 0)
            {
                var tempLanguages = new List<RedumpOrg.Language?>();
                foreach (Match? submatch in matches)
                {
                    if (submatch is null)
                        continue;

                    var language = RedumpOrg.Extensions.ToLanguage(submatch.Groups[1].Value);
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
                match = RedumpOrg.Constants.ErrorCountRegex.Match(discData);
                if (match.Success)
                    info.CommonDiscInfo.ErrorsCount = match.Groups[1].Value;
            }

            // Version
            if (info.VersionAndEditions.Version is null)
            {
                match = RedumpOrg.Constants.VersionRegex.Match(discData);
                if (match.Success)
                    info.VersionAndEditions.Version = $"(VERIFY THIS) {WebUtility.HtmlDecode(match.Groups[1].Value)}";
            }

            // Dumpers
            matches = RedumpOrg.Constants.DumpersRegex.Matches(discData);
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
                match = RedumpOrg.Constants.PS3DiscKey.Match(discData);
                if (match.Success && match.Groups[1].Value != "<span class=\"null\">NULL</span>")
                    info.Extras.DiscKey = match.Groups[1].Value;
            }

            // TODO: Unify handling of fields that can include site codes (Comments/Contents)

            // Comments
            if (includeAllData)
            {
                match = RedumpOrg.Constants.CommentsRegex.Match(discData);
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
                match = RedumpOrg.Constants.ContentsRegex.Match(discData);
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
            match = RedumpOrg.Constants.AddedRegex.Match(discData);
            if (match.Success)
            {
                if (DateTime.TryParse(match.Groups[1].Value, out DateTime added))
                    info.Added = added;
                else
                    info.Added = null;
            }

            // Last Modified
            match = RedumpOrg.Constants.LastModifiedRegex.Match(discData);
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
            info.RingCodes.Layer1MouldSIDs = seed.RingCodes.Layer1MouldSIDs;
            info.RingCodes.Layer1AdditionalMoulds = seed.RingCodes.Layer1AdditionalMoulds;

            info.RingCodes.Layer2MasteringCode = seed.RingCodes.Layer2MasteringCode;
            info.RingCodes.Layer2MasteringSID = seed.RingCodes.Layer2MasteringSID;
            info.RingCodes.Layer2Toolstamps = seed.RingCodes.Layer2Toolstamps;

            info.RingCodes.Layer3MasteringCode = seed.RingCodes.Layer3MasteringCode;
            info.RingCodes.Layer3MasteringSID = seed.RingCodes.Layer3MasteringSID;
            info.RingCodes.Layer3Toolstamps = seed.RingCodes.Layer3Toolstamps;

            return info;
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
