using System;

namespace SabreTools.RedumpLib.Attributes
{
    /// <summary>
    /// Generic attribute for human readable values
    /// </summary>
    public class HumanReadableAttribute : Attribute
    {
        /// <summary>
        /// Item is marked as obsolete or unusable
        /// </summary>
        public bool Available { get; set; } = true;

        /// <summary>
        /// Human-readable name of the item
        /// </summary>
#if NET48
        public string LongName { get; set; }
#else
        public string? LongName { get; set; }
#endif

        /// <summary>
        /// Internally used name of the item
        /// </summary>
#if NET48
        public string ShortName { get; set; }
#else
        public string? ShortName { get; set; }
#endif
    }
}