using System;
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
        [InlineData(DiscSubpath.Changes, "http://redump.org/disc/1/changes/")]
        [InlineData(DiscSubpath.Cuesheet, "http://redump.org/disc/1/cue/")]
        [InlineData(DiscSubpath.Edit, "http://redump.org/disc/1/edit/")]
        [InlineData(DiscSubpath.GDI, "http://redump.org/disc/1/gdi/")]
        [InlineData(DiscSubpath.Key, "http://redump.org/disc/1/key/")]
        [InlineData(DiscSubpath.LSD, "http://redump.org/disc/1/lsd/")]
        [InlineData(DiscSubpath.MD5, "http://redump.org/disc/1/md5/")]
        [InlineData(DiscSubpath.SBI, "http://redump.org/disc/1/sbi/")]
        [InlineData(DiscSubpath.SFV, "http://redump.org/disc/1/sfv/")]
        [InlineData(DiscSubpath.SHA1, "http://redump.org/disc/1/sha1/")]
        [InlineData(null, "http://redump.org/disc/1/")]
        public void BuildDiscUrl_Subpath_Builds(DiscSubpath? subpath, string expected)
        {
            string actual = UrlBuilder.BuildDiscUrl(1, subpath);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region BuildDiscsUrl

        [Fact]
        public void BuildDiscsUrl_DumperWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(dumper: "user", page: 3);
            Assert.Equal("http://redump.org/discs/dumper/user/?page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_DumperLastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(dumper: "user", sort: SortCategory.Modified, sortDir: SortDirection.Descending, page: 3);
            Assert.Equal("http://redump.org/discs/dumper/user/sort/modified/dir/desc/?page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_LastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(sort: SortCategory.Modified, sortDir: SortDirection.Descending, page: 3);
            Assert.Equal("http://redump.org/discs/sort/modified/dir/desc/?page=3", actual);
        }

        [Theory]
        [InlineData(true, true, true, "http://redump.org/discs/comments/only/")]
        [InlineData(false, true, true, "http://redump.org/discs/contents/only/")]
        [InlineData(false, false, true, "http://redump.org/discs/protection/only/")]
        [InlineData(false, false, false, "http://redump.org/discs/")]
        public void BuildDiscsUrl_SingleOnlyFilterAllowedAtMost(bool comments, bool contents, bool protection, string expected)
        {
            string actual = UrlBuilder.BuildDiscsUrl(comments: comments, contents: contents, protection: protection);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "http://redump.org/discs/quicksearch//?page=3")]
        [InlineData("simple", "http://redump.org/discs/quicksearch/simple/?page=3")]
        [InlineData("search-format", "http://redump.org/discs/quicksearch/search-format/?page=3")]
        [InlineData("invalid format", "http://redump.org/discs/quicksearch/invalid format/?page=3")]
        [InlineData("extra/path", "http://redump.org/discs/quicksearch/extra/path/?page=3")]
        public void BuildDiscsUrl_QuicksearchWithPages_Builds(string query, string expected)
        {
            string actual = UrlBuilder.BuildDiscsUrl(quicksearch: query, page: 3);
            Assert.Equal(expected, actual);
        }

        // TODO: Implement more extensive tests

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
        [InlineData(null, "http://redump.org/list/user/arch/")]
        public void BuildListUrl_HaveMissSet(bool? have, string expected)
        {
            string actual = UrlBuilder.BuildListUrl("user", have, system: RedumpSystem.AcornArchimedes);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildListUrl_InvalidUsername_Builds()
        {
            string actual = UrlBuilder.BuildListUrl(string.Empty, have: true, system: RedumpSystem.AcornArchimedes);
            Assert.Equal("http://redump.org/list/have//arch/", actual);
        }

        [Theory]
        [InlineData(RedumpSystem.MarkerOtherEnd)]
        [InlineData(RedumpSystem.RainbowDisc)]
        public void BuildListUrl_InvalidSystem_Builds(RedumpSystem? system)
        {
            string actual = UrlBuilder.BuildListUrl("user", have: true, system: system);
            Assert.Equal("http://redump.org/list/have/user/", actual);
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

        [Theory]
        [InlineData(PackType.Cuesheets, "http://redump.org/cues/arch/")]
        [InlineData(PackType.Datfile, "http://redump.org/datfile/arch/")]
        [InlineData(PackType.DecryptedKeys, "http://redump.org/dkeys/arch/")]
        [InlineData(PackType.Gdis, "http://redump.org/gdi/arch/")]
        [InlineData(PackType.Keys, "http://redump.org/keys/arch/")]
        [InlineData(PackType.Lsds, "http://redump.org/lsd/arch/")]
        [InlineData(PackType.Sbis, "http://redump.org/sbi/arch/")]
        public void BuildPackUrl_ValidPackType_ValidSystem_Builds(PackType packType, string expected)
        {
            string actual = UrlBuilder.BuildPackUrl(packType, RedumpSystem.AcornArchimedes);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildPackUrl_InvalidPackType_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => UrlBuilder.BuildPackUrl((PackType)int.MaxValue, RedumpSystem.AcornArchimedes));
        }

        [Fact]
        public void BuildPackUrl_InvalidSystem_Builds()
        {
            string actual = UrlBuilder.BuildPackUrl(PackType.Datfile, RedumpSystem.MarkerOtherEnd);
            Assert.Equal("http://redump.org/datfile//", actual);
        }

        #endregion

        #region BuildStatisticsUrl

        [Fact]
        public void BuildStatisticsUrl_Constant()
        {
            string actual = UrlBuilder.BuildStatisticsUrl();
            Assert.Equal("http://redump.org/statistics/", actual);
        }

        #endregion

    }
}
