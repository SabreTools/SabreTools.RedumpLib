using System;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;
using Xunit;

namespace SabreTools.RedumpLib.Test.Web
{
    public class UrlBuilderTests
    {
        #region BuildAboutUrl

        [Fact]
        public void BuildAboutUrl_Constant()
        {
            string actual = UrlBuilder.BuildAboutUrl();
            Assert.Equal("https://redump.info/about", actual);
        }

        #endregion

        #region BuildDiscUrl

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildDiscUrl_AlwaysPositive(int id, int expected)
        {
            string actual = UrlBuilder.BuildDiscUrl(id);
            Assert.Equal($"https://redump.info/disc/{expected}", actual);
        }

        [Theory]
        [InlineData(null, "https://redump.info/disc/1")]
        [InlineData(DiscSubpath.Cuesheet, "https://redump.info/disc/1/cue")]
        [InlineData(DiscSubpath.Edit, "https://redump.info/disc/1/edit")]
        [InlineData(DiscSubpath.SBI, "https://redump.info/disc/1/sbi")]
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
            Assert.Equal("https://redump.info/discs?dumper=user&page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_DumperLastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(dumper: "user", sort: SortCategory.Modified, order: SortDirection.Descending, page: 3);
            Assert.Equal("https://redump.info/discs?dumper=user&sort=modified&order=desc&page=3", actual);
        }

        [Fact]
        public void BuildDiscsUrl_LastModifiedWithPages_Builds()
        {
            string actual = UrlBuilder.BuildDiscsUrl(sort: SortCategory.Modified, order: SortDirection.Descending, page: 3);
            Assert.Equal("https://redump.info/discs?sort=modified&order=desc&page=3", actual);
        }

        [Theory]
        [InlineData("", "https://redump.info/discs?q=&page=3")]
        [InlineData("simple", "https://redump.info/discs?q=simple&page=3")]
        [InlineData("search-format", "https://redump.info/discs?q=search-format&page=3")]
        [InlineData("invalid format", "https://redump.info/discs?q=invalid format&page=3")]
        [InlineData("extra/path", "https://redump.info/discs?q=extra/path&page=3")]
        public void BuildDiscsUrl_QuicksearchWithPages_Builds(string query, string expected)
        {
            string actual = UrlBuilder.BuildDiscsUrl(query: query, page: 3);
            Assert.Equal(expected, actual);
        }

        // TODO: Implement more extensive discs tests

        #endregion

        #region BuildDownloadsUrl

        [Theory]
        [InlineData(null, "https://redump.info/downloads")]
        [InlineData(true, "https://redump.info/downloads/database")]
        [InlineData(false, "https://redump.info/downloads")]
        public void BuildDownloadsUrl_Builds(bool? database, string expected)
        {
            string actual = UrlBuilder.BuildDownloadsUrl(database);
            Assert.Equal(expected, actual);
        }

        #endregion

        #region BuildPackUrl

        [Theory]
        [InlineData(PackType.Cuesheets, "https://redump.info/cues/ARCH")]
        [InlineData(PackType.Datfile, "https://redump.info/datfile/ARCH")]
        [InlineData(PackType.Keys, "https://redump.info/keys/ARCH")]
        [InlineData(PackType.Sbis, "https://redump.info/sbi/ARCH")]
        public void BuildPackUrl_ValidPackType_ValidSystem_Builds(PackType packType, string expected)
        {
            string actual = UrlBuilder.BuildPackUrl(packType, PhysicalSystem.AcornArchimedesAndRiscPC);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(PhysicalSystem.MicrosoftXboxBIOS, "https://redump.info/static/bios/Microsoft%20-%20Xbox%20-%20BIOS%20Images%20%289%29%20%282026-06-16%29.dat")]
        [InlineData(PhysicalSystem.NintendoGameCubeBIOS, "https://redump.info/static/bios/Nintendo%20-%20GameCube%20-%20BIOS%20Images%20%2817%29%20%282026-06-16%29.dat")]
        [InlineData(PhysicalSystem.SonyPlayStationBIOS, "https://redump.info/static/bios/Sony%20-%20PlayStation%20-%20BIOS%20Images%20%2824%29%20%282026-06-16%29.dat")]
        [InlineData(PhysicalSystem.SonyPlayStation2BIOS, "https://redump.info/static/bios/Sony%20-%20PlayStation%202%20-%20BIOS%20Datfile%20%28140%29%20%282026-06-16%29.dat")]
        public void BuildPackUrl_BIOSDatfile(PhysicalSystem system, string expected)
        {
            string actual = UrlBuilder.BuildPackUrl(PackType.Datfile, system);
            Assert.Equal(expected, actual);
        }

       [Fact]
        public void BuildPackUrl_InvalidPackType_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => UrlBuilder.BuildPackUrl((PackType)int.MaxValue, PhysicalSystem.AcornArchimedesAndRiscPC));
        }

        [Fact]
        public void BuildPackUrl_InvalidSystem_Builds()
        {
            string actual = UrlBuilder.BuildPackUrl(PackType.Datfile, PhysicalSystem.MarkerOtherEnd);
            Assert.Equal("https://redump.info/datfile/", actual);
        }

        #endregion

        #region BuildQueueUrl

        [Fact]
        public void BuildQueueUrl_Constant()
        {
            string actual = UrlBuilder.BuildQueueUrl();
            Assert.Equal("https://redump.info/queue", actual);
        }

        // TODO: Implement more extensive queue tests

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
