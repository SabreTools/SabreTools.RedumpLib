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
            string expected = "https://redump.info/bios/XBOX";

            string actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Nintendo GameCube
            endpoint.System = PhysicalSystem.NintendoGameCubeBIOS;
            expected = "https://redump.info/bios/GC";

            actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Sony PlayStation
            endpoint.System = PhysicalSystem.SonyPlayStationBIOS;
            expected = "https://redump.info/bios/PSX";

            actual = endpoint.BuildUrl();
            Assert.Equal(expected, actual);

            // Sony PlayStation 2
            endpoint.System = PhysicalSystem.SonyPlayStation2BIOS;
            expected = "https://redump.info/bios/PS2";

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
