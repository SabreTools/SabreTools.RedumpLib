using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;

namespace SabreTools.RedumpLib.Tools
{
    public static class Formatter
    {
        /// <summary>
        /// Ordered set of comment codes for output
        /// </summary>
        internal static readonly SiteCode[] OrderedCommentCodes =
        [
            // Identifying Info
            SiteCode.AlternativeTitle,
            SiteCode.AlternativeForeignTitle,
            SiteCode.DiscTitleNonLatin,
            SiteCode.EditionNonLatin,
            SiteCode.InternalName,
            SiteCode.InternalSerialName,
            SiteCode.VolumeLabel,
            SiteCode.HighSierraVolumeDescriptor,
            SiteCode.Multisession,
            SiteCode.UniversalHash,
            SiteCode.RingNonZeroDataStart,
            SiteCode.RingPerfectAudioOffset,

            SiteCode.XMID,
            SiteCode.XeMID,
            SiteCode.DMIHash,
            SiteCode.PFIHash,
            SiteCode.SSHash,
            SiteCode.SSVersion,

            SiteCode.Filename,
            SiteCode.TitleID,

            SiteCode.Protection,

            SiteCode.BBFCRegistrationNumber,
            SiteCode.DiscHologramID,
            SiteCode.DNASDiscID,
            SiteCode.DiscID,
            SiteCode.CoverID,
            SiteCode.ISBN,
            SiteCode.ISSN,
            SiteCode.PPN,
            SiteCode.VFCCode,

            SiteCode.CompatibleOS,
            SiteCode.AdditionalBCAData,

            // Legacy Codes
            SiteCode.Genre,
            SiteCode.Series,
            SiteCode.PostgapType,
            SiteCode.VCD,

            // Publisher / Company IDs
            SiteCode.TwoKGamesID,
            SiteCode.AcclaimID,
            SiteCode.AccoladeID,
            SiteCode.ActivisionID,
            SiteCode.BandaiID,
            SiteCode.BethesdaID,
            SiteCode.CDProjektID,
            SiteCode.DisneyInteractiveID,
            SiteCode.EidosID,
            SiteCode.ElectronicArtsID,
            SiteCode.FoxInteractiveID,
            SiteCode.GTInteractiveID,
            SiteCode.InterplayID,
            SiteCode.JASRACID,
            SiteCode.KingRecordsID,
            SiteCode.KoeiID,
            SiteCode.KonamiID,
            SiteCode.LucasArtsID,
            SiteCode.MicrosoftID,
            SiteCode.NaganoID,
            SiteCode.NamcoID,
            SiteCode.NipponIchiSoftwareID,
            SiteCode.OriginID,
            SiteCode.PonyCanyonID,
            SiteCode.SegaID,
            SiteCode.SelenID,
            SiteCode.SierraID,
            SiteCode.SteamAppID,
            SiteCode.TaitoID,
            SiteCode.UbisoftID,
            SiteCode.ValveID,

            // Standardized Comments
            SiteCode.PCMacHybrid,
        ];

        /// <summary>
        /// Ordered set of content codes for output
        /// </summary>
        internal static readonly SiteCode[] OrderedContentCodes =
        [
            // Applications
            SiteCode.Applications,

            // Games
            SiteCode.Games,
            SiteCode.NetYarozeGames,
            SiteCode.SteamSimSidDepotID,
            SiteCode.SteamCsmCsdDepotID,

            // Demos
            SiteCode.PlayableDemos,
            SiteCode.RollingDemos,
            SiteCode.TechDemos,

            // Video
            SiteCode.GameFootage,
            SiteCode.Videos,

            // Miscellaneous
            SiteCode.Patches,
            SiteCode.Savegames,
            SiteCode.Extras,
        ];

