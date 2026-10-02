using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class DownloadsTests
    {
        #region BuildUrl

        [Theory]
        [InlineData(null, "https://redump.info/downloads")]
        [InlineData(true, "https://redump.info/downloads/database")]
        [InlineData(false, "https://redump.info/downloads")]
        public void BuildUrl_Builds(bool? database, string expected)
        {
            var endpoint = new RedumpLib.Endpoints.Downloads { Database = database };
            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
