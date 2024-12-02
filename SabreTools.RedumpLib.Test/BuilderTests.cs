

using System;
using System.IO;
using SabreTools.RedumpLib.Data;
using Xunit;

namespace SabreTools.RedumpLib.Test
{
    public class BuilderTests
    {
        [Theory]
        [InlineData("success_complete.json", false)]
        [InlineData("success_invalid.json", false)] // Fully invalid returns a default object
        [InlineData("success_partial.json", false)]
        [InlineData("fail_invalid.json", true)]
        public void CreateFromFileTest(string filename, bool expectNull)
        {
            // Get the full path to the test file
            string path = Path.Combine(Environment.CurrentDirectory, "TestData", filename);

            // Try to create the submission info from file
            var si = Builder.CreateFromFile(path);

            // Check for an expected result
            Assert.Equal(expectNull, si == null);
        }

        [Fact]
        public void EnsureAllSections_Null_Filled()
        {
            SubmissionInfo? si = null;
            var actual = Builder.EnsureAllSections(si);

            Assert.NotNull(actual);
            Assert.NotNull(actual.CommonDiscInfo);
            Assert.NotNull(actual.CommonDiscInfo.CommentsSpecialFields);
            Assert.NotNull(actual.CommonDiscInfo.ContentsSpecialFields);
            Assert.NotNull(actual.VersionAndEditions);
            Assert.NotNull(actual.EDC);
            Assert.NotNull(actual.ParentCloneRelationship);
            Assert.NotNull(actual.Extras);
            Assert.NotNull(actual.CopyProtection);
            Assert.NotNull(actual.DumpersAndStatus);
            Assert.NotNull(actual.TracksAndWriteOffsets);
            Assert.NotNull(actual.SizeAndChecksums);
            Assert.NotNull(actual.DumpingInfo);
            Assert.NotNull(actual.Artifacts);
        }

        [Fact]
        public void EnsureAllSections_Empty_Filled()
        {
            SubmissionInfo? si = new SubmissionInfo
            {
                CommonDiscInfo = null,
                VersionAndEditions = null,
                EDC = null,
                ParentCloneRelationship = null,
                Extras = null,
                CopyProtection = null,
                DumpersAndStatus = null,
                TracksAndWriteOffsets = null,
                SizeAndChecksums = null,
                DumpingInfo = null,
                Artifacts = null,
            };
            var actual = Builder.EnsureAllSections(si);

            Assert.NotNull(actual);
            Assert.NotNull(actual.CommonDiscInfo);
            Assert.NotNull(actual.CommonDiscInfo.CommentsSpecialFields);
            Assert.NotNull(actual.CommonDiscInfo.ContentsSpecialFields);
            Assert.NotNull(actual.VersionAndEditions);
            Assert.NotNull(actual.EDC);
            Assert.NotNull(actual.ParentCloneRelationship);
            Assert.NotNull(actual.Extras);
            Assert.NotNull(actual.CopyProtection);
            Assert.NotNull(actual.DumpersAndStatus);
            Assert.NotNull(actual.TracksAndWriteOffsets);
            Assert.NotNull(actual.SizeAndChecksums);
            Assert.NotNull(actual.DumpingInfo);
            Assert.NotNull(actual.Artifacts);
        }

        [Fact]
        public void EnsureAllSections_Filled_Filled()
        {
            SubmissionInfo? si = new SubmissionInfo
            {
                CommonDiscInfo = new CommonDiscInfoSection
                {
                    CommentsSpecialFields = [],
                    ContentsSpecialFields = [],
                },
                VersionAndEditions = new VersionAndEditionsSection(),
                EDC = new EDCSection(),
                ParentCloneRelationship = new ParentCloneRelationshipSection(),
                Extras = new ExtrasSection(),
                CopyProtection = new CopyProtectionSection(),
                DumpersAndStatus = new DumpersAndStatusSection(),
                TracksAndWriteOffsets = new TracksAndWriteOffsetsSection(),
                SizeAndChecksums = new SizeAndChecksumsSection(),
                DumpingInfo = new DumpingInfoSection(),
                Artifacts = [],
            };
            var actual = Builder.EnsureAllSections(si);

            Assert.NotNull(actual);
            Assert.NotNull(actual.CommonDiscInfo);
            Assert.NotNull(actual.CommonDiscInfo.CommentsSpecialFields);
            Assert.NotNull(actual.CommonDiscInfo.ContentsSpecialFields);
            Assert.NotNull(actual.VersionAndEditions);
            Assert.NotNull(actual.EDC);
            Assert.NotNull(actual.ParentCloneRelationship);
            Assert.NotNull(actual.Extras);
            Assert.NotNull(actual.CopyProtection);
            Assert.NotNull(actual.DumpersAndStatus);
            Assert.NotNull(actual.TracksAndWriteOffsets);
            Assert.NotNull(actual.SizeAndChecksums);
            Assert.NotNull(actual.DumpingInfo);
            Assert.NotNull(actual.Artifacts);
        }
    
        [Fact]
        public void InjectSubmissionInformation_BothNull_Null()
        {
            SubmissionInfo? si = null;
            SubmissionInfo? seed = null;

            var actual = Builder.InjectSubmissionInformation(si, seed);
            Assert.Null(actual);
        }

        [Fact]
        public void InjectSubmissionInformation_ValidInputNullSeed_Valid()
        {
            SubmissionInfo? si = new SubmissionInfo();
            SubmissionInfo? seed = null;

            var actual = Builder.InjectSubmissionInformation(si, seed);
            Assert.NotNull(actual);
        }

        [Fact]
        public void InjectSubmissionInformation_BothValid_Valid()
        {
            SubmissionInfo? si = new SubmissionInfo();
            SubmissionInfo? seed = new SubmissionInfo();

            var actual = Builder.InjectSubmissionInformation(si, seed);
            Assert.NotNull(actual);
        }

        [Fact]
        public void ReplaceHtmlWithSiteCodes_EmptyString_Empty()
        {
            string original = string.Empty;
            string actual = Builder.ReplaceHtmlWithSiteCodes(original);
            Assert.Empty(actual);
        }

        [Fact]
        public void ReplaceHtmlWithSiteCodes_NoReplace_Identical()
        {
            string original = "<p>Nothing here will be replaced</p>";
            string actual = Builder.ReplaceHtmlWithSiteCodes(original);
            Assert.Equal(original, actual);
        }

        [Fact]
        public void ReplaceHtmlWithSiteCodes_StandardCode_Replaced()
        {
            string original = "<b>ISBN</b>: 000-0-00-000000-0";
            string expected = "[T:ISBN] 000-0-00-000000-0";

            string actual = Builder.ReplaceHtmlWithSiteCodes(original);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReplaceHtmlWithSiteCodes_OutdatedCode_Replaced()
        {
            string original = "XMID: AB12345C";
            string expected = "<b>XMID</b>: AB12345C";

            string actual = Builder.ReplaceHtmlWithSiteCodes(original);
            Assert.Equal(expected, actual);
        }
    }
}