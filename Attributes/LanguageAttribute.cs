namespace SabreTools.RedumpLib.Attributes
{
    /// <summary>
    /// Attribute specifc to Language values
    /// </summary>
    /// <remarks>
    /// Some languages have multiple proper names. Should all be supported?
    /// </remarks>
    public class LanguageAttribute : HumanReadableAttribute
    {
        /// <summary>
        /// ISO 639-1 Code
        /// </summary>
#if NET48
        public string TwoLetterCode { get; set; } = null;
#else
        public string? TwoLetterCode { get; set; }
#endif

        /// <summary>
        /// ISO 639-2 Code (Standard or Bibliographic)
        /// </summary>
#if NET48
        public string ThreeLetterCode { get; set; } = null;
#else
        public string? ThreeLetterCode { get; set; }
#endif

        /// <summary>
        /// ISO 639-2 Code (Terminology)
        /// </summary>
#if NET48
        public string ThreeLetterCodeAlt { get; set; } = null;
#else
        public string? ThreeLetterCodeAlt { get; set; }
#endif
    }
}