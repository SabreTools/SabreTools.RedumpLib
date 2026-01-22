using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Data.Sections;

namespace SabreTools.RedumpLib
{
    public static class Formatter
    {
        /// <summary>
        /// Ordered set of comment codes for output
        /// </summary>
        internal static readonly SiteCode[] OrderedCommentCodes =
        [
            // Submission Info
            SiteCode.LogsLink,

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
            SiteCode.ISBN,
            SiteCode.ISSN,
            SiteCode.PPN,
            SiteCode.VFCCode,

            SiteCode.CompatibleOS,
            SiteCode.Genre,
            SiteCode.Series,
            SiteCode.PostgapType,
            SiteCode.VCD,

            // Publisher / Company IDs
            SiteCode.TwoKGamesID,
            SiteCode.AcclaimID,
            SiteCode.ActivisionID,
            SiteCode.BandaiID,
            SiteCode.BethesdaID,
            SiteCode.CDProjektID,
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
        /// <param name="enableRedumpCompatibility">True to enable Redump compatiblity, false otherwise</param>
        /// <returns>String representing each line of an output file, null on error</returns>
        public static string? FormatOutputData(SubmissionInfo? info, bool enableRedumpCompatibility, out string? status)
        {
            // Check to see if the inputs are valid
            if (info == null)
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

                // Common Disc Info section
                FormatOutputData(output,
                    info.CommonDiscInfo,
                    info.SizeAndChecksums,
                    info.TracksAndWriteOffsets,
                    info.FullyMatchedID,
                    info.PartiallyMatchedIDs);
                output.AppendLine();

                // Version and Editions section
                FormatOutputData(output, info.VersionAndEditions);
                output.AppendLine();

                // EDC section
                FormatOutputData(output, info.EDC, info.CommonDiscInfo.System);
                output.AppendLine();

                // Extras section
                FormatOutputData(output, info.Extras);
                output.AppendLine();

                // Copy Protection section
                FormatOutputData(output, info.CopyProtection, info.CommonDiscInfo.System);
                output.AppendLine();

                // Tracks and Write Offsets section
                if (!string.IsNullOrEmpty(info.TracksAndWriteOffsets.ClrMameProData))
                {
                    FormatOutputData(output, info.TracksAndWriteOffsets!);
                    output.AppendLine();
                }
                // Size & Checksum section
                else
                {
                    FormatOutputData(output,
                        info.SizeAndChecksums,
                        info.CommonDiscInfo.Media.ToMediaType(),
                        info.CommonDiscInfo.System,
                        enableRedumpCompatibility);
                    output.AppendLine();
                }

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
            // If there is no submission info
            if (info?.CommonDiscInfo == null)
                return;

            // Process the comments field
            if (info.CommonDiscInfo.CommentsSpecialFields != null && info.CommonDiscInfo.CommentsSpecialFields.Count > 0)
            {
                // If the field is missing, add an empty one to fill in
                info.CommonDiscInfo.Comments ??= string.Empty;

                // Add all special fields before any comments
                var orderedTags = OrderCommentTags(info.CommonDiscInfo.CommentsSpecialFields);
                var formattedTags = Array.ConvertAll(orderedTags, kvp => FormatSiteTag(kvp.Key, kvp.Value));
                info.CommonDiscInfo.Comments = string.Join("\n", formattedTags) + "\n" + info.CommonDiscInfo.Comments;

                // Normalize the assembled string
                info.CommonDiscInfo.Comments = info.CommonDiscInfo.Comments.Replace("\r\n", "\n");
                info.CommonDiscInfo.Comments = info.CommonDiscInfo.Comments.Replace("\n\n", "\n");
                info.CommonDiscInfo.Comments = info.CommonDiscInfo.Comments.Trim();

                // Wipe out the special fields dictionary
                info.CommonDiscInfo.CommentsSpecialFields.Clear();
            }

            // Process the contents field
            if (info.CommonDiscInfo.ContentsSpecialFields != null && info.CommonDiscInfo.ContentsSpecialFields.Count > 0)
            {
                // If the field is missing, add an empty one to fill in
                info.CommonDiscInfo.Contents ??= string.Empty;

                // Add all special fields before any contents
                var orderedTags = OrderContentTags(info.CommonDiscInfo.ContentsSpecialFields);
                var formattedTags = Array.ConvertAll(orderedTags, kvp => FormatSiteTag(kvp.Key, kvp.Value));
                info.CommonDiscInfo.Contents = string.Join("\n", formattedTags) + "\n" + info.CommonDiscInfo.Contents;

                // Normalize the assembled string
                info.CommonDiscInfo.Contents = info.CommonDiscInfo.Contents.Replace("\r\n", "\n");
                info.CommonDiscInfo.Contents = info.CommonDiscInfo.Contents.Replace("\n\n", "\n");
                info.CommonDiscInfo.Contents = info.CommonDiscInfo.Contents.Trim();

                // Wipe out the special fields dictionary
                info.CommonDiscInfo.ContentsSpecialFields.Clear();
            }
        }

        /// <summary>
        /// Format a CommonDiscInfoSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output,
            CommonDiscInfoSection? section,
            SizeAndChecksumsSection? sac,
            TracksAndWriteOffsetsSection? tawo,
            int? fullyMatchedID,
            List<int>? partiallyMatchedIDs)
        {
            // Sony-printed discs have layers in the opposite order
            var system = section?.System;
            bool reverseOrder = system.HasReversedRingcodes();

            output.AppendLine("Common Disc Info:");

            AddIfExists(output, Template.TitleField, section?.Title, 1);
            AddIfExists(output, Template.ForeignTitleField, section?.ForeignTitleNonLatin, 1);
            AddIfExists(output, Template.DiscNumberField, section?.DiscNumberLetter, 1);
            AddIfExists(output, Template.DiscTitleField, section?.DiscTitle, 1);
            AddIfExists(output, Template.SystemField, section?.System.LongName(), 1);
            AddIfExists(output, Template.MediaTypeField, GetFixedMediaType(
                    section?.Media.ToMediaType(),
                    sac?.PICIdentifier,
                    sac?.Size,
                    sac?.Layerbreak,
                    sac?.Layerbreak2,
                    sac?.Layerbreak3),
                1);
            AddIfExists(output, Template.CategoryField, section?.Category.LongName(), 1);
            AddIfExists(output, Template.FullyMatchingIDField, fullyMatchedID?.ToString(), 1);
            AddIfExists(output, Template.PartiallyMatchingIDsField, partiallyMatchedIDs, 1);
            AddIfExists(output, Template.RegionField, section?.Region.LongName() ?? "SPACE! (CHANGE THIS)", 1);
            AddIfExists(output, Template.LanguagesField,
                Array.ConvertAll(section?.Languages ?? [null], l => l.LongName() ?? "ADD LANGUAGES HERE (ONLY IF YOU TESTED)"), 1);
            AddIfExists(output, Template.PlaystationLanguageSelectionViaField,
                Array.ConvertAll(section?.LanguageSelection ?? [], l => l.LongName()), 1);
            AddIfExists(output, Template.DiscSerialField, section?.Serial, 1);
            output.AppendLine();

            // All ringcode information goes in an indented area
            output.AppendLine("\tRingcode Information:");
            output.AppendLine();

            // If we have a triple-layer disc
            if (sac?.Layerbreak3 != default && sac?.Layerbreak3 != default(long))
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, section?.Layer0MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section?.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, section?.Layer0ToolstampMasteringCode, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDField, section?.Layer0MouldSID, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldField, section?.Layer0AdditionalMould, 0);

