using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class ListTests
    {
        #region BuildUrl

        [Fact]
        public void BuildUrl_MissingUsername_DefaultEmpty()
        {
            var endpoint = new RedumpLib.Endpoints.List();
            string actual = endpoint.BuildUrl();
            Assert.Equal($"https://redump.info/list/", actual);
        }

        [Fact]
        public void BuildUrl_Subpath_Builds()
        {
            // Create a base endpoint and then modify the fields
            var endpoint = new RedumpLib.Endpoints.List { Username = "user" };

            // Invalid subpath, no system
            endpoint.Subpath = null;
            endpoint.System = null;
            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/user", actual);

            // Invalid subpath, valid system
            endpoint.Subpath = null;
            endpoint.System = PhysicalSystem.AcornArchimedesAndRiscPC;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/user/ARCH", actual);

            // Have subpath, no system
            endpoint.Subpath = ListSubpath.Have;
            endpoint.System = null;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/have/user", actual);

            // Have subpath, valid system
            endpoint.Subpath = ListSubpath.Have;
            endpoint.System = PhysicalSystem.AcornArchimedesAndRiscPC;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/have/user/ARCH", actual);

            // Cuesheets subpath, no system
            endpoint.Subpath = ListSubpath.Cuesheets;
            endpoint.System = null;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/cues/user", actual);

            // Cuesheets subpath, valid system
            endpoint.Subpath = ListSubpath.Cuesheets;
            endpoint.System = PhysicalSystem.AcornArchimedesAndRiscPC;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/cues/user/ARCH", actual);

            // Datfile subpath, no system
            endpoint.Subpath = ListSubpath.Datfile;
            endpoint.System = null;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/datfile/user", actual);

            // Datfile subpath, valid system
            endpoint.Subpath = ListSubpath.Datfile;
            endpoint.System = PhysicalSystem.AcornArchimedesAndRiscPC;
            actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/list/datfile/user/ARCH", actual);
        }

        #endregion
    }
}
