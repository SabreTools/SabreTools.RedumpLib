namespace SabreTools.RedumpLib.Attributes
{
    /// <summary>
    /// Attribute specifc to Region values
    /// </summary>
    public class RegionCodeAttribute : HumanReadableAttribute
    {
        /// <summary>
        /// redump.org short code
        /// </summary>
        public string? RedumpOrgCode { get; set; }
    }
}
