using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Attributes
{
    /// <summary>
    /// Attribute specifc to Redump System values
    /// </summary>
    public class SystemAttribute : HumanReadableAttribute
    {
        /// <summary>
        /// redump.org short code
        /// </summary>
        public string? RedumpOrgCode { get; set; }

        /// <summary>
        /// Category for the system
        /// </summary>
        public SystemCategory Category { get; set; }

        /// <summary>
        /// System is restricted to dumpers
        /// </summary>
        /// <remarks>redump.org only</remarks>
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
        /// System has a decrypted keys pack
        /// </summary>
        /// <remarks>redump.org only</remarks>
        public bool HasDkeys { get; set; } = false;

        /// <summary>
        /// System has a GDI pack
        /// </summary>
        /// <remarks>redump.org only</remarks>
        public bool HasGdi { get; set; } = false;

        /// <summary>
        /// System has a keys pack
        /// </summary>
        public bool HasKeys { get; set; } = false;

        /// <summary>
        /// System has an LSD pack
        /// </summary>
        /// <remarks>redump.org only</remarks>
        public bool HasLsd { get; set; } = false;

        /// <summary>
        /// System has an SBI pack
        /// </summary>
        public bool HasSbi { get; set; } = false;
    }
}
