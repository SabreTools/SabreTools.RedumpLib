namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Template field values for submission info
    /// </summary>
    internal static class Template
    {
        #region Matching Information

        public const string FullyMatchingIDsField = "Fully Matching IDs";
        public const string PartiallyMatchingIDsField = "Partially Matching IDs";

        #endregion

        #region Disc Identity

        public const string SystemField = "System";
        public const string MediaTypeField = "Media Type";
        public const string CategoryField = "Category";
        public const string TitleField = "Title";
        public const string ForeignTitleField = "Foreign Title";
        public const string DiscNumberField = "Disc Number";
        public const string DiscTitleField = "Disc Title";
        public const string FilenameSuffixField = "Filename Suffix";

        #endregion

        #region Regions and Languages

        public const string RegionsField = "Regions";
        public const string LanguagesField = "Languages";

        #endregion

        #region Disc Identifiers

        public const string DiscSerialsField = "Disc Serials";
        public const string EditionsField = "Editions";
        public const string BarcodesField = "Barcode";
        public const string VersionField = "Version";
        public const string ErrorCountField = "Error Count";
        public const string EXEDate = "EXE Date";
        public const string EDCField = "EDC";
        public const string LayerbreakField = "Layerbreak";
        public const string DiscIDField = "Disc ID";
        public const string DiscKeyField = "Disc Key";
        public const string UniversalHashField = "Universal Hash";

        #endregion

        #region Ring Codes

        public const string MasteringCodeField = "Mastering Code";
        public const string MasteringSIDField = "Mastering SID";
        public const string ToolstampsField = "Toolstamps";
        public const string MouldSIDsField = "Mould SIDs";
        public const string AdditionalMouldsField = "Additional Moulds";
        public const string WriteOffsetField = "Write Offset";
        public const string SampleStartField = "Sample Start";

        #endregion

        #region Dump Metadata

        public const string CommentsField = "Comments";
        public const string ContentsField = "Contents";
        public const string ProtectionField = "Protection";
        public const string SectorRangesField = "Sector Ranges";
        public const string SBIField = "SBI";
        public const string PVDField = "Primary Volume Descriptor (PVD)";
        public const string HeaderField = "Header";
        public const string BCAField = "Burst Cutting Area (BCA)";
        public const string PICField = "Permanent Information & Control (PIC)";
        public const string CuesheetField = "Cuesheet";
        public const string DatField = "Dat";

        #endregion

        #region Submission Controls

        public const string DumpLogField = "Dump Log";
        public const string LogsArchiveURLField = "Logs Archive URL";
        public const string ReviewCommentField = "Review Comment";
        public const string SubmissionCommentField = "Submission Comment";
        public const string SubmitAsField = "Submit As";

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

        #region Redump.org only

        public const string PlayStationAntiModchipField = "Anti-modchip";
        public const string PlaystationLanguageSelectionViaField = "Language Selection Via";
        public const string PlayStationLibCryptField = "LibCrypt";

        #endregion
    }
}