        /// <summary>
        /// Format the output data in a human readable way
        /// </summary>
        /// <param name="info">Information object that should contain normalized values</param>
        /// <returns>String representing each line of an output file, null on error</returns>
        public static string? FormatOutputData(SubmissionInfo? info, out string? status)
        {
            // Check to see if the inputs are valid
            if (info is null)
            {
                status = "Submission information was missing";
                return null;
            }

            try
            {
                // Create the string builder for output
                var output = new StringBuilder();

                // Preamble for submission
                output.AppendLine("Users who wish to submit this information to Redump must ensure that all of the fields below are accurate for the exact media they have.");
                output.AppendLine("Please double-check to ensure that there are no fields that need verification, such as the version or copy protection.");
                output.AppendLine("If there are no fields in need of verification or all fields are accurate, this preamble can be removed before submission.");
                output.AppendLine();

                // Disc Identity section
                FormatOutputData(output,
                    info.DiscIdentity,
                    info.DiscIdentifiers,
                    info.DumpMetadata,
                    info.FullyMatchedIDs,
                    info.PartiallyMatchedIDs);
                output.AppendLine();

                // Regions and Languages section
                FormatOutputData(output, info.RegionsAndLanguages);
                output.AppendLine();

                // Disc Identifiers section
                FormatOutputData(output, info.DiscIdentifiers);
                output.AppendLine();

                // Ring Codes section
                FormatOutputData(output,
                    info.RingCodes,
                    info.DiscIdentifiers,
                    info.DiscIdentity.System);
                output.AppendLine();

                // Dump Metadata section
                FormatOutputData(output, info.DumpMetadata);
                output.AppendLine();

                // Submission Controls section
                FormatOutputData(output, info.SubmissionControls);
                output.AppendLine();

                // Dumping Info section
                FormatOutputData(output, info.DumpingInfo);

                status = "Formatting complete!";

                // Make sure there aren't any instances of two blank lines in a row
                return RemoveConsecutiveEmptyLines(output.ToString());
            }
            catch (Exception ex)
            {
                status = $"Error formatting submission info: {ex}";
                return null;
            }
        }

        /// <summary>
        /// Process any fields that have to be combined
        /// </summary>
        /// <param name="info">Information object to normalize</param>
        public static void ProcessSpecialFields(SubmissionInfo info)
        {
            // Process the comments field
            if (info.DumpMetadata.CommentsSpecialFields is not null && info.DumpMetadata.CommentsSpecialFields.Count > 0)
            {
                // If the field is missing, add an empty one to fill in
                info.DumpMetadata.Comments ??= string.Empty;

                // Add all special fields before any comments
                var orderedTags = OrderCommentTags(info.DumpMetadata.CommentsSpecialFields);
                var formattedTags = Array.ConvertAll(orderedTags, kvp => FormatSiteTag(kvp.Key, kvp.Value));
                info.DumpMetadata.Comments = string.Join("\n", formattedTags) + "\n" + info.DumpMetadata.Comments;

                // Normalize the assembled string
                info.DumpMetadata.Comments = info.DumpMetadata.Comments.Replace("\r\n", "\n");
                info.DumpMetadata.Comments = info.DumpMetadata.Comments.Replace("\n\n", "\n");
                info.DumpMetadata.Comments = info.DumpMetadata.Comments.Trim();

                // Wipe out the special fields dictionary
                info.DumpMetadata.CommentsSpecialFields.Clear();
            }

            // Process the contents field
            if (info.DumpMetadata.ContentsSpecialFields is not null && info.DumpMetadata.ContentsSpecialFields.Count > 0)
            {
                // If the field is missing, add an empty one to fill in
                info.DumpMetadata.Contents ??= string.Empty;

                // Add all special fields before any contents
                var orderedTags = OrderContentTags(info.DumpMetadata.ContentsSpecialFields);
                var formattedTags = Array.ConvertAll(orderedTags, kvp => FormatSiteTag(kvp.Key, kvp.Value));
                info.DumpMetadata.Contents = string.Join("\n", formattedTags) + "\n" + info.DumpMetadata.Contents;

                // Normalize the assembled string
                info.DumpMetadata.Contents = info.DumpMetadata.Contents.Replace("\r\n", "\n");
                info.DumpMetadata.Contents = info.DumpMetadata.Contents.Replace("\n\n", "\n");
                info.DumpMetadata.Contents = info.DumpMetadata.Contents.Trim();

                // Wipe out the special fields dictionary
                info.DumpMetadata.ContentsSpecialFields.Clear();
            }
        }

