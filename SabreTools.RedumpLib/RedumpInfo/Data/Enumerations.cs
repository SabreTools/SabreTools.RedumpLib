using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
    /// <summary>
    /// List of all disc subpaths
    /// </summary>
    public enum DiscSubpath
    {
        [HumanReadable(LongName = "Cuesheet", ShortName = "cue")]
        Cuesheet,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit,

        // Placeholder for the linked queue history page, not an actual subpath
        [HumanReadable(LongName = "History", ShortName = "history")]
        History,
    }

    /// <summary>
    /// Dump status
    /// </summary>
    public enum DumpStatus
    {
        // TODO: Verify new naming
        [HumanReadable(LongName = "Unknown", ShortName = "grey")]
        UnknownGrey = 1,

        // TODO: Verify new naming
        [HumanReadable(LongName = "Bad Dump", ShortName = "red")]
        BadDumpRed = 2,

        [HumanReadable(LongName = "Questionable", ShortName = "yellow")]
        Questionable,

        [HumanReadable(LongName = "Unverified", ShortName = "blue")]
        Unverified,

        [HumanReadable(LongName = "Verified", ShortName = "green")]
        Verified,
    }
}
