using System;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Data
{
    public class ExtensionsTests
    {
        #region Physical Media Type

        [Fact]
        public void PhysicalMediaType_ListMediaTypes()
        {
            var actual = Extensions.ListMediaTypes();
            Assert.NotEmpty(actual);
        }

        /// <summary>
        /// Check that every PhysicalMediaType has a long name provided
        /// </summary>
        /// <param name="mediaType">PhysicalMediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalMediaTypeTestData))]
        public void PhysicalMediaType_LongName(PhysicalMediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every PhysicalMediaType has a short name provided
        /// </summary>
        /// <param name="mediaType">PhysicalMediaType value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GeneratePhysicalMediaTypeTestData))]
        public void PhysicalMediaType_ShortName(PhysicalMediaType? mediaType, bool expectNull)
        {
            var actual = mediaType.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Generate a test set of PhysicalMediaType values
        /// </summary>
        /// <returns>MemberData-compatible list of PhysicalMediaType values</returns>
        public static TheoryData<PhysicalMediaType?, bool> GeneratePhysicalMediaTypeTestData()
        {
            var testData = new TheoryData<PhysicalMediaType?, bool>() { { null, true } };
            foreach (PhysicalMediaType? mediaType in Enum.GetValues<PhysicalMediaType>().Cast<PhysicalMediaType?>())
            {
                testData.Add(mediaType, false);
            }

            return testData;
        }

        #endregion
    }
}