        /// <summary>
        /// Format a DiscIdentitySection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output,
            DiscIdentitySection section,
            DiscIdentifiersSection discIdentifiers,
            DumpMetadataSection dumpMetadata,
            List<int>? fullyMatchedIDs,
            List<int>? partiallyMatchedIDs)
        {
            // Extract the size from the hashes
            long size = Extensions.ExtractSizeFromHashData(dumpMetadata.Dat);

            output.AppendLine("Disc Identity:");

            AddIfExists(output, Template.SystemField, section.System.LongName(), 1);
            AddIfExists(output, Template.MediaTypeField, GetFixedMediaType(
                    section.Media.ToPhysicalMediaType(),
                    dumpMetadata?.PIC,
                    size,
                    discIdentifiers?.Layerbreak,
                    discIdentifiers?.Layerbreak2,
                    discIdentifiers?.Layerbreak3),
                1);
            AddIfExists(output, Template.CategoryField, section.Category.LongName(), 1);
            AddIfExists(output, Template.TitleField, section.Title, 1);
            AddIfExists(output, Template.ForeignTitleField, section.ForeignTitle, 1);
            AddIfExists(output, Template.DiscNumberField, section.DiscNumber, 1);
            AddIfExists(output, Template.DiscTitleField, section.DiscTitle, 1);
            AddIfExists(output, Template.FilenameSuffixField, section.FilenameSuffix, 1);

            AddIfExists(output, Template.FullyMatchingIDsField, fullyMatchedIDs, 1);
            AddIfExists(output, Template.PartiallyMatchingIDsField, partiallyMatchedIDs, 1);
        }

        /// <summary>
        /// Format a RegionsAndLanguagesSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, RegionsAndLanguagesSection section)
        {
            output.AppendLine("Regions and Languages:");

            AddIfExists(output, Template.RegionsField,
                Array.ConvertAll(section.Regions ?? [null], l => l.LongName() ?? "SPACE! (CHANGE THIS)"), 1);
            AddIfExists(output, Template.LanguagesField,
                Array.ConvertAll(section.Languages ?? [null], l => l.LongName() ?? "ADD LANGUAGES HERE (ONLY IF YOU TESTED)"), 1);
        }

        /// <summary>
        /// Format a DiscIdentifiersSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, DiscIdentifiersSection section)
        {
            output.AppendLine("Disc Identifiers:");

            AddIfExists(output, Template.DiscSerialsField, section.DiscSerials, 1);
            AddIfExists(output, Template.EditionsField, section.Editions, 1);
            AddIfExists(output, Template.BarcodesField, section.Barcodes, 1);
            AddIfExists(output, Template.VersionField, section.Version, 1);
            AddIfExists(output, Template.ErrorCountField, section.ErrorCount, 1);
            AddIfExists(output, Template.EXEDate, section.EXEDate, 1);
            if (section.EDC != YesNo.NULL)
                AddIfExists(output, Template.EDCField, section.EDC.LongName(), 1);
            AddIfExists(output, Template.LayerbreakField, section.Layerbreak, 1);
            AddIfExists(output, $"{Template.LayerbreakField} 2", section.Layerbreak2, 1);
            AddIfExists(output, $"{Template.LayerbreakField} 3", section.Layerbreak3, 1);
            AddIfExists(output, Template.DiscIDField, section.DiscID, 1);
            AddIfExists(output, Template.DiscKeyField, section.DiscKey, 1);
            AddIfExists(output, Template.UniversalHashField, section.UniversalHash, 1);
        }

