using SabreTools.RedumpLib.Data;
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

        [Theory]
        [InlineData(true, true, true, true, true, true, true, true, true, true, "http://redump.org/disc/1/changes/")]
        [InlineData(false, true, true, true, true, true, true, true, true, true, "http://redump.org/disc/1/cue/")]
        [InlineData(false, false, true, true, true, true, true, true, true, true, "http://redump.org/disc/1/edit/")]
        [InlineData(false, false, false, true, true, true, true, true, true, true, "http://redump.org/disc/1/gdi/")]
        [InlineData(false, false, false, false, true, true, true, true, true, true, "http://redump.org/disc/1/key/")]
        [InlineData(false, false, false, false, false, true, true, true, true, true, "http://redump.org/disc/1/lsd/")]
        [InlineData(false, false, false, false, false, false, true, true, true, true, "http://redump.org/disc/1/md5/")]
        [InlineData(false, false, false, false, false, false, false, true, true, true, "http://redump.org/disc/1/sbi/")]
        [InlineData(false, false, false, false, false, false, false, false, true, true, "http://redump.org/disc/1/sfv/")]
        [InlineData(false, false, false, false, false, false, false, false, false, true, "http://redump.org/disc/1/sha1/")]
        [InlineData(false, false, false, false, false, false, false, false, false, false, "http://redump.org/disc/1/")]
        public void BuildDiscUrl_SingleSubpathAllowedAtMost(bool changes, bool cue, bool edit, bool gdi, bool key, bool lsd, bool md5, bool sbi, bool sfv, bool sha1, string expected)
        {
            string actual = UrlBuilder.BuildDiscUrl(1, changes, cue, edit, gdi, key, lsd, md5, sbi, sfv, sha1);
            Assert.Equal(expected, actual);
        }

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

        [Theory]
        [InlineData(true, "http://redump.org/list/have/user/arch/")]
        [InlineData(false, "http://redump.org/list/miss/user/arch/")]
        public void BuildListUrl_HaveMissSet(bool have, string expected)
        {
            string actual = UrlBuilder.BuildListUrl(have, "user", RedumpSystem.AcornArchimedes);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildListUrl_InvalidUsername_Builds()
        {
            string actual = UrlBuilder.BuildListUrl(true, string.Empty, RedumpSystem.AcornArchimedes);
            Assert.Equal("http://redump.org/list/have//arch/", actual);
        }

        [Fact]
        public void BuildListUrl_InvalidSystem_Builds()
        {
            string actual = UrlBuilder.BuildListUrl(true, "user", RedumpSystem.MarkerOtherEnd);
            Assert.Equal("http://redump.org/list/have/user//", actual);
        }

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
