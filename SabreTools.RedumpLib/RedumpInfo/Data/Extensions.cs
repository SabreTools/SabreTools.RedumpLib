using System;
using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    public static class Extensions
    {
        #region Disc Subpath

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="discSubpath">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DiscSubpath? ToDiscSubpath(this string? discSubpath)
        {
            // No value means no match
            if (discSubpath is null || discSubpath.Length == 0)
                return null;

            discSubpath = discSubpath.ToLowerInvariant();
            var discSubpaths = (DiscSubpath[])Enum.GetValues(typeof(DiscSubpath));

            // Check short names
            int index = Array.FindIndex(discSubpaths, s => discSubpath == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            // Check long names
            index = Array.FindIndex(discSubpaths, s => discSubpath == s.LongName()?.ToLowerInvariant()
                || discSubpath == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            return null;
        }

        #endregion

        #region Dump Status

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="dumpStatus">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DumpStatus? ToDumpStatus(this string? dumpStatus)
        {
            // No value means no match
            if (dumpStatus is null || dumpStatus.Length == 0)
                return null;

            dumpStatus = dumpStatus.ToLowerInvariant();
            var dumpStatuses = (DumpStatus[])Enum.GetValues(typeof(DumpStatus));

            // Check short names
            int index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check long names
            index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.LongName()?.ToLowerInvariant()
                || dumpStatus == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check numeric values
            if (int.TryParse(dumpStatus, out int dumpStatusInt) && Enum.IsDefined(typeof(DumpStatus), dumpStatusInt))
                return (DumpStatus)dumpStatusInt;

            return null;
        }

        #endregion
    }
}
