using SabreTools.RedumpLib.Attributes;
using SabreTools.RedumpLib.RedumpInfo.Data;

namespace SabreTools.RedumpLib.RedumpInfo
{
    /// <summary>
    /// Attribute specifc to Redump System values
    /// </summary>
    public class SystemAttribute : HumanReadableAttribute
    {
        /// <summary>
        /// Category for the system
        /// </summary>
        public SystemCategory Category { get; set; }

        /// <summary>
        /// System is restricted to dumpers
        /// </summary>
        public bool IsBanned { get; set; } = false;

        /// <summary>
        /// System has a CUE pack
        /// </summary>
        public bool HasCues { get; set; } = false;

        /// <summary>
        /// System has a DAT
        /// </summary>
        public bool HasDat { get; set; } = false;

        /// <summary>
        /// System has a keys pack
        /// </summary>
        public bool HasKeys { get; set; } = false;

        /// <summary>
        /// System has an SBI pack
        /// </summary>
        public bool HasSbi { get; set; } = false;
    }
}
