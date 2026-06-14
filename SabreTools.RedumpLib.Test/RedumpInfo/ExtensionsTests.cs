using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.RedumpInfo.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.RedumpInfo
{
    public class ExtensionsTests
    {
        #region Disc Subpath

        /// <summary>
        /// Check that every DiscSubpath has a long name provided
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_LongName(DiscSubpath? discSubpath, bool expectNull)
        {
            var actual = discSubpath.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DiscSubpath has a short name provided
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_ShortName(DiscSubpath? discSubpath, bool expectNull)
        {
            var actual = discSubpath.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every DiscSubpath that has a short name that is unique
        /// </summary>
        [Fact]
        public void DiscSubpath_ShortName_NoDuplicates()
        {
            var fullDiscSubpaths = Enum.GetValues<DiscSubpath>().Cast<DiscSubpath>().ToList();
            var filteredDiscSubpaths = new Dictionary<string, DiscSubpath?>();

            int totalCount = 0;
            foreach (DiscSubpath? discSubpath in fullDiscSubpaths)
            {
                var code = discSubpath.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredDiscSubpaths.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredDiscSubpaths[code] = discSubpath;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredDiscSubpaths.Count);
        }

        /// <summary>
        /// Check that every DiscSubpath can be mapped from a string
        /// </summary>
        /// <param name="discSubpath">DiscSubpath value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDiscSubpathTestData))]
        public void DiscSubpath_ToDiscSubpath(DiscSubpath? discSubpath, bool expectNull)
        {
            string? longName = discSubpath.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);

            var actualNormal = longName.ToDiscSubpath();
            var actualSpaceless = longNameSpaceless.ToDiscSubpath();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
            }
            else
            {
                Assert.Equal(discSubpath, actualNormal);
                Assert.Equal(discSubpath, actualSpaceless);
            }
        }

        /// <summary>
        /// Generate a test set of DiscSubpath values
        /// </summary>
        /// <returns>MemberData-compatible list of DiscSubpath values</returns>
        public static TheoryData<DiscSubpath?, bool> GenerateDiscSubpathTestData()
        {
            var testData = new TheoryData<DiscSubpath?, bool>() { { null, true } };
            foreach (DiscSubpath? discSubpath in Enum.GetValues<DiscSubpath>().Cast<DiscSubpath?>())
            {
                testData.Add(discSubpath, false);
            }

            return testData;
        }

        #endregion

        #region Dump Status

        /// <summary>
        /// Check that every DumpStatus has a long name provided
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_LongName(DumpStatus? dumpStatus, bool expectNull)
        {
            var actual = dumpStatus.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every DumpStatus has a short name provided
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_ShortName(DumpStatus? dumpStatus, bool expectNull)
        {
            var actual = dumpStatus.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Ensure that every DumpStatus that has a short name that is unique
        /// </summary>
        [Fact]
        public void DumpStatus_ShortName_NoDuplicates()
        {
            var fullDumpStatuses = Enum.GetValues<DumpStatus>().Cast<DumpStatus>().ToList();
            var filteredDumpStatuses = new Dictionary<string, DumpStatus?>();

            int totalCount = 0;
            foreach (DumpStatus? dumpStatus in fullDumpStatuses)
            {
                var code = dumpStatus.ShortName();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredDumpStatuses.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredDumpStatuses[code] = dumpStatus;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredDumpStatuses.Count);
        }

        /// <summary>
        /// Check that every DumpStatus can be mapped from a string
        /// </summary>
        /// <param name="dumpStatus">DumpStatus value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateDumpStatusTestData))]
        public void DumpStatus_ToDumpStatus(DumpStatus? dumpStatus, bool expectNull)
        {
            string? longName = dumpStatus.LongName();
            string? longNameSpaceless = longName?.Replace(" ", string.Empty);
            string? possibleInteger = $"{(int?)dumpStatus}";

            var actualNormal = longName.ToDumpStatus();
            var actualSpaceless = longNameSpaceless.ToDumpStatus();
            var actualInteger = possibleInteger.ToDumpStatus();

            if (expectNull)
            {
                Assert.Null(actualNormal);
                Assert.Null(actualSpaceless);
                Assert.Null(actualInteger);
            }
            else
            {
                Assert.Equal(dumpStatus, actualNormal);
                Assert.Equal(dumpStatus, actualSpaceless);
                Assert.Equal(dumpStatus, actualInteger);
            }
        }

        /// <summary>
        /// Generate a test set of DumpStatus values
        /// </summary>
        /// <returns>MemberData-compatible list of DumpStatus values</returns>
        public static TheoryData<DumpStatus?, bool> GenerateDumpStatusTestData()
        {
            var testData = new TheoryData<DumpStatus?, bool>() { { null, true } };
            foreach (DumpStatus? dumpStatus in Enum.GetValues<DumpStatus>().Cast<DumpStatus?>())
            {
                testData.Add(dumpStatus, false);
            }

            return testData;
        }

        #endregion
    }
}