                AddIfExists(output, "Layer 1 " + Template.MasteringRingField, section?.Layer1MasteringRing, 0);
                AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, section?.Layer1MasteringSID, 0);
                AddIfExists(output, "Layer 1 " + Template.ToolstampField, section?.Layer1ToolstampMasteringCode, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDField, section?.Layer1MouldSID, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldField, section?.Layer1AdditionalMould, 0);

                AddIfExists(output, "Layer 2 " + Template.MasteringRingField, section?.Layer2MasteringRing, 0);
                AddIfExists(output, "Layer 2 " + Template.MasteringSIDField, section?.Layer2MasteringSID, 0);
                AddIfExists(output, "Layer 2 " + Template.ToolstampField, section?.Layer2ToolstampMasteringCode, 0);

                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringRingField, section?.Layer3MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringSIDField, section?.Layer3MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.ToolstampField, section?.Layer3ToolstampMasteringCode, 0);
            }
            // If we have a triple-layer disc
            else if (sac?.Layerbreak2 != default && sac?.Layerbreak2 != default(long))
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, section?.Layer0MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section?.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, section?.Layer0ToolstampMasteringCode, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDField, section?.Layer0MouldSID, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldField, section?.Layer0AdditionalMould, 0);

                AddIfExists(output, "Layer 1 " + Template.MasteringRingField, section?.Layer1MasteringRing, 0);
                AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, section?.Layer1MasteringSID, 0);
                AddIfExists(output, "Layer 1 " + Template.ToolstampField, section?.Layer1ToolstampMasteringCode, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDField, section?.Layer1MouldSID, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldField, section?.Layer1AdditionalMould, 0);

                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringRingField, section?.Layer2MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringSIDField, section?.Layer2MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.ToolstampField, section?.Layer2ToolstampMasteringCode, 0);
            }
            // If we have a dual-layer disc
            else if (sac?.Layerbreak != default && sac?.Layerbreak != default(long))
            {
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, section?.Layer0MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, section?.Layer0MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, section?.Layer0ToolstampMasteringCode, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDField, section?.Layer0MouldSID, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldField, section?.Layer0AdditionalMould, 0);

                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringRingField, section?.Layer1MasteringRing, 0);
                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringSIDField, section?.Layer1MasteringSID, 0);
                AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.ToolstampField, section?.Layer1ToolstampMasteringCode, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDField, section?.Layer1MouldSID, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldField, section?.Layer1AdditionalMould, 0);
            }
            // If we have a single-layer disc
            else
            {
                AddIfExists(output, "Data Side " + Template.MasteringRingField, section?.Layer0MasteringRing, 0);
                AddIfExists(output, "Data Side " + Template.MasteringSIDField, section?.Layer0MasteringSID, 0);
                AddIfExists(output, "Data Side " + Template.ToolstampField, section?.Layer0ToolstampMasteringCode, 0);
                AddIfExists(output, "Data Side " + Template.MouldSIDField, section?.Layer0MouldSID, 0);
                AddIfExists(output, "Data Side " + Template.AdditionalMouldField, section?.Layer0AdditionalMould, 0);

                AddIfExists(output, "Label Side " + Template.MasteringRingField, section?.Layer1MasteringRing, 0);
                AddIfExists(output, "Label Side " + Template.MasteringSIDField, section?.Layer1MasteringSID, 0);
                AddIfExists(output, "Label Side " + Template.ToolstampField, section?.Layer1ToolstampMasteringCode, 0);
                AddIfExists(output, "Label Side " + Template.MouldSIDField, section?.Layer1MouldSID, 0);
                AddIfExists(output, "Label Side " + Template.AdditionalMouldField, section?.Layer1AdditionalMould, 0);
            }

            var offset = tawo?.OtherWriteOffsets;
            if (int.TryParse(offset, out int i))
                offset = i.ToString("+#;-#;0");

            AddIfExists(output, Template.WriteOffsetField, offset, 0);
            output.AppendLine();

            AddIfExists(output, Template.BarcodeField, section?.Barcode, 1);
            AddIfExists(output, Template.EXEDateBuildDate, section?.EXEDateBuildDate, 1);
            AddIfExists(output, Template.ErrorCountField, section?.ErrorsCount, 1);
            AddIfExists(output, Template.CommentsField, section?.Comments?.Trim(), 1);
            AddIfExists(output, Template.ContentsField, section?.Contents?.Trim(), 1);
        }

        /// <summary>
        /// Format a VersionAndEditionsSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, VersionAndEditionsSection? section)
        {
            output.AppendLine("Version and Editions:");

            AddIfExists(output, Template.VersionField, section?.Version, 1);
            AddIfExists(output, Template.EditionField, section?.OtherEditions, 1);
        }

        /// <summary>
        /// Format a EDCSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, EDCSection? section, RedumpSystem? system)
        {
            // Check the section can be added
            if (system != RedumpSystem.SonyPlayStation)
                return;

            output.AppendLine("EDC:");

            AddIfExists(output, Template.PlayStationEDCField, section?.EDC.LongName(), 1);
        }

        /// <summary>
        /// Format a ExtrasSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, ExtrasSection? section)
        {
            // Optional sections have to exist to format
            if (section == null)
                return;

            // Check the section can be added
            if (section.PVD == null
                && section.PIC == null
                && section.BCA == null
                && section.SecuritySectorRanges == null)
            {
                return;
            }

            output.AppendLine("Extras:");

            AddIfExists(output, Template.PVDField, section.PVD?.Trim(), 1);
            AddIfExists(output, Template.PlayStation3WiiDiscKeyField, section.DiscKey, 1);
            AddIfExists(output, Template.PlayStation3DiscIDField, section.DiscID, 1);
            AddIfExists(output, Template.PICField, section.PIC, 1);
            AddIfExists(output, Template.HeaderField, section.Header, 1);
            AddIfExists(output, Template.GameCubeWiiBCAField, section.BCA, 1);
            AddIfExists(output, Template.XBOXSSRanges, section.SecuritySectorRanges, 1);
        }

        /// <summary>
        /// Format a ExtrasSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output,
            CopyProtectionSection? section,
            RedumpSystem? system)
        {
            // Optional sections have to exist to format
            if (section == null)
                return;

            // Check the section can be added
            if (string.IsNullOrEmpty(section.Protection)
                && (section.AntiModchip == null || section.AntiModchip == YesNo.NULL)
                && (section.LibCrypt == null || section.LibCrypt == YesNo.NULL)
                && string.IsNullOrEmpty(section.LibCryptData)
                && string.IsNullOrEmpty(section.SecuROMData))
            {
                return;
            }

            output.AppendLine("Copy Protection:");

            if (system == RedumpSystem.SonyPlayStation)
            {
                AddIfExists(output, Template.PlayStationAntiModchipField, section.AntiModchip.LongName(), 1);
                AddIfExists(output, Template.PlayStationLibCryptField, section.LibCrypt.LongName(), 1);
                AddIfExists(output, Template.SubIntentionField, section.LibCryptData, 1);
            }

            AddIfExists(output, Template.CopyProtectionField, section.Protection, 1);
            AddIfExists(output, Template.SubIntentionField, section.SecuROMData, 1);
        }

        /// <summary>
        /// Format a TracksAndWriteOffsetsSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, TracksAndWriteOffsetsSection section)
        {
            output.AppendLine("Tracks and Write Offsets:");

            AddIfExists(output, Template.DATField, section.ClrMameProData + "\n", 1);
            AddIfExists(output, Template.CuesheetField, section.Cuesheet, 1);
            // TODO: Figure out how to emit raw cuesheet field instead of normal cuesheet
            var offset = section.OtherWriteOffsets;
            if (int.TryParse(offset, out int i))
                offset = i.ToString("+#;-#;0");

            AddIfExists(output, Template.WriteOffsetField, offset, 1);
        }

        /// <summary>
        /// Format a SizeAndChecksumsSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output,
            SizeAndChecksumsSection? section,
            MediaType? mediaType,
            RedumpSystem? system,
            bool enableRedumpCompatibility)
        {
            output.AppendLine("Size & Checksum:");

            // Gross hack because of automatic layerbreaks in Redump
            if (!enableRedumpCompatibility
                || (mediaType != MediaType.BluRay && system.IsXGD() == false))
            {
                AddIfExists(output, Template.LayerbreakField, section?.Layerbreak, 1);
            }

            AddIfExists(output, Template.SizeField, section?.Size.ToString(), 1);
            AddIfExists(output, Template.CRC32Field, section?.CRC32, 1);
            AddIfExists(output, Template.MD5Field, section?.MD5, 1);
            AddIfExists(output, Template.SHA1Field, section?.SHA1, 1);
        }

        /// <summary>
        /// Format a DumpingInfoSection
        /// </summary>
        internal static void FormatOutputData(StringBuilder output, DumpingInfoSection? section)
        {
            output.AppendLine("Dumping Info:");

            AddIfExists(output, Template.FrontendVersionField, section?.FrontendVersion, 1);
            AddIfExists(output, Template.DumpingProgramField, section?.DumpingProgram, 1);
            AddIfExists(output, Template.DumpingDateField, section?.DumpingDate, 1);
            AddIfExists(output, Template.DumpingParametersField, section?.DumpingParameters, 1);
            AddIfExists(output, Template.DumpingDriveManufacturer, section?.Manufacturer, 1);
            AddIfExists(output, Template.DumpingDriveModel, section?.Model, 1);
            AddIfExists(output, Template.DumpingDriveFirmware, section?.Firmware, 1);
            AddIfExists(output, Template.ReportedDiscType, section?.ReportedDiscType, 1);
            AddIfExists(output, Template.C2ErrorCountField, section?.C2ErrorsCount, 1);
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
            if (value == null)
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
            if (value == null || value.Length == 0)
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
            if (value == null || value == default(long))
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
            if (value == null || value.Count == 0)
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
        internal static string? GetFixedMediaType(MediaType? mediaType, string? picIdentifier, long? size, long? layerbreak, long? layerbreak2, long? layerbreak3)
        {
            switch (mediaType)
            {
                case MediaType.DVD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-9";
                    else
                        return $"{mediaType.LongName()}-5";

                case MediaType.BluRay:
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

                case MediaType.HDDVD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-DL";
                    else
                        return $"{mediaType.LongName()}-SL";

                case MediaType.UMD:
                    if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-DL";
                    else
                        return $"{mediaType.LongName()}-SL";

                default:
                    return mediaType.LongName();
            }
        }

        /// <summary>
        /// Order comment code tags according to Redump requirements
        /// </summary>
        /// <returns>Ordered list of KeyValuePairs representing the tags and values</returns>
        internal static KeyValuePair<SiteCode, string>[] OrderCommentTags(Dictionary<SiteCode, string> tags)
        {
            // If the input is invalid, just return an empty set
            if (tags == null || tags.Count == 0)
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
            if (tags == null || tags.Count == 0)
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
