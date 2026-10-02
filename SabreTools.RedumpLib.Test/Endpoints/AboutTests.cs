using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class AboutTests
    {
        #region BuildUrl

        [Fact]
        public void BuildAboutUrl_Constant()
        {
            var endpoint = new RedumpLib.Endpoints.About();

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/about", actual);
        }

        #endregion
    }
}
