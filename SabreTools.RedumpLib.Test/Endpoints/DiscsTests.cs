using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class DiscsTests
    {
        #region BuildUrl

        [Fact]
        public void BuildUrl_DumperWithPages_Builds()
        {
            var endpoint = new RedumpLib.Endpoints.Discs
            {
                Dumper = "user",
                Page = 3,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/discs?dumper=user&page=3", actual);
        }

        [Fact]
        public void BuildUrl_DumperLastModifiedWithPages_Builds()
        {
            var endpoint = new RedumpLib.Endpoints.Discs
            {
                Dumper = "user",
                Sort = SortCategory.Modified,
                Order = SortDirection.Descending,
                Page = 3,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/discs?dumper=user&sort=modified&order=desc&page=3", actual);
        }

        [Fact]
        public void BuildUrl_LastModifiedWithPages_Builds()
        {
            var endpoint = new RedumpLib.Endpoints.Discs
            {
                Sort = SortCategory.Modified,
                Order = SortDirection.Descending,
                Page = 3,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/discs?sort=modified&order=desc&page=3", actual);
        }

        [Theory]
        [InlineData("", "https://redump.info/discs?q=&page=3")]
        [InlineData("simple", "https://redump.info/discs?q=simple&page=3")]
        [InlineData("search-format", "https://redump.info/discs?q=search-format&page=3")]
        [InlineData("invalid format", "https://redump.info/discs?q=invalid-format&page=3")]
        [InlineData("extra/path", "https://redump.info/discs?q=extra-path&page=3")]
        public void BuildUrl_QuicksearchWithPages_Builds(string query, string expected)
        {
            var endpoint = new RedumpLib.Endpoints.Discs
            {
                Query = query,
                Page = 3,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        // TODO: Implement more extensive discs tests

        #endregion
    }
}
