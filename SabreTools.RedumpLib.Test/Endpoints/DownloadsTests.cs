using System;
using SabreTools.RedumpLib.Data;
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
        public void BuildUrl_Database(bool? database, string expected)
        {
            var endpoint = new RedumpLib.Endpoints.Downloads { Database = database };
            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(PackType.Cuesheets, "https://redump.info/cues/ARCH")]
        [InlineData(PackType.Datfile, "https://redump.info/datfile/ARCH")]
        [InlineData(PackType.Sbis, "https://redump.info/sbi/ARCH")]
        public void BuildUrl_ValidPackType_ValidSystem_Builds(PackType packType, string expected)
        {
            var endpoint = new RedumpLib.Endpoints.Downloads
            {
                Pack = packType,
                System = PhysicalSystem.AcornArchimedesAndRiscPC,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildUrl_BIOSDatfile()
        {
            var endpoint = new RedumpLib.Endpoints.Downloads { Pack = PackType.Datfile };

            // Microsoft Xbox
            endpoint.System = PhysicalSystem.MicrosoftXboxBIOS;
            string expected = "https://redump.info/static/bios/Microsoft%20-%20Xbox%20-%20BIOS%20Images%20%289%29%20%282026-06-16%29.dat";

            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Nintendo GameCube
            endpoint.System = PhysicalSystem.NintendoGameCubeBIOS;
            expected = "https://redump.info/static/bios/Nintendo%20-%20GameCube%20-%20BIOS%20Images%20%2817%29%20%282026-06-16%29.dat";

            actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Sony PlayStation
            endpoint.System = PhysicalSystem.SonyPlayStationBIOS;
            expected = "https://redump.info/static/bios/Sony%20-%20PlayStation%20-%20BIOS%20Images%20%2824%29%20%282026-06-16%29.dat";

            actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Sony PlayStation 2
            endpoint.System = PhysicalSystem.SonyPlayStation2BIOS;
            expected = "https://redump.info/static/bios/Sony%20-%20PlayStation%202%20-%20BIOS%20Datfile%20%28140%29%20%282026-06-16%29.dat";

            actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildUrl_InvalidPackType_Throws()
        {
            var endpoint = new RedumpLib.Endpoints.Downloads
            {
                Pack = (PackType)int.MaxValue,
                System = PhysicalSystem.AcornArchimedesAndRiscPC,
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => endpoint.BuildUrl());
        }

        [Fact]
        public void BuildUrl_InvalidSystem_Builds()
        {
            var endpoint = new RedumpLib.Endpoints.Downloads
            {
                Pack = PackType.Datfile,
                System = PhysicalSystem.MarkerOtherEnd,
            };

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/datfile/", actual);
        }

        #endregion
    }
}
