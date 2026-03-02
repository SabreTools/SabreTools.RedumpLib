using SabreTools.RedumpLib.Web;
using Xunit;

namespace SabreTools.RedumpLib.Test.Web
{
    public class UrlBuilderTests
    {
        #region BuildDiscUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildDiscUrl_AlwaysPositive(int id, int expected)
        {
            string actual = UrlBuilder.BuildDiscUrl(id);
            Assert.Equal($"http://redump.org/disc/{expected}/", actual);
        }

        // TODO: Implement

        #endregion

        #region BuildDiscsUrl

        // TODO: Implement

        #endregion

        #region BuildDiscsWipUrl

        [Fact]
        public void BuildDiscsWipUrl_Constant()
        {
            string actual = UrlBuilder.BuildDiscsWipUrl();
            Assert.Equal("http://redump.org/discs-wip/", actual);
        }

        #endregion

        #region BuildDownloadsUrl

        [Fact]
        public void BuildDownloadsUrl_Constant()
        {
            string actual = UrlBuilder.BuildDownloadsUrl();
            Assert.Equal("http://redump.org/downloads/", actual);
        }

        #endregion

        #region BuildListUrl

        // TODO: Implement

        #endregion

        #region BuildMemberPromotionUrl

        [Fact]
        public void BuildMemberPromotionUrl_Constant()
        {
            string actual = UrlBuilder.BuildMemberPromotionUrl();
            Assert.Equal("http://redump.org/member2dumper/", actual);
        }

        #endregion

        #region BuildNewDiscUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildNewDiscUrl_AlwaysPositive(int id, int expected)
        {
            string actual = UrlBuilder.BuildNewDiscUrl(id);
            Assert.Equal($"http://redump.org/newdisc/{expected}/", actual);
        }

        #endregion

        #region BuildPackUrl

        // TODO: Implement

        #endregion
    }
}
