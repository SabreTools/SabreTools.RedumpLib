using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib
{
    public static class Formatter
    {
        /// <summary>
        /// Format the output data in a human readable way, separating each printed line into a new item in the list
        /// </summary>
        /// <param name="info">Information object that should contain normalized values</param>
        /// <param name="enableRedumpCompatibility">True to enable Redump compatiblity, false otherwise</param>
        /// <returns>List of strings representing each line of an output file, null on error</returns>
        public static (List<string>?, string?) FormatOutputData(SubmissionInfo? info, bool enableRedumpCompatibility)
        {
            // Check to see if the inputs are valid
            if (info == null)
                return (null, "Submission information was missing");

            try
            {
                // Sony-printed discs have layers in the opposite order
                var system = info.CommonDiscInfo?.System;
                bool reverseOrder = system.HasReversedRingcodes();

                // Preamble for submission
#pragma warning disable IDE0028
                var output = new List<string>
                {
                    "Users who wish to submit this information to Redump must ensure that all of the fields below are accurate for the exact media they have.",
                    "Please double-check to ensure that there are no fields that need verification, such as the version or copy protection.",
                    "If there are no fields in need of verification or all fields are accurate, this preamble can be removed before submission.",
                    "",
                };

                // Common Disc Info section
                output.Add("Common Disc Info:");
                AddIfExists(output, Template.TitleField, info.CommonDiscInfo?.Title, 1);
                AddIfExists(output, Template.ForeignTitleField, info.CommonDiscInfo?.ForeignTitleNonLatin, 1);
                AddIfExists(output, Template.DiscNumberField, info.CommonDiscInfo?.DiscNumberLetter, 1);
                AddIfExists(output, Template.DiscTitleField, info.CommonDiscInfo?.DiscTitle, 1);
                AddIfExists(output, Template.SystemField, info.CommonDiscInfo?.System.LongName(), 1);
                AddIfExists(output, Template.MediaTypeField, GetFixedMediaType(
                        info.CommonDiscInfo?.Media.ToMediaType(),
                        info.SizeAndChecksums?.PICIdentifier,
                        info.SizeAndChecksums?.Size,
                        info.SizeAndChecksums?.Layerbreak,
                        info.SizeAndChecksums?.Layerbreak2,
                        info.SizeAndChecksums?.Layerbreak3),
                    1);
                AddIfExists(output, Template.CategoryField, info.CommonDiscInfo?.Category.LongName(), 1);
                AddIfExists(output, Template.FullyMatchingIDField, info.FullyMatchedID?.ToString(), 1);
                AddIfExists(output, Template.PartiallyMatchingIDsField, info.PartiallyMatchedIDs, 1);
                AddIfExists(output, Template.RegionField, info.CommonDiscInfo?.Region.LongName() ?? "SPACE! (CHANGE THIS)", 1);
                AddIfExists(output, Template.LanguagesField, (info.CommonDiscInfo?.Languages ?? [null]).Select(l => l.LongName() ?? "SILENCE! (CHANGE THIS)").ToArray(), 1);
                AddIfExists(output, Template.PlaystationLanguageSelectionViaField, (info.CommonDiscInfo?.LanguageSelection ?? []).Select(l => l.LongName()).ToArray(), 1);
                AddIfExists(output, Template.DiscSerialField, info.CommonDiscInfo?.Serial, 1);

                // All ringcode information goes in an indented area
                output.Add(""); output.Add("\tRingcode Information:"); output.Add("");

                // If we have a triple-layer disc
                if (info.SizeAndChecksums?.Layerbreak3 != default && info.SizeAndChecksums?.Layerbreak3 != default(long))
                {
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer0MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer0MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer0ToolstampMasteringCode, 0);
                    AddIfExists(output, "Data Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer0MouldSID, 0);
                    AddIfExists(output, "Data Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer0AdditionalMould, 0);

                    AddIfExists(output, "Layer 1 " + Template.MasteringRingField, info.CommonDiscInfo?.Layer1MasteringRing, 0);
                    AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, info.CommonDiscInfo?.Layer1MasteringSID, 0);
                    AddIfExists(output, "Layer 1 " + Template.ToolstampField, info.CommonDiscInfo?.Layer1ToolstampMasteringCode, 0);
                    AddIfExists(output, "Label Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer1MouldSID, 0);
                    AddIfExists(output, "Label Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer1AdditionalMould, 0);

                    AddIfExists(output, "Layer 2 " + Template.MasteringRingField, info.CommonDiscInfo?.Layer2MasteringRing, 0);
                    AddIfExists(output, "Layer 2 " + Template.MasteringSIDField, info.CommonDiscInfo?.Layer2MasteringSID, 0);
                    AddIfExists(output, "Layer 2 " + Template.ToolstampField, info.CommonDiscInfo?.Layer2ToolstampMasteringCode, 0);

                    AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer3MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer3MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 3 (Inner) " : "Layer 3 (Outer) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer3ToolstampMasteringCode, 0);
                }
                // If we have a triple-layer disc
                else if (info.SizeAndChecksums?.Layerbreak2 != default && info.SizeAndChecksums?.Layerbreak2 != default(long))
                {
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer0MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer0MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer0ToolstampMasteringCode, 0);
                    AddIfExists(output, "Data Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer0MouldSID, 0);
                    AddIfExists(output, "Data Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer0AdditionalMould, 0);

                    AddIfExists(output, "Layer 1 " + Template.MasteringRingField, info.CommonDiscInfo?.Layer1MasteringRing, 0);
                    AddIfExists(output, "Layer 1 " + Template.MasteringSIDField, info.CommonDiscInfo?.Layer1MasteringSID, 0);
                    AddIfExists(output, "Layer 1 " + Template.ToolstampField, info.CommonDiscInfo?.Layer1ToolstampMasteringCode, 0);
                    AddIfExists(output, "Label Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer1MouldSID, 0);
                    AddIfExists(output, "Label Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer1AdditionalMould, 0);

                    AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer2MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer2MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 2 (Inner) " : "Layer 2 (Outer) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer2ToolstampMasteringCode, 0);
                }
                // If we have a dual-layer disc
                else if (info.SizeAndChecksums?.Layerbreak != default && info.SizeAndChecksums?.Layerbreak != default(long))
                {
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer0MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer0MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 0 (Outer) " : "Layer 0 (Inner) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer0ToolstampMasteringCode, 0);
                    AddIfExists(output, "Data Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer0MouldSID, 0);
                    AddIfExists(output, "Data Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer0AdditionalMould, 0);

                    AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringRingField, info.CommonDiscInfo?.Layer1MasteringRing, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.MasteringSIDField, info.CommonDiscInfo?.Layer1MasteringSID, 0);
                    AddIfExists(output, (reverseOrder ? "Layer 1 (Inner) " : "Layer 1 (Outer) ") + Template.ToolstampField, info.CommonDiscInfo?.Layer1ToolstampMasteringCode, 0);
                    AddIfExists(output, "Label Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer1MouldSID, 0);
                    AddIfExists(output, "Label Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer1AdditionalMould, 0);
                }
                // If we have a single-layer disc
                else
                {
                    AddIfExists(output, "Data Side " + Template.MasteringRingField, info.CommonDiscInfo?.Layer0MasteringRing, 0);
                    AddIfExists(output, "Data Side " + Template.MasteringSIDField, info.CommonDiscInfo?.Layer0MasteringSID, 0);
                    AddIfExists(output, "Data Side " + Template.ToolstampField, info.CommonDiscInfo?.Layer0ToolstampMasteringCode, 0);
                    AddIfExists(output, "Data Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer0MouldSID, 0);
                    AddIfExists(output, "Data Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer0AdditionalMould, 0);

                    AddIfExists(output, "Label Side " + Template.MasteringRingField, info.CommonDiscInfo?.Layer1MasteringRing, 0);
                    AddIfExists(output, "Label Side " + Template.MasteringSIDField, info.CommonDiscInfo?.Layer1MasteringSID, 0);
                    AddIfExists(output, "Label Side " + Template.ToolstampField, info.CommonDiscInfo?.Layer1ToolstampMasteringCode, 0);
                    AddIfExists(output, "Label Side " + Template.MouldSIDField, info.CommonDiscInfo?.Layer1MouldSID, 0);
                    AddIfExists(output, "Label Side " + Template.AdditionalMouldField, info.CommonDiscInfo?.Layer1AdditionalMould, 0);
                }

                output.Add("");
                AddIfExists(output, Template.BarcodeField, info.CommonDiscInfo?.Barcode, 1);
                AddIfExists(output, Template.EXEDateBuildDate, info.CommonDiscInfo?.EXEDateBuildDate, 1);
                AddIfExists(output, Template.ErrorCountField, info.CommonDiscInfo?.ErrorsCount, 1);
                AddIfExists(output, Template.CommentsField, info.CommonDiscInfo?.Comments?.Trim(), 1);
                AddIfExists(output, Template.ContentsField, info.CommonDiscInfo?.Contents?.Trim(), 1);

                // Version and Editions section
                output.Add(""); output.Add("Version and Editions:");
                AddIfExists(output, Template.VersionField, info.VersionAndEditions?.Version, 1);
                AddIfExists(output, Template.EditionField, info.VersionAndEditions?.OtherEditions, 1);

                // EDC section
                if (info.CommonDiscInfo?.System == RedumpSystem.SonyPlayStation)
                {
                    output.Add(""); output.Add("EDC:");
                    AddIfExists(output, Template.PlayStationEDCField, info.EDC?.EDC.LongName(), 1);
                }

                // Parent/Clone Relationship section
                // output.Add(""); output.Add("Parent/Clone Relationship:");
                // AddIfExists(output, Template.ParentIDField, info.ParentID);
                // AddIfExists(output, Template.RegionalParentField, info.RegionalParent.ToString());

                // Extras section
                if (info.Extras?.PVD != null || info.Extras?.PIC != null || info.Extras?.BCA != null || info.Extras?.SecuritySectorRanges != null)
                {
                    output.Add(""); output.Add("Extras:");
                    AddIfExists(output, Template.PVDField, info.Extras.PVD?.Trim(), 1);
                    AddIfExists(output, Template.PlayStation3WiiDiscKeyField, info.Extras.DiscKey, 1);
                    AddIfExists(output, Template.PlayStation3DiscIDField, info.Extras.DiscID, 1);
                    AddIfExists(output, Template.PICField, info.Extras.PIC, 1);
                    AddIfExists(output, Template.HeaderField, info.Extras.Header, 1);
                    AddIfExists(output, Template.GameCubeWiiBCAField, info.Extras.BCA, 1);
                    AddIfExists(output, Template.XBOXSSRanges, info.Extras.SecuritySectorRanges, 1);
                }

                // Copy Protection section
                if (!string.IsNullOrEmpty(info.CopyProtection?.Protection)
                    || (info.CopyProtection?.AntiModchip != null && info.CopyProtection.AntiModchip != YesNo.NULL)
                    || (info.CopyProtection?.LibCrypt != null && info.CopyProtection.LibCrypt != YesNo.NULL)
                    || !string.IsNullOrEmpty(info.CopyProtection?.LibCryptData)
                    || !string.IsNullOrEmpty(info.CopyProtection?.SecuROMData))
                {
                    output.Add(""); output.Add("Copy Protection:");
                    if (info.CommonDiscInfo?.System == RedumpSystem.SonyPlayStation)
                    {
                        AddIfExists(output, Template.PlayStationAntiModchipField, info.CopyProtection!.AntiModchip.LongName(), 1);
                        AddIfExists(output, Template.PlayStationLibCryptField, info.CopyProtection.LibCrypt.LongName(), 1);
                        AddIfExists(output, Template.SubIntentionField, info.CopyProtection.LibCryptData, 1);
                    }

                    AddIfExists(output, Template.CopyProtectionField, info.CopyProtection!.Protection, 1);
                    AddIfExists(output, Template.SubIntentionField, info.CopyProtection.SecuROMData, 1);
                }

                // Dumpers and Status section
                // output.Add(""); output.Add("Dumpers and Status");
                // AddIfExists(output, Template.StatusField, info.Status.Name());
                // AddIfExists(output, Template.OtherDumpersField, info.OtherDumpers);

                // Tracks and Write Offsets section
                if (!string.IsNullOrEmpty(info.TracksAndWriteOffsets?.ClrMameProData))
                {
                    output.Add(""); output.Add("Tracks and Write Offsets:");
                    AddIfExists(output, Template.DATField, info.TracksAndWriteOffsets!.ClrMameProData + "\n", 1);
                    AddIfExists(output, Template.CuesheetField, info.TracksAndWriteOffsets.Cuesheet, 1);
                    var offset = info.TracksAndWriteOffsets.OtherWriteOffsets;
                    if (Int32.TryParse(offset, out int i))
                        offset = i.ToString("+#;-#;0");

                    AddIfExists(output, Template.WriteOffsetField, offset, 1);
                }
                // Size & Checksum section
                else
                {
                    output.Add(""); output.Add("Size & Checksum:");

                    // Gross hack because of automatic layerbreaks in Redump
                    if (!enableRedumpCompatibility
                        || (info.CommonDiscInfo?.Media.ToMediaType() != MediaType.BluRay
                            && info.CommonDiscInfo?.System.IsXGD() == false))
                    {
                        AddIfExists(output, Template.LayerbreakField, info.SizeAndChecksums?.Layerbreak, 1);
                    }

                    AddIfExists(output, Template.SizeField, info.SizeAndChecksums?.Size.ToString(), 1);
                    AddIfExists(output, Template.CRC32Field, info.SizeAndChecksums?.CRC32, 1);
                    AddIfExists(output, Template.MD5Field, info.SizeAndChecksums?.MD5, 1);
                    AddIfExists(output, Template.SHA1Field, info.SizeAndChecksums?.SHA1, 1);
                }

                // Dumping Info section
                output.Add(""); output.Add("Dumping Info:");
                AddIfExists(output, Template.FrontendVersionField, info.DumpingInfo?.FrontendVersion, 1);
                AddIfExists(output, Template.DumpingProgramField, info.DumpingInfo?.DumpingProgram, 1);
                AddIfExists(output, Template.DumpingDateField, info.DumpingInfo?.DumpingDate, 1);
                AddIfExists(output, Template.DumpingDriveManufacturer, info.DumpingInfo?.Manufacturer, 1);
                AddIfExists(output, Template.DumpingDriveModel, info.DumpingInfo?.Model, 1);
                AddIfExists(output, Template.DumpingDriveFirmware, info.DumpingInfo?.Firmware, 1);
                AddIfExists(output, Template.ReportedDiscType, info.DumpingInfo?.ReportedDiscType, 1);
                AddIfExists(output, Template.C2ErrorCountField, info.DumpingInfo?.C2ErrorsCount, 1);

                // Make sure there aren't any instances of two blank lines in a row
                string? last = null;
                for (int i = 0; i < output.Count;)
                {
                    if (output[i] == last && string.IsNullOrEmpty(last))
                    {
                        output.RemoveAt(i);
                    }
                    else
                    {
                        last = output[i];
                        i++;
                    }
                }

                return (output, "Formatting complete!");
            }
            catch (Exception ex)
            {
                return (null, $"Error formatting submission info: {ex}");
            }
        }

        /// <summary>
        /// Process any fields that have to be combined
        /// </summary>
        /// <param name="info">Information object to normalize</param>
        public static void ProcessSpecialFields(SubmissionInfo? info)
        {
            // If there is no submission info
            if (info == null)
                return;

            // Process the comments field
            if (info.CommonDiscInfo?.CommentsSpecialFields != null && info.CommonDiscInfo.CommentsSpecialFields?.Any() == true)
            {
                // If the field is missing, add an empty one to fill in
                if (info.CommonDiscInfo.Comments == null)
                    info.CommonDiscInfo.Comments = string.Empty;

                // Add all special fields before any comments
                info.CommonDiscInfo.Comments = string.Join(
                    "\n", OrderCommentTags(info.CommonDiscInfo.CommentsSpecialFields)
                        .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                        .Select(FormatSiteTag)
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToArray()
                ) + "\n" + info.CommonDiscInfo.Comments;

                // Normalize newlines
                info.CommonDiscInfo.Comments = info.CommonDiscInfo.Comments.Replace("\r\n", "\n");

                // Trim the comments field
                info.CommonDiscInfo.Comments = info.CommonDiscInfo.Comments.Trim();

                // Wipe out the special fields dictionary
                info.CommonDiscInfo.CommentsSpecialFields = null;
            }

            // Process the contents field
            if (info.CommonDiscInfo?.ContentsSpecialFields != null && info.CommonDiscInfo.ContentsSpecialFields?.Any() == true)
            {
                // If the field is missing, add an empty one to fill in
                if (info.CommonDiscInfo.Contents == null)
                    info.CommonDiscInfo.Contents = string.Empty;

                // Add all special fields before any contents
                info.CommonDiscInfo.Contents = string.Join(
                    "\n", OrderContentTags(info.CommonDiscInfo.ContentsSpecialFields)
                        .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                        .Select(FormatSiteTag)
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToArray()
                ) + "\n" + info.CommonDiscInfo.Contents;

                // Normalize newlines
                info.CommonDiscInfo.Contents = info.CommonDiscInfo.Contents.Replace("\r\n", "\n");

                // Trim the contents field
                info.CommonDiscInfo.Contents = info.CommonDiscInfo.Contents.Trim();

                // Wipe out the special fields dictionary
                info.CommonDiscInfo.ContentsSpecialFields = null;
            }
        }

        #region Helpers

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">Output list</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(List<string> output, string key, string? value, int indent)
        {
            // If there's no valid value to write
            if (value == null)
                return;

            string prefix = string.Empty;
            for (int i = 0; i < indent; i++)
                prefix += "\t";

            // Skip fields that need to keep internal whitespace intact
            if (key != "Primary Volume Descriptor (PVD)"
                && key != "Header"
                && key != "Cuesheet")
            {
                // Convert to tabs
                value = value.Replace("<tab>", "\t");
                value = value.Replace("<TAB>", "\t");
                value = value.Replace("   ", "\t");

                // Sanitize whitespace around tabs
                value = Regex.Replace(value, @"\s*\t\s*", "\t", RegexOptions.Compiled);
            }

            // If the value contains a newline
            value = value.Replace("\r\n", "\n");
            if (value.Contains('\n'))
            {
                output.Add(prefix + key + ":"); output.Add("");
                string[] values = value.Split('\n');
                foreach (string val in values)
                    output.Add(val);

                output.Add("");
            }

            // For all regular values
            else
            {
                output.Add(prefix + key + ": " + value);
            }
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">Output list</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(List<string> output, string key, string?[]? value, int indent)
        {
            // If there's no valid value to write
            if (value == null || value.Length == 0)
                return;

            AddIfExists(output, key, string.Join(", ", value), indent);
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">Output list</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(List<string> output, string key, long? value, int indent)
        {
            // If there's no valid value to write
            if (value == null || value == default(long))
                return;

            string prefix = string.Empty;
            for (int i = 0; i < indent; i++)
                prefix += "\t";

            output.Add(prefix + key + ": " + value);
        }

        /// <summary>
        /// Add the properly formatted key and value, if possible
        /// </summary>
        /// <param name="output">Output list</param>
        /// <param name="key">Name of the output key to write</param>
        /// <param name="value">Name of the output value to write</param>
        /// <param name="indent">Number of tabs to indent the line</param>
        private static void AddIfExists(List<string> output, string key, List<int>? value, int indent)
        {
            // If there's no valid value to write
            if (value == null || value.Count == 0)
                return;

            AddIfExists(output, key, string.Join(", ", value.Select(o => o.ToString()).ToArray()), indent);
        }

        /// <summary>
        /// Format a single site tag to string
        /// </summary>
        /// <param name="kvp">KeyValuePair representing the site tag and value</param>
        /// <returns>String-formatted tag and value</returns>
        private static string FormatSiteTag(KeyValuePair<SiteCode?, string> kvp)
        {
            bool isMultiLine = kvp.Key.IsMultiLine();
            string line = $"{kvp.Key.ShortName()}{(isMultiLine ? "\n" : " ")}";

            // Special case for boolean fields
            if (IsBoolean(kvp.Key))
            {
                if (kvp.Value != true.ToString())
                    return string.Empty;

                return line.Trim();
            }

            return $"{line}{kvp.Value}{(isMultiLine ? "\n" : string.Empty)}";
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
        /// TODO: Figure out why we have this and NormalizeDiscType as well
        private static string? GetFixedMediaType(MediaType? mediaType, string? picIdentifier, long? size, long? layerbreak, long? layerbreak2, long? layerbreak3)
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
                    else if (layerbreak != default && layerbreak != default(long) && picIdentifier == SabreTools.Models.PIC.Constants.DiscTypeIdentifierROMUltra)
                        return $"{mediaType.LongName()}-66";
                    else if (layerbreak != default && layerbreak != default(long) && size > 53_687_063_712)
                        return $"{mediaType.LongName()}-66";
                    else if (layerbreak != default && layerbreak != default(long))
                        return $"{mediaType.LongName()}-50";
                    else if (picIdentifier == SabreTools.Models.PIC.Constants.DiscTypeIdentifierROMUltra)
                        return $"{mediaType.LongName()}-33";
                    else if (size > 26_843_531_856)
                        return $"{mediaType.LongName()}-33";
                    else
                        return $"{mediaType.LongName()}-25";

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
        /// Check if a site code is boolean or not
        /// </summary>
        /// <param name="siteCode">SiteCode to check</param>
        /// <returns>True if the code field is a flag with no value, false otherwise</returns>
        /// <remarks>TODO: This should move to Extensions at some point</remarks>
        private static bool IsBoolean(SiteCode? siteCode)
        {
            return siteCode switch
            {
                SiteCode.PostgapType => true,
                SiteCode.VCD => true,
                _ => false,
            };
        }

        /// <summary>
        /// Order comment code tags according to Redump requirements
        /// </summary>
        /// <returns>Ordered list of KeyValuePairs representing the tags and values</returns>
        private static List<KeyValuePair<SiteCode?, string>> OrderCommentTags(Dictionary<SiteCode, string> tags)
        {
            var sorted = new List<KeyValuePair<SiteCode?, string>>();

            // If the input is invalid, just return an empty set
            if (tags == null || tags.Count == 0)
                return sorted;

            // Identifying Info
            if (tags.ContainsKey(SiteCode.AlternativeTitle))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.AlternativeTitle, tags[SiteCode.AlternativeTitle]));
            if (tags.ContainsKey(SiteCode.AlternativeForeignTitle))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.AlternativeForeignTitle, tags[SiteCode.AlternativeForeignTitle]));
            if (tags.ContainsKey(SiteCode.InternalName))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.InternalName, tags[SiteCode.InternalName]));
            if (tags.ContainsKey(SiteCode.InternalSerialName))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.InternalSerialName, tags[SiteCode.InternalSerialName]));
            if (tags.ContainsKey(SiteCode.VolumeLabel))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.VolumeLabel, tags[SiteCode.VolumeLabel]));
            if (tags.ContainsKey(SiteCode.Multisession))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Multisession, tags[SiteCode.Multisession]));
            if (tags.ContainsKey(SiteCode.UniversalHash))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.UniversalHash, tags[SiteCode.UniversalHash]));
            if (tags.ContainsKey(SiteCode.RingNonZeroDataStart))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.RingNonZeroDataStart, tags[SiteCode.RingNonZeroDataStart]));

            if (tags.ContainsKey(SiteCode.XMID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.XMID, tags[SiteCode.XMID]));
            if (tags.ContainsKey(SiteCode.XeMID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.XeMID, tags[SiteCode.XeMID]));
            if (tags.ContainsKey(SiteCode.DMIHash))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.DMIHash, tags[SiteCode.DMIHash]));
            if (tags.ContainsKey(SiteCode.PFIHash))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.PFIHash, tags[SiteCode.PFIHash]));
            if (tags.ContainsKey(SiteCode.SSHash))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.SSHash, tags[SiteCode.SSHash]));
            if (tags.ContainsKey(SiteCode.SSVersion))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.SSVersion, tags[SiteCode.SSVersion]));

            if (tags.ContainsKey(SiteCode.Filename))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Filename, tags[SiteCode.Filename]));

            if (tags.ContainsKey(SiteCode.BBFCRegistrationNumber))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.BBFCRegistrationNumber, tags[SiteCode.BBFCRegistrationNumber]));
            if (tags.ContainsKey(SiteCode.CDProjektID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.CDProjektID, tags[SiteCode.CDProjektID]));
            if (tags.ContainsKey(SiteCode.DiscHologramID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.DiscHologramID, tags[SiteCode.DiscHologramID]));
            if (tags.ContainsKey(SiteCode.DNASDiscID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.DNASDiscID, tags[SiteCode.DNASDiscID]));
            if (tags.ContainsKey(SiteCode.ISBN))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.ISBN, tags[SiteCode.ISBN]));
            if (tags.ContainsKey(SiteCode.ISSN))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.ISSN, tags[SiteCode.ISSN]));
            if (tags.ContainsKey(SiteCode.PPN))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.PPN, tags[SiteCode.PPN]));
            if (tags.ContainsKey(SiteCode.VFCCode))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.VFCCode, tags[SiteCode.VFCCode]));

            if (tags.ContainsKey(SiteCode.Genre))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Genre, tags[SiteCode.Genre]));
            if (tags.ContainsKey(SiteCode.Series))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Series, tags[SiteCode.Series]));
            if (tags.ContainsKey(SiteCode.PostgapType))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.PostgapType, tags[SiteCode.PostgapType]));
            if (tags.ContainsKey(SiteCode.VCD))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.VCD, tags[SiteCode.VCD]));

            // Publisher / Company IDs
            if (tags.ContainsKey(SiteCode.AcclaimID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.AcclaimID, tags[SiteCode.AcclaimID]));
            if (tags.ContainsKey(SiteCode.ActivisionID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.ActivisionID, tags[SiteCode.ActivisionID]));
            if (tags.ContainsKey(SiteCode.BandaiID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.BandaiID, tags[SiteCode.BandaiID]));
            if (tags.ContainsKey(SiteCode.ElectronicArtsID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.ElectronicArtsID, tags[SiteCode.ElectronicArtsID]));
            if (tags.ContainsKey(SiteCode.FoxInteractiveID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.FoxInteractiveID, tags[SiteCode.FoxInteractiveID]));
            if (tags.ContainsKey(SiteCode.GTInteractiveID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.GTInteractiveID, tags[SiteCode.GTInteractiveID]));
            if (tags.ContainsKey(SiteCode.JASRACID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.JASRACID, tags[SiteCode.JASRACID]));
            if (tags.ContainsKey(SiteCode.KingRecordsID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.KingRecordsID, tags[SiteCode.KingRecordsID]));
            if (tags.ContainsKey(SiteCode.KoeiID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.KoeiID, tags[SiteCode.KoeiID]));
            if (tags.ContainsKey(SiteCode.KonamiID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.KonamiID, tags[SiteCode.KonamiID]));
            if (tags.ContainsKey(SiteCode.LucasArtsID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.LucasArtsID, tags[SiteCode.LucasArtsID]));
            if (tags.ContainsKey(SiteCode.MicrosoftID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.MicrosoftID, tags[SiteCode.MicrosoftID]));
            if (tags.ContainsKey(SiteCode.NaganoID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.NaganoID, tags[SiteCode.NaganoID]));
            if (tags.ContainsKey(SiteCode.NamcoID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.NamcoID, tags[SiteCode.NamcoID]));
            if (tags.ContainsKey(SiteCode.NipponIchiSoftwareID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.NipponIchiSoftwareID, tags[SiteCode.NipponIchiSoftwareID]));
            if (tags.ContainsKey(SiteCode.OriginID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.OriginID, tags[SiteCode.OriginID]));
            if (tags.ContainsKey(SiteCode.PonyCanyonID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.PonyCanyonID, tags[SiteCode.PonyCanyonID]));
            if (tags.ContainsKey(SiteCode.SegaID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.SegaID, tags[SiteCode.SegaID]));
            if (tags.ContainsKey(SiteCode.SelenID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.SelenID, tags[SiteCode.SelenID]));
            if (tags.ContainsKey(SiteCode.SierraID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.SierraID, tags[SiteCode.SierraID]));
            if (tags.ContainsKey(SiteCode.TaitoID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.TaitoID, tags[SiteCode.TaitoID]));
            if (tags.ContainsKey(SiteCode.UbisoftID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.UbisoftID, tags[SiteCode.UbisoftID]));
            if (tags.ContainsKey(SiteCode.ValveID))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.ValveID, tags[SiteCode.ValveID]));

            return sorted;
        }

        /// <summary>
        /// Order content code tags according to Redump requirements
        /// </summary>
        /// <returns>Ordered list of KeyValuePairs representing the tags and values</returns>
        private static List<KeyValuePair<SiteCode?, string>> OrderContentTags(Dictionary<SiteCode, string> tags)
        {
            var sorted = new List<KeyValuePair<SiteCode?, string>>();

            // If the input is invalid, just return an empty set
            if (tags == null || tags.Count == 0)
                return sorted;

            // Games
            if (tags.ContainsKey(SiteCode.Games))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Games, tags[SiteCode.Games]));
            if (tags.ContainsKey(SiteCode.NetYarozeGames))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.NetYarozeGames, tags[SiteCode.NetYarozeGames]));

            // Demos
            if (tags.ContainsKey(SiteCode.PlayableDemos))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.PlayableDemos, tags[SiteCode.PlayableDemos]));
            if (tags.ContainsKey(SiteCode.RollingDemos))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.RollingDemos, tags[SiteCode.RollingDemos]));
            if (tags.ContainsKey(SiteCode.TechDemos))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.TechDemos, tags[SiteCode.TechDemos]));

            // Video
            if (tags.ContainsKey(SiteCode.GameFootage))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.GameFootage, tags[SiteCode.GameFootage]));
            if (tags.ContainsKey(SiteCode.Videos))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Videos, tags[SiteCode.Videos]));

            // Miscellaneous
            if (tags.ContainsKey(SiteCode.Patches))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Patches, tags[SiteCode.Patches]));
            if (tags.ContainsKey(SiteCode.Savegames))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Savegames, tags[SiteCode.Savegames]));
            if (tags.ContainsKey(SiteCode.Extras))
                sorted.Add(new KeyValuePair<SiteCode?, string>(SiteCode.Extras, tags[SiteCode.Extras]));

            return sorted;
        }

        #endregion
    }
}
