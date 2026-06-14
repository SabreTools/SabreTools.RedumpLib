using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
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
