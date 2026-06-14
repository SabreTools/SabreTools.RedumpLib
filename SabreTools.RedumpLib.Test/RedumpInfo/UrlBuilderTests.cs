using System;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpInfo;
using SabreTools.RedumpLib.RedumpInfo.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.RedumpInfo
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
            Assert.Equal($"https://redump.info/disc/{expected}/", actual);
        }

        [Theory]
        [InlineData(DiscSubpath.Cuesheet, "https://redump.info/disc/1/cue")]
        [InlineData(DiscSubpath.Edit, "https://redump.info/disc/1/edit/")]
        [InlineData(null, "https://redump.info/disc/1/")]
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
            Assert.Equal("https://redump.info/discs/?dumper=user&page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_DumperLastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(dumper: "user", sort: RedumpLib.RedumpOrg.Data.SortCategory.Modified, sortDir: RedumpLib.RedumpOrg.Data.SortDirection.Descending, page: 3);
            Assert.Equal("https://redump.info/discs/?dumper=user&sort=modified&order=desc&page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_LastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(sort: RedumpLib.RedumpOrg.Data.SortCategory.Modified, sortDir: RedumpLib.RedumpOrg.Data.SortDirection.Descending, page: 3);
            Assert.Equal("https://redump.info/discs/?sort=modified&order=desc&page=3", actual);
        }

        [Theory]
        [InlineData("", "https://redump.info/discs/?q=&page=3")]
        [InlineData("simple", "https://redump.info/discs/?q=simple&page=3")]
        [InlineData("search-format", "https://redump.info/discs/?q=search-format&page=3")]
        [InlineData("invalid format", "https://redump.info/discs/?q=invalid format&page=3")]
        [InlineData("extra/path", "https://redump.info/discs/?q=extra/path&page=3")]
        public void BuildDiscsUrl_QuicksearchWithPages_Builds(string query, string expected)
        {
            string actual = UrlBuilder.BuildDiscsUrl(quicksearch: query, page: 3);
            Assert.Equal(expected, actual);
        }

        // TODO: Implement more extensive tests

        #endregion

        #region BuildDownloadsUrl

        [Fact]
        public void BuildDownloadsUrl_Constant()
        {
            string actual = UrlBuilder.BuildDownloadsUrl();
            Assert.Equal("https://redump.info/downloads/", actual);
        }

        #endregion

        #region BuildPackUrl

        [Theory]
        [InlineData(PackType.Cuesheets, "https://redump.info/cues/ARCH/")]
        [InlineData(PackType.Datfile, "https://redump.info/datfile/ARCH/")]
        [InlineData(PackType.Keys, "https://redump.info/keys/ARCH/")]
        [InlineData(PackType.Sbis, "https://redump.info/sbi/ARCH/")]
        public void BuildPackUrl_ValidPackType_ValidSystem_Builds(PackType packType, string expected)
        {
            string actual = UrlBuilder.BuildPackUrl(packType, PhysicalSystem.AcornArchimedes);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildPackUrl_InvalidPackType_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => UrlBuilder.BuildPackUrl((PackType)int.MaxValue, PhysicalSystem.AcornArchimedes));
        }

        [Fact]
        public void BuildPackUrl_InvalidSystem_Builds()
        {
            string actual = UrlBuilder.BuildPackUrl(PackType.Datfile, PhysicalSystem.MarkerOtherEnd);
            Assert.Equal("https://redump.info/datfile//", actual);
        }

        #endregion

        #region BuildQueueUrl

        [Fact]
        public void BuildQueueUrl_Constant()
        {
            string actual = UrlBuilder.BuildQueueUrl();
            Assert.Equal("https://redump.info/queue/", actual);
        }

        #endregion

        #region BuildQueueDiscUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildQueueDiscUrl_AlwaysPositive(int id, int expected)
        {
            string actual = UrlBuilder.BuildQueueDiscUrl(id);
            Assert.Equal($"https://redump.info/queue/{expected}/", actual);
        }

        #endregion
    }
}
