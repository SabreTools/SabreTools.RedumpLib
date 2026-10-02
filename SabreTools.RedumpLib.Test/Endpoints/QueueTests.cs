using Xunit;

namespace SabreTools.RedumpLib.Test.Endpoints
{
    public class QueueTests
    {
        #region BuildUrl

        [Fact]
        public void BuildUrl_Constant()
        {
            var endpoint = new RedumpLib.Endpoints.Queue();

            string actual = endpoint.BuildUrl();
            Assert.Equal("https://redump.info/queue", actual);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, 1)]
        public void BuildUrl_WithId_AlwaysPositive(int id, int expected)
        {
            var endpoint = new RedumpLib.Endpoints.Queue { Id = id, Submitter = "ignored" };

            string actual = endpoint.BuildUrl();
            Assert.Equal($"https://redump.info/queue/{expected}/", actual);
        }

        // TODO: Implement more extensive queue tests

        #endregion
    }
}
