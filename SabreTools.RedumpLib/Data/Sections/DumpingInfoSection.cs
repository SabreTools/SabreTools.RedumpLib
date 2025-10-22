using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Dumping info section for moderation
    /// </summary>
    public class DumpingInfoSection : ICloneable
    {
        // Name not defined by Redump -- Only used with MPF
        [JsonProperty(PropertyName = "d_frontend_version", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? FrontendVersion { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_program", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingProgram { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_date", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingDate { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_dumping_params", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpingParameters { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_manufacturer", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Manufacturer { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_model", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Model { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_drive_firmware", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? Firmware { get; set; }

        // Name not defined by Redump
        [JsonProperty(PropertyName = "d_reported_disc_type", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? ReportedDiscType { get; set; }

        // Name not defined by Redump -- Only used with Redumper
        [JsonProperty(PropertyName = "d_errors_c2", NullValueHandling = NullValueHandling.Ignore)]
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
