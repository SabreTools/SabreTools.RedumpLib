namespace SabreTools.RedumpLib.Legacy.Data
{
    /// <summary>
    /// Template field values for submission info
    /// </summary>
    internal static class Template
    {
        #region Common Disc Info

        public const string TitleField = "Title";
        public const string ForeignTitleNonLatinField = "Foreign Title (Non-Latin)";
        public const string DiscNumberLetterField = "Disc Number/Letter";
        public const string DiscTitleField = "Disc Title";
        public const string SystemField = "System";
        public const string MediaTypeField = "Media Type";
        public const string CategoryField = "Category";
        public const string FullyMatchingIDField = "Fully Matching ID";
        public const string PartiallyMatchingIDsField = "Partially Matching IDs";
        public const string RegionField = "Region";
        public const string LanguagesField = "Languages";
        public const string PlaystationLanguageSelectionViaField = "Language Selection Via";
        public const string SerialField = "Serial";

        #region Ringcode Information

        public const string MasteringRingField = "Mastering Ring";
        public const string MasteringSIDField = "Mastering SID";
        public const string ToolstampMasteringCodeField = "Toolstamp/Mastering Code";
        public const string MouldSIDField = "Mould SID";
        public const string AdditionalMouldField = "Additional Mould";
        public const string WriteOffsetField = "Write Offset";

        #endregion

        public const string BarcodeField = "Barcode";
        public const string EXEDateBuildDate = "EXE/Build Date";
        public const string ErrorsCountField = "Errors Count";
        public const string CommentsField = "Comments";
        public const string ContentsField = "Contents";

        #endregion

        #region Version and Editions

        public const string VersionField = "Version";
        public const string EditionsField = "Editions";

        #endregion

        #region EDC

        public const string EDCField = "EDC";

        #endregion

        #region Extras

        public const string PVDField = "Primary Volume Descriptor (PVD)";
        public const string DiscKeyField = "Disc Key";
        public const string DiscIDField = "Disc ID";
        public const string PICField = "Permanent Information & Control (PIC)";
        public const string HeaderField = "Header";
        public const string BCAField = "Burst Cutting Area (BCA)";
        public const string SecuritySectorRangesField = "Security Sector Ranges";

        #endregion

        #region Copy Protection

        public const string PlayStationAntiModchipField = "Anti-modchip";
        public const string PlayStationLibCryptField = "LibCrypt";
        public const string LibCryptDataField = "LibCrypt Data";
        public const string ProtectionField = "Protection";
        public const string SecuROMDataField = "SecuROM Data";

        #endregion

        #region Tracks and Write Offsets

        public const string ClrMameProDataField = "ClrMamePro Data (DAT)";
        public const string CuesheetField = "Cuesheet";

        #endregion

        #region Size and Checksums

        public const string LayerbreakField = "Layerbreak";

        #endregion

        #region Dumping Info

        public const string FrontendVersionField = "Frontend Version";
        public const string DumpingProgramField = "Dumping Program";
        public const string DumpingDateField = "Date";
        public const string DumpingParametersField = "Parameters";
        public const string DumpingDriveManufacturer = "Manufacturer";
        public const string DumpingDriveModel = "Model";
        public const string DumpingDriveFirmware = "Firmware";
        public const string ReportedDiscType = "Reported Disc Type";
        public const string C2ErrorCount = "C2 Error Count";

        #endregion

        #region Default values

        public const string RequiredValue = "(REQUIRED)";
        public const string RequiredIfExistsValue = "(REQUIRED, IF EXISTS)";
        public const string OptionalValue = "(OPTIONAL)";
        public const string DiscNotDetected = "Disc Not Detected";

        #endregion
    }
}
