using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Size &amp; checksums section of New Disc form (DVD/BD/UMD-based)
    /// </summary>
    public class SizeAndChecksumsSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_layerbreak", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak { get; set; }

        [JsonProperty(PropertyName = "d_layerbreak_2", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak2 { get; set; }

        [JsonProperty(PropertyName = "d_layerbreak_3", NullValueHandling = NullValueHandling.Ignore)]
        public long Layerbreak3 { get; set; }

        [JsonProperty(PropertyName = "d_pic_identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string? PICIdentifier { get; set; }

        [JsonProperty(PropertyName = "d_size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "d_crc32", NullValueHandling = NullValueHandling.Ignore)]
        public string? CRC32 { get; set; }

        [JsonProperty(PropertyName = "d_md5", NullValueHandling = NullValueHandling.Ignore)]
        public string? MD5 { get; set; }

        [JsonProperty(PropertyName = "d_sha1", NullValueHandling = NullValueHandling.Ignore)]
        public string? SHA1 { get; set; }

        public object Clone()
        {
            return new SizeAndChecksumsSection
            {
                Layerbreak = this.Layerbreak,
                Layerbreak2 = this.Layerbreak2,
                Layerbreak3 = this.Layerbreak3,
                PICIdentifier = this.PICIdentifier,
                Size = this.Size,
                CRC32 = this.CRC32,
                MD5 = this.MD5,
                SHA1 = this.SHA1,
            };
        }
    }
}
