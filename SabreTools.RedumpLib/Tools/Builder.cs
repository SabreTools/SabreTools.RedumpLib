using System;
using System.Collections.Generic;
using System.IO;
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
        /// Fill out an existing SubmissionInfo object based on a disc API query
        /// </summary>
        /// <param name="client">Client for making the connection</param>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <param name="id">Redump disc ID to retrieve</param>
        /// <param name="includeAllData">True to include all pullable information, false to only include data that should not vary by disc</param>
        public static async Task<bool> FillFromId(Client client, SubmissionInfo info, int id, bool includeAllData)
        {
            var endpoint = new Endpoints.Api
            {
                Subpath = ApiSubpath.Disc,
                Id = id,
            };
            var discResponse = await client.DownloadSingleDiscApiEndpoint(endpoint);
            if (discResponse is null)
                return false;

            // Added
            if (discResponse.AddedAt is not null)
            {
                if (DateTime.TryParse(discResponse.AddedAt, out DateTime added))
                    info.Added = added;
            }

            // Last Modified
            if (discResponse.ModifiedAt is not null)
            {
                if (DateTime.TryParse(discResponse.ModifiedAt, out DateTime lastModified))
                    info.LastModified = lastModified;
            }

            #region Disc Identity

            // System
            if (includeAllData && discResponse.System is not null)
                info.DiscIdentity.System = discResponse.System.ToPhysicalSystem();

            // Media Type
            if (includeAllData && discResponse.MediaType is not null)
                info.DiscIdentity.Media = discResponse.MediaType.ToMediaType();

            // Category
            if (discResponse.Category is not null)
                info.DiscIdentity.Category = discResponse.Category.ToDiscCategory();
            else
                info.DiscIdentity.Category = DiscCategory.Games;

            // Title
            if (discResponse.Title is not null)
                info.DiscIdentity.Title = discResponse.Title;

            // Foreign Title
            if (discResponse.TitleForeign is not null)
                info.DiscIdentity.ForeignTitle = discResponse.TitleForeign;

            // Disc Number/Letter
            if (discResponse.DiscNumber is not null)
                info.DiscIdentity.DiscNumber = discResponse.DiscNumber;

            // Disc Title
            if (discResponse.DiscTitle is not null)
                info.DiscIdentity.DiscTitle = discResponse.DiscTitle;

            // Foreign Disc Title
            if (discResponse.DiscTitleForeign is not null)
                info.DiscIdentity.ForeignDiscTitle = discResponse.DiscTitleForeign;

            // Filename Suffix
            if (includeAllData && discResponse.FilenameSuffix is not null)
                info.DiscIdentity.FilenameSuffix = discResponse.FilenameSuffix;

            #endregion

            #region Regions and Languages

            // Region
            if (discResponse.Regions is not null
                && discResponse.Regions.Length > 0)
            {
                var tempRegions = new List<RegionCode?>();
                foreach (var regionStr in discResponse.Regions)
                {
                    var region = regionStr.ToRegionCode();
                    if (region is not null)
                        tempRegions.Add(region);
                }

                info.RegionsAndLanguages.Regions = [.. tempRegions];
            }

            // Languages
            if (discResponse.Languages is not null
                && discResponse.Languages.Length > 0)
            {
                var tempLanguages = new List<LanguageCode?>();
                foreach (var languageStr in discResponse.Languages)
                {
                    var language = languageStr.ToLanguageCode();
                    if (language is not null)
                        tempLanguages.Add(language);
                }

                info.RegionsAndLanguages.Languages = [.. tempLanguages];
            }

            #endregion

            #region Disc Identifiers

            // Disc Serials
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.DiscSerials)
                && discResponse.Serial is not null
                && discResponse.Serial.Length > 0)
            {
                string serialStr = string.Join(", ", discResponse.Serial);
                info.DiscIdentifiers.DiscSerials = $"(VERIFY THIS) {serialStr}";
            }

            // Editions
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.Editions)
                && discResponse.Edition is not null
                && discResponse.Edition.Length > 0)
            {
                string editionStr = string.Join(", ", discResponse.Edition);
                info.DiscIdentifiers.Editions = $"(VERIFY THIS) {editionStr}";
            }

            // Barcodes
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.Barcodes)
                && discResponse.Barcode is not null
                && discResponse.Barcode.Length > 0)
            {
                string barcodeStr = string.Join(", ", discResponse.Barcode);
                info.DiscIdentifiers.Barcodes = $"(VERIFY THIS) {barcodeStr}";
            }

            // Version
            if (string.IsNullOrEmpty(info.DiscIdentifiers.Version)
                && discResponse.Version is not null)
            {
                info.DiscIdentifiers.Version = $"(VERIFY THIS) {discResponse.Version}";;
            }

            // Error Count
            if (string.IsNullOrEmpty(info.DiscIdentifiers.ErrorCount)
                && discResponse.ErrorCount is not null)
            {
                info.DiscIdentifiers.ErrorCount = discResponse.ErrorCount.ToString();
            }

            // EXE Date
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.EXEDate)
                && discResponse.ExeDate is not null)
            {
                info.DiscIdentifiers.EXEDate = discResponse.ExeDate;
            }

            // Internal Serial
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.InternalSerial)
                && discResponse.InternalSerial is not null)
            {
                info.DiscIdentifiers.InternalSerial = discResponse.InternalSerial;
            }

            // Volume Label
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.VolumeLabel)
                && discResponse.VolumeLabel is not null)
            {
                info.DiscIdentifiers.VolumeLabel = discResponse.VolumeLabel;
            }

            // EDC
            if (includeAllData
                && info.DiscIdentifiers.EDC is null
                && discResponse.Edc == true)
            {
                info.DiscIdentifiers.EDC = discResponse.Edc.ToYesNo();
            }

            // TODO: Figure out the best way to pull layerbreak information from the array

            // Universal Hash
            if (includeAllData
                && string.IsNullOrEmpty(info.DiscIdentifiers.UniversalHash)
                && discResponse.UniversalHash is not null)
            {
                info.DiscIdentifiers.UniversalHash = discResponse.UniversalHash;
            }

            #endregion

            #region Dump Metadata

            // TODO: Unify handling of fields that can include site codes (Comments/Contents)

            // Comments
            if (includeAllData
                && discResponse.Comments is not null)
            {
                // Process the old comments block
                string oldComments = info.DumpMetadata.Comments
                    + (string.IsNullOrEmpty(info.DumpMetadata.Comments) ? string.Empty : "\n")
                    + discResponse.Comments
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
                    foreach (SiteCode? siteCode in SiteCode.AllSiteCodes)
                    {
                        // If we have a null site code, just skip
                        if (siteCode is null)
                            continue;

                        // If the line doesn't contain this tag, just skip
                        if (siteCode.Code is not null && !commentLine.Contains(siteCode.Code))
                            continue;
                        else if (siteCode.Code is null && !commentLine.Contains(siteCode.HTML))
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
                        {
                            // Remove both possible values
                            string value = commentLine.Replace(siteCode.HTML, string.Empty).Trim();
                            if (siteCode.Code is not null)
                                value = commentLine.Replace(siteCode.Code, string.Empty).Trim();

                            info.DumpMetadata.CommentsSpecialFields[siteCode] = $"(VERIFY THIS) {value}";
                        }

                        // Otherwise, append the value to the existing key
                        else
                        {
                            // Remove both possible values
                            string value = commentLine.Replace(siteCode.HTML, string.Empty).Trim();
                            if (siteCode.Code is not null)
                                value = commentLine.Replace(siteCode.Code, string.Empty).Trim();

                            info.DumpMetadata.CommentsSpecialFields[siteCode] += $", {value}";
                        }

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

            // Contents
            if (includeAllData
                && discResponse.Contents is not null)
            {
                // Process the old contents block
                string oldContents = info.DumpMetadata.Contents
                    + (string.IsNullOrEmpty(info.DumpMetadata.Contents) ? string.Empty : "\n")
                    + discResponse.Contents
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
                    foreach (SiteCode? siteCode in SiteCode.AllSiteCodes)
                    {
                        // If we have a null site code, just skip
                        if (siteCode is null)
                            continue;

                        // If the line doesn't contain this tag, just skip
                        if (siteCode.Code is not null && !contentLine.Contains(siteCode.Code))
                            continue;
                        else if (siteCode.Code is null && !contentLine.Contains(siteCode.HTML))
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
                        if (!info.DumpMetadata.ContentsSpecialFields.ContainsKey(siteCode))
                        {
                            // Remove both possible values
                            string value = contentLine.Replace(siteCode.HTML, string.Empty).Trim();
                            if (siteCode.Code is not null)
                                value = contentLine.Replace(siteCode.Code, string.Empty).Trim();

                            info.DumpMetadata.ContentsSpecialFields[siteCode] = $"(VERIFY THIS) {value}";
                        }

                        // Otherwise, append the value to the existing key
                        else
                        {
                            // Remove both possible values
                            string value = contentLine.Replace(siteCode.HTML, string.Empty).Trim();
                            if (siteCode.Code is not null)
                                value = contentLine.Replace(siteCode.Code, string.Empty).Trim();

                            info.DumpMetadata.ContentsSpecialFields[siteCode] += $", {value}";
                        }

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

            // Protection
            if (includeAllData
                && discResponse.Protection is not null)
            {
                // If there is protection information, add to it
                if (!string.IsNullOrEmpty(info.DumpMetadata.Protection))
                    info.DumpMetadata.Protection = $"(VERIFY THIS) {discResponse.Protection}\n{info.DumpMetadata.Protection}";
                else
                    info.DumpMetadata.Protection = $"(VERIFY THIS) {discResponse.Protection}\n";
            }

            // Sector Ranges
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.SectorRanges)
                && discResponse.SectorRanges is not null
                && discResponse.SectorRanges.Length > 0)
            {
                var sectorRanges = Array.ConvertAll(discResponse.SectorRanges, r => $"{r[0]}-{r[1]}");
                info.DumpMetadata.SectorRanges = string.Join("\n", sectorRanges);
            }

            // PVD
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.PVD)
                && discResponse.Pvd is not null)
            {
                info.DumpMetadata.PVD = discResponse.Pvd;
            }

            // Header
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.Header)
                && discResponse.Header is not null)
            {
                info.DumpMetadata.Header = discResponse.Header;
            }

            // BCA
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.BCA)
                && discResponse.BcaMastering is not null
                && discResponse.BcaMastering.Length > 0)
            {
                info.DumpMetadata.BCA = string.Join("\n", discResponse.BcaMastering);
            }

            // PIC
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.PIC)
                && discResponse.Pic is not null)
            {
                info.DumpMetadata.PIC = discResponse.Pic;
            }

            // Cuesheet
            if (includeAllData
                && string.IsNullOrEmpty(info.DumpMetadata.Cuesheet)
                && discResponse.Cue is not null)
            {
                info.DumpMetadata.Cuesheet = discResponse.Cue;
            }

            // TODO: Figure out how to rebuild the DAT from the outputs
            // TODO: Figure out how to rebuild the extras DAT from the outputs

            #endregion

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
            if (!string.IsNullOrEmpty(seed.DiscIdentity.ForeignDiscTitle)) info.DiscIdentity.ForeignDiscTitle = seed.DiscIdentity.ForeignDiscTitle;

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

            foreach (SiteCode siteCode in SiteCode.AllSiteCodes)
            {
                if (siteCode.Code is not null)
                    text = text.Replace(siteCode.HTML, siteCode.Code);
            }

            // For some outdated tags, we need to use alternate names
            text = text.Replace("<b>Demos</b>:", SiteCode.PlayableDemos.Code);
            text = text.Replace("<b>Disney ID</b>:", SiteCode.DisneyInteractiveID.HTML);
            text = text.Replace("<b>LucasArts ID</b>:", SiteCode.LucasArtsID.Code);
            text = text.Replace("SS version:", SiteCode.SSVersion.HTML);
            text = text.Replace("XeMID:", SiteCode.XeMID.HTML);
            text = text.Replace("XMID:", SiteCode.XMID.HTML);

            return text;
        }

        /// <summary>
        /// Determine if a site code should be skipped on pulling
        /// </summary>
        private static bool ShouldSkipSiteCode(SiteCode? siteCode)
        {
            // Multiple
            if (siteCode == SiteCode.HighSierraVolumeDescriptor)
                return true;
            else if (siteCode == SiteCode.Multisession)
                return true;

            // Audio CD
            if (siteCode == SiteCode.RingPerfectAudioOffset)
                return true;

            // Microsoft Xbox and Xbox 360
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
    }
}