        /// <summary>
        /// Format a RingCodesSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output,
            RingCodesSection section,
            DiscIdentifiersSection discIdentifiers,
            PhysicalSystem? system)
        {
            // Sony-printed discs have layers in the opposite order
            bool reverseOrder = system.HasReversedRingcodes();

            output.AppendLine("Ring Codes:");
            output.AppendLine();

            // If we have a triple-layer disc
            if (discIdentifiers.Layerbreak3 != default && discIdentifiers.Layerbreak3 != default)
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringCodeField, section.Layer0MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampsField, section.Layer0Toolstamps, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDsField, section.Layer0MouldSIDs, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldsField, section.Layer0AdditionalMoulds, 0);

                AddIfExists(output, "Layer 1 " + Template.MasteringCodeField, section.Layer1MasteringCode, 0);
                AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, section.Layer1MasteringSID, 0);
                AddIfExists(output, "Layer 1 " + Template.ToolstampsField, section.Layer1Toolstamps, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDsField, section.Layer1MouldSIDs, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldsField, section.Layer1AdditionalMoulds, 0);

                AddIfExists(output, "Layer 2 " + Template.MasteringCodeField, section.Layer2MasteringCode, 0);
                AddIfExists(output, "Layer 2 " + Template.MasteringSIDField, section.Layer2MasteringSID, 0);
                AddIfExists(output, "Layer 2 " + Template.ToolstampsField, section.Layer2Toolstamps, 0);

                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringCodeField, section.Layer3MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringSIDField, section.Layer3MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.ToolstampsField, section.Layer3Toolstamps, 0);
            }
            // If we have a triple-layer disc
            else if (discIdentifiers?.Layerbreak2 != default && discIdentifiers?.Layerbreak2 != default(long))
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringCodeField, section.Layer0MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampsField, section.Layer0Toolstamps, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDsField, section.Layer0MouldSIDs, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldsField, section.Layer0AdditionalMoulds, 0);

                AddIfExists(output, "Layer 1 " + Template.MasteringCodeField, section.Layer1MasteringCode, 0);
                AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, section.Layer1MasteringSID, 0);
                AddIfExists(output, "Layer 1 " + Template.ToolstampsField, section.Layer1Toolstamps, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDsField, section.Layer1MouldSIDs, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldsField, section.Layer1AdditionalMoulds, 0);

                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringCodeField, section.Layer2MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringSIDField, section.Layer2MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.ToolstampsField, section.Layer2Toolstamps, 0);
            }
            // If we have a dual-layer disc
            else if (discIdentifiers?.Layerbreak != default && discIdentifiers?.Layerbreak != default(long))
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringCodeField, section.Layer0MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampsField, section.Layer0Toolstamps, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDsField, section.Layer0MouldSIDs, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldsField, section.Layer0AdditionalMoulds, 0);

                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringCodeField, section.Layer1MasteringCode, 0);
                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringSIDField, section.Layer1MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.ToolstampsField, section.Layer1Toolstamps, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDsField, section.Layer1MouldSIDs, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldsField, section.Layer1AdditionalMoulds, 0);
            }
            // If we have a single-layer disc
            else
            {
                AddIfExists(output, "Data Side " + Template.MasteringCodeField, section.Layer0MasteringCode, 0);
                AddIfExists(output, "Data Side " + Template.MasteringSIDField, section.Layer0MasteringSID, 0);
                AddIfExists(output, "Data Side " + Template.ToolstampsField, section.Layer0Toolstamps, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDsField, section.Layer0MouldSIDs, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldsField, section.Layer0AdditionalMoulds, 0);

                AddIfExists(output, "Label Side " + Template.MasteringCodeField, section.Layer1MasteringCode, 0);
                AddIfExists(output, "Label Side " + Template.MasteringSIDField, section.Layer1MasteringSID, 0);
                AddIfExists(output, "Label Side " + Template.ToolstampsField, section.Layer1Toolstamps, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDsField, section.Layer1MouldSIDs, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldsField, section.Layer1AdditionalMoulds, 0);
            }

            var offset = section.WriteOffset;
            if (int.TryParse(offset, out int i))
                offset = i.ToString("+#;-#;0");

            AddIfExists(output, Template.WriteOffsetField, offset, 0);
            output.AppendLine();
        }

        /// <summary>
        /// Format a DumpMetadataSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, DumpMetadataSection section)
        {
            output.AppendLine("Dump Metadata:");

            AddIfExists(output, Template.CommentsField, section.Comments?.Trim(), 1);
            AddIfExists(output, Template.ContentsField, section.Contents?.Trim(), 1);
            AddIfExists(output, Template.ProtectionField, section.Protection, 1);
            AddIfExists(output, Template.SectorRangesField, section.SectorRanges, 1);
            AddIfExists(output, Template.SBIField, section.SBI, 1);
            AddIfExists(output, Template.PVDField, section.PVD, 1);
            AddIfExists(output, Template.HeaderField, section.Header, 1);
            AddIfExists(output, Template.BCAField, section.BCA, 1);
            AddIfExists(output, Template.PICField, section.PIC, 1);
            AddIfExists(output, Template.CuesheetField, section.Cuesheet, 1);
            AddIfExists(output, Template.DatField, section.Dat, 1);
        }

        /// <summary>
        /// Format a SubmissionControlsSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, SubmissionControlsSection section)
        {
            output.AppendLine("Submission Controls:");

            AddIfExists(output, Template.DumpLogField, section.DumpLog, 1);
            AddIfExists(output, Template.LogsArchiveURLField, section.LogsArchiveURL, 1);
            AddIfExists(output, Template.ReviewCommentField, section.ReviewComment, 1);
            AddIfExists(output, Template.SubmissionCommentField, section.SubmissionComment, 1);
            AddIfExists(output, Template.SubmitAsField, section.SubmitAs, 1);
        }

        /// <summary>
        /// Format a DumpingInfoSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, DumpingInfoSection section)
        {
            output.AppendLine("Dumping Info:");

            AddIfExists(output, Template.FrontendVersionField, section.FrontendVersion, 1);
            AddIfExists(output, Template.DumpingProgramField, section.DumpingProgram, 1);
            AddIfExists(output, Template.DumpingDateField, section.DumpingDate, 1);
            AddIfExists(output, Template.DumpingParametersField, section.DumpingParameters, 1);
            AddIfExists(output, Template.DumpingDriveManufacturer, section.Manufacturer, 1);
            AddIfExists(output, Template.DumpingDriveModel, section.Model, 1);
            AddIfExists(output, Template.DumpingDriveFirmware, section.Firmware, 1);
            AddIfExists(output, Template.ReportedDiscType, section.ReportedDiscType, 1);
            AddIfExists(output, Template.C2ErrorCount, section.C2ErrorsCount, 1);
        }

        #region Helpers

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">String builder representing the output</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(StringBuilder output, string key, string? value, int indent)
        {
            // If there's no valid value to write
            if (value is null)
                return;

            string prefix = string.Empty;
            for (int i = 0; i < indent; i++)
            {
                prefix += "\t";
            }

            // Skip fields that need to keep internal whitespace intact
            if (key != "Primary Volume Descriptor (PVD)"
                && key != "Header"
                && key != "Cuesheet")
            {
                // Convert to tabs
#if NETCOREAPP
                value = value.Replace("<tab>", "\t", StringComparison.OrdinalIgnoreCase);
#else
                value = value.Replace("<tab>", "\t");
                value = value.Replace("<TAB>", "\t");
                value = value.Replace("<Tab>", "\t");
#endif
                value = value.Replace("  ", "\t");

                // Sanitize whitespace around tabs
                value = Regex.Replace(value, @"\s*\t\s*", "\t", RegexOptions.Compiled);
            }

            // If the value contains a newline
            value = value.Replace("\r\n", "\n");
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
            if (value.Contains('\n'))
#else
            if (value.Contains("\n"))
#endif
            {
                output.AppendLine(prefix + key + ":"); output.AppendLine();
                string[] values = value.Split('\n');
                foreach (string val in values)
                {
                    output.AppendLine(val);
                }

                output.AppendLine();
            }

            // For all regular values
            else
            {
                output.AppendLine(prefix + key + ": " + value);
            }
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">String builder representing the output</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(StringBuilder output, string key, string?[]? value, int indent)
        {
            // If there's no valid value to write
            if (value is null || value.Length == 0)
                return;

            AddIfExists(output, key, string.Join(", ", value), indent);
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">String builder representing the output</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(StringBuilder output, string key, long? value, int indent)
        {
            // If there's no valid value to write
            if (value is null || value == default(long))
                return;

            string prefix = string.Empty;
            for (int i = 0; i < indent; i++)
            {
                prefix += "\t";
            }

            output.AppendLine(prefix + key + ": " + value);
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">String builder representing the output</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(StringBuilder output, string key, List<int>? value, int indent)
        {
            // If there's no valid value to write
            if (value is null || value.Count == 0)
                return;

            AddIfExists(output, key, string.Join(", ", [.. value.ConvertAll(o => o.ToString())]), indent);
        }

        /// <summary>
        /// Format a single site tag to string
        /// </summary>
        /// <param name="code">Site tag to format</param>
        /// <param name="value">String value to use</param>
        /// <returns>String-formatted tag and value</returns>
        internal static string FormatSiteTag(SiteCode code, string value)
        {
            // Do not format empty tags
            if (value.Length == 0)
                return string.Empty;

            bool isMultiLine = code.IsMultiLine();
            string line = $"{code.ShortName()}{(isMultiLine ? "\n" : " ")}";

            // Special case for boolean fields
            if (code.IsBoolean())
            {
                if (value != true.ToString())
                    return string.Empty;

                return line.Trim();
            }

            return $"{line}{value}{(isMultiLine ? "\n" : string.Empty)}";
        }

        /// <summary>
        /// Get the adjusted name of the media based on layers, if applicable
        /// </summary>
        /// <param name="mediaType">MediaType to get the proper name for</param>
        /// <param name="picIdentifier">PIC identifier string (BD only)</param>
        /// <param name="size">Size of the current media</param>
        /// <param name="layerbreak">First layerbreak value, as applicable</param>
        /// <param name="layerbreak2">Second layerbreak value, as applicable</param>
        /// <param name="layerbreak3">Third layerbreak value, as applicable</param>
        /// <returns>String representation of the media, including layer specification</returns>
        internal static string? GetFixedMediaType(PhysicalMediaType? mediaType,
            string? picIdentifier,
            long? size,
            long? layerbreak,
            long? layerbreak2,
            long? layerbreak3)
        {
#pragma warning disable IDE0010
            switch (mediaType)
            {
                case PhysicalMediaType.DVD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-9";
                    else
                        return $"{mediaType.LongName()}-5";

                case PhysicalMediaType.BluRay:
                    if (layerbreak3 != default && layerbreak3 != default(long))
                        return $"{mediaType.LongName()}-128";
                    else if (layerbreak2 != default && layerbreak2 != default(long))
                        return $"{mediaType.LongName()}-100";
                    else if (layerbreak != default && layerbreak != default(long) && picIdentifier == "BDU")
                        return $"{mediaType.LongName()}-66";
                    else if (layerbreak != default && layerbreak != default(long) && size > 53_687_063_712)
                        return $"{mediaType.LongName()}-66";
                    else if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-50";
                    else if (picIdentifier == "BDU")
                        return $"{mediaType.LongName()}-33";
                    else if (size > 26_843_531_856)
                        return $"{mediaType.LongName()}-33";
                    else
                        return $"{mediaType.LongName()}-25";

                case PhysicalMediaType.HDDVD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-DL";
                    else
                        return $"{mediaType.LongName()}-SL";

                case PhysicalMediaType.UMD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-DL";
                    else
                        return $"{mediaType.LongName()}-SL";

                default:
                    return mediaType.LongName();
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Order comment code tags according to Redump requirements
        /// </summary>
        /// <returns>Ordered list of KeyValuePairs representing the tags and values</returns>
        internal static KeyValuePair<SiteCode, string>[] OrderCommentTags(Dictionary<SiteCode, string> tags)
        {
            // If the input is invalid, just return an empty set
            if (tags is null || tags.Count == 0)
                return [];

            // Loop through the ordered set of codes and add if needed
            var sorted = new List<KeyValuePair<SiteCode, string>>();
            foreach (var code in OrderedCommentCodes)
            {
                // Only add if it exists
                if (!tags.ContainsKey(code))
                    continue;

                // Get the tag value
                string value = tags[code];
                if (value.Length == 0)
                    continue;

                // Add to the set
                sorted.Add(new KeyValuePair<SiteCode, string>(code, value));
            }

            return [.. sorted];
        }

        /// <summary>
        /// Order content code tags according to Redump requirements
        /// </summary>
        /// <returns>Ordered list of KeyValuePairs representing the tags and values</returns>
        internal static KeyValuePair<SiteCode, string>[] OrderContentTags(Dictionary<SiteCode, string> tags)
        {
            // If the input is invalid, just return an empty set
            if (tags is null || tags.Count == 0)
                return [];

            // Loop through the ordered set of codes and add if needed
            var sorted = new List<KeyValuePair<SiteCode, string>>();
            foreach (var code in OrderedContentCodes)
            {
                // Only add if it exists
                if (!tags.ContainsKey(code))
                    continue;

                // Get the tag value
                string value = tags[code];
                if (value.Length == 0)
                    continue;

                // Add to the set
                sorted.Add(new KeyValuePair<SiteCode, string>(code, value));
            }

            return [.. sorted];
        }

        /// <summary>
        /// Make sure there aren't any instances of two blank lines in a row
        /// </summary>
        internal static string RemoveConsecutiveEmptyLines(string str)
        {
            str = Regex.Replace(str, @"(\r\n){2,}", "\r\n\r\n");
            return Regex.Replace(str, @"(\n){2,}", "\n\n");
        }

        #endregion
    }
}
