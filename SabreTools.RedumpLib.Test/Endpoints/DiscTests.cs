using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class DiscTests
    {
        #region BuildUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildUrl_AlwaysPositive(int id, int expected)
        {
            var endpoint = new RedumpLib.Endpoints.Disc { Id = id };
            string actual = endpoint.BuildUrl();
            Assert.Equal($"https://redump.info/disc/{expected}", actual);
        }

        [Theory]
        [InlineData(null, "https://redump.info/disc/1")]
        [InlineData(DiscSubpath.Cuesheet, "https://redump.info/disc/1/cue")]
        [InlineData(DiscSubpath.Edit, "https://redump.info/disc/1/edit")]
        [InlineData(DiscSubpath.SBI, "https://redump.info/disc/1/sbi")]
        public void BuildUrl_Subpath_Builds(DiscSubpath? subpath, string expected)
        {
            var endpoint = new RedumpLib.Endpoints.Disc
            {
                Id = 1,
                Subpath = subpath,
            };
            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
