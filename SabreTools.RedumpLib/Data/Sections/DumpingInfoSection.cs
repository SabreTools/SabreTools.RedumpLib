using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Dumping info section for moderation
    /// </summary>
    /// <remarks>These do not map to existing fields in a submission form</remarks>
    public class DumpingInfoSection : ICloneable
    {
        [JsonProperty(PropertyName = "frontend_version", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? FrontendVersion { get; set; }

        [JsonProperty(PropertyName = "dumping_program", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingProgram { get; set; }

        [JsonProperty(PropertyName = "dumping_date", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingDate { get; set; }

        [JsonProperty(PropertyName = "dumping_params", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingParameters { get; set; }

        [JsonProperty(PropertyName = "drive_manufacturer", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Manufacturer { get; set; }

        [JsonProperty(PropertyName = "drive_model", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Model { get; set; }

        [JsonProperty(PropertyName = "drive_firmware", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Firmware { get; set; }

        [JsonProperty(PropertyName = "reported_disc_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? ReportedDiscType { get; set; }

        [JsonProperty(PropertyName = "errors_c2", NullValueHandling = NullValueHandling.Ignore)]
        public string? C2ErrorsCount { get; set; }

        public object Clone()
        {
            return new DumpingInfoSection
            {
                FrontendVersion = this.FrontendVersion,
                DumpingProgram = this.DumpingProgram,
                DumpingDate = this.DumpingDate,
                DumpingParameters = this.DumpingParameters,
                Manufacturer = this.Manufacturer,
                Model = this.Model,
                Firmware = this.Firmware,
                ReportedDiscType = this.ReportedDiscType,
                C2ErrorsCount = this.C2ErrorsCount,
            };
        }
    }
}
