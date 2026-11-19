using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Data
{
    public class DiscResponseTests
    {
        [Fact]
        public void Deserialize_SectorRanges_Pairs()
        {
            // https://redump.info/api/disc?id=46617 returns each range as [start, end]
            string json = "{\"id\":46617,\"sector_ranges\":[[12544,16639],[4246304,4250399]]}";

            var discResponse = JsonConvert.DeserializeObject<DiscResponse>(json);

            Assert.NotNull(discResponse);
            Assert.NotNull(discResponse.SectorRanges);
            Assert.Equal(2, discResponse.SectorRanges.Length);
            Assert.Equal([12544, 16639], discResponse.SectorRanges[0]);
            Assert.Equal([4246304, 4250399], discResponse.SectorRanges[1]);
        }
    }
}
