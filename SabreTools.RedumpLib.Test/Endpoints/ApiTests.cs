using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class ApiTests
    {
        #region BuildUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildUrl_DiscSubpath_AlwaysPositive(int id, int expected)
        {
            var endpoint = new RedumpLib.Endpoints.Api
            {
                Subpath = ApiSubpath.Disc,
                Id = id,
            };
            string actual = endpoint.BuildUrl();
            Assert.Equal($"https://redump.info/api/disc?id={expected}", actual);
        }

        [Fact]
        public void BuildUrl_DiscsSubpath_Builds()
        {
            var endpoint = new RedumpLib.Endpoints.Api
            {
                Subpath = ApiSubpath.Discs,
                Sha1 = "sha1-value",
                Md5 = "md5-value",
                Crc32 = "crc32-value",
            };

            // SHA-1 takes precedence
            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/api/discs?sha1=sha1-value", actual);

            // Then MD5
            endpoint.Sha1 = null;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/api/discs?md5=md5-value", actual);

            // Then CRC-32
            endpoint.Md5 = null;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/api/discs?crc32=crc32-value", actual);
        }

        #endregion
    }
}
