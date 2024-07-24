

using System;
using System.IO;
using Xunit;

namespace SabreTools.RedumpLib.Test
{
    public class BuilderTests
    {
        [Theory]
        [InlineData("success_complete.json", false)]
        [InlineData("success_invalid.json", false)] // Fully in valid returns a default object
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
    }
}