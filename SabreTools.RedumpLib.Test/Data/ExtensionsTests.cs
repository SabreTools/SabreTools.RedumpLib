using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test.Data
{
    public class ExtensionsTests
    {
        #region Language

        /// <summary>
        /// Check that every Language has a long name provided
        /// </summary>
        /// <param name="language">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_LongName(Language? language, bool expectNull)
        {
            var actual = language.LongName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every Language has a short name provided
        /// </summary>
        /// <param name="language">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_ShortName(Language? language, bool expectNull)
        {
            var actual = language.ShortName();

            if (expectNull)
                Assert.Null(actual);
            else
                Assert.NotNull(actual);
        }

        /// <summary>
        /// Check that every Language can be mapped from a string
        /// </summary>
        /// <param name="lang">Language value to check</param>
        /// <param name="expectNull">True to expect a null value, false otherwise</param>
        [Theory]
        [MemberData(nameof(GenerateLanguageTestData))]
        public void Language_ToLanguage(Language? lang, bool expectNull)
        {
            string? twoLetterCode = lang.TwoLetterCode();
            string? threeLetterCode = lang.ThreeLetterCode();
            string? threeLetterCodeAlt = lang.ThreeLetterCodeAlt();

            var actualTwoLetterCode = twoLetterCode.ToLanguage();
            var actualThreeLetterCode = threeLetterCode.ToLanguage();
            var actualThreeLetterCodeAlt = threeLetterCodeAlt.ToLanguage();

            if (expectNull)
            {
                Assert.Null(actualTwoLetterCode);
                Assert.Null(actualThreeLetterCode);
                Assert.Null(actualThreeLetterCodeAlt);
            }
            else
            {
                Assert.Equal(lang, actualThreeLetterCode);

                // Not guaranteed to exist
                if (twoLetterCode is not null)
                    Assert.Equal(lang, actualTwoLetterCode);
                if (threeLetterCodeAlt is not null)
                    Assert.Equal(lang, actualThreeLetterCodeAlt);
            }
        }

        /// <summary>
        /// Ensure that every Language that has a standard/bibliographic ISO 639-2 code is unique
        /// </summary>
        [Fact]
        public void Language_ThreeLetterCode_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.ThreeLetterCode();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Ensure that every Language that has a terminology ISO 639-2 code is unique
        /// </summary>
        [Fact]
        public void Language_ThreeLetterCodeAlt_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.ThreeLetterCodeAlt();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Ensure that every Language that has an ISO 639-1 code is unique
        /// </summary>
        [Fact]
        public void Language_TwoLetterCode_NoDuplicates()
        {
            var fullLanguages = Enum.GetValues<Language>().Cast<Language?>().ToList();
            var filteredLanguages = new Dictionary<string, Language?>();

            int totalCount = 0;
            foreach (Language? language in fullLanguages)
            {
                var code = language.TwoLetterCode();
                if (string.IsNullOrEmpty(code))
                    continue;

                // Throw if the code already exists
                if (filteredLanguages.ContainsKey(code))
                    throw new DuplicateNameException($"Code {code} already in dictionary");

                filteredLanguages[code] = language;
                totalCount++;
            }

            Assert.Equal(totalCount, filteredLanguages.Count);
        }

        /// <summary>
        /// Generate a test set of Language values
        /// </summary>
        /// <returns>MemberData-compatible list of Language values</returns>
        public static TheoryData<Language?, bool> GenerateLanguageTestData()
        {
            var testData = new TheoryData<Language?, bool>() { { null, true } };
            foreach (Language? language in Enum.GetValues<Language>().Cast<Language?>())
            {
                testData.Add(language, false);
            }

            return testData;
        }

        #endregion

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
