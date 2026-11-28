using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Tools
{
    /// <summary>
    /// SubmissionInfo extensions for writing to file
    /// </summary>
    public static class Writer
    {
        /// <summary>
        /// Write submission info data to a formatted JSON file
        /// </summary>
        /// <param name="info">SubmissionInfo object representing data to write</param>
        /// <param name="outputDirectory">Output folder to use as the base path</param>
        /// <param name="filenameSuffix">Optional suffix to append to the filename</param>
        /// <returns>True on success, false on error</returns>
        public static bool WriteToJson(this SubmissionInfo? info, string? outputDirectory, string? filenameSuffix)
        {
            // Check to see if the input is valid
            if (info is null)
                return false;

            // Clone the submission info to perform normalization steps
            if (info.Clone() is not SubmissionInfo infoClone)
                return false;

            // Normalize whitespace in all ringcode fields
            // TODO: Figure out how to make this act more like AddIfExists(StringBuilder, string, string?, int)
            infoClone.RingCodes.Layer0MasteringCode = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer0MasteringCode);
            infoClone.RingCodes.Layer0MasteringSID = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer0MasteringSID);
            infoClone.RingCodes.Layer0Toolstamps = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer0Toolstamps);
            infoClone.RingCodes.Layer0MouldSIDs = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer0MouldSIDs);
            infoClone.RingCodes.Layer0AdditionalMoulds = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer0AdditionalMoulds);

            infoClone.RingCodes.Layer1MasteringCode = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer1MasteringCode);
            infoClone.RingCodes.Layer1MasteringSID = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer1MasteringSID);
            infoClone.RingCodes.Layer1Toolstamps = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer1Toolstamps);

            infoClone.RingCodes.Layer2MasteringCode = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer2MasteringCode);
            infoClone.RingCodes.Layer2MasteringSID = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer2MasteringSID);
            infoClone.RingCodes.Layer2Toolstamps = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer2Toolstamps);

            infoClone.RingCodes.Layer3MasteringCode = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer3MasteringCode);
            infoClone.RingCodes.Layer3MasteringSID = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer3MasteringSID);
            infoClone.RingCodes.Layer3Toolstamps = Formatter.NormalizeWhitespace(infoClone.RingCodes.Layer3Toolstamps);

            infoClone.RingCodes.LabelSideMasteringCode = Formatter.NormalizeWhitespace(infoClone.RingCodes.LabelSideMasteringCode);
            infoClone.RingCodes.LabelSideMasteringSID = Formatter.NormalizeWhitespace(infoClone.RingCodes.LabelSideMasteringSID);
            infoClone.RingCodes.LabelSideToolstamps = Formatter.NormalizeWhitespace(infoClone.RingCodes.LabelSideToolstamps);
            infoClone.RingCodes.LabelSideMouldSIDs = Formatter.NormalizeWhitespace(infoClone.RingCodes.LabelSideMouldSIDs);
            infoClone.RingCodes.LabelSideAdditionalMoulds = Formatter.NormalizeWhitespace(infoClone.RingCodes.LabelSideAdditionalMoulds);

            try
            {
                // Get the output path
                var path = string.Empty;
                if (string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = "!submissionInfo.json";
                else if (string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = $"!submissionInfo_{filenameSuffix}.json";
                else if (!string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, "!submissionInfo.json");
                else if (!string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, $"!submissionInfo_{filenameSuffix}.json");

                // Create and open the output file
                using var fs = File.Create(path);

                // Create the JSON serializer
                var serializer = new JsonSerializer { Formatting = Formatting.Indented };
                using var sw = new StreamWriter(fs, Encoding.UTF8);
                serializer.Serialize(sw, infoClone);
            }
            catch
            {
                // Absorb the exception
                return false;
            }

            return true;
        }

        /// <summary>
        /// Write submission info data to a formatted text file
        /// </summary>
        /// <param name="info">SubmissionInfo object representing data to write</param>
        /// <param name="outputDirectory">Output folder to use as the base path</param>
        /// <param name="filenameSuffix">Optional suffix to append to the filename</param>
        /// <returns>True on success, false on error</returns>
        public static bool WriteToText(this SubmissionInfo? info, string? outputDirectory, string? filenameSuffix, out string status)
        {
            // Check to see if the input is valid
            if (info is null)
            {
                status = "No submission information found to write!";
                return false;
            }

            // Format the lines to be written to the output
            string? lines = Formatter.FormatOutputData(info, out string? formatStatus);
            if (lines is null)
            {
                status = formatStatus ?? "No formatted data found to write!";
                return false;
            }

            // Now write out to a generic file
            try
            {
                // Get the file path
                var path = string.Empty;
                if (string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = "!submissionInfo.txt";
                else if (string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = $"!submissionInfo_{filenameSuffix}.txt";
                else if (!string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, "!submissionInfo.txt");
                else if (!string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, $"!submissionInfo_{filenameSuffix}.txt");

                // Ensure the output directory has been created
                string? parentDirectory = Path.GetDirectoryName(path);
                if (parentDirectory is not null)
                    Directory.CreateDirectory(parentDirectory);

                using var sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), Encoding.UTF8);
                sw.Write(lines);
            }
            catch (Exception ex)
            {
                status = $"Writing could not complete: {ex}";
                return false;
            }

            status = "Writing complete!";
            return true;
        }

        /// <summary>
        /// Write protection data found in a submission info to a formatted text file
        /// </summary>
        /// <param name="info">SubmissionInfo object representing data to write</param>
        /// <param name="outputDirectory">Output folder to use as the base path</param>
        /// <param name="filenameSuffix">Optional suffix to append to the filename</param>
        /// <param name="hideDriveLetters">True if drive letters are to be removed from output, false otherwise</param>
        /// <returns>True on success, false on error</returns>
        public static bool WriteProtectionData(this SubmissionInfo? info, string? outputDirectory, string? filenameSuffix, bool hideDriveLetters)
        {
            // Check to see if the inputs are valid
            if (info?.DumpMetadata?.FullProtections is null || info.DumpMetadata.FullProtections.Count == 0)
                return true;

            // Now write out to a generic file
            try
            {
                var path = string.Empty;
                if (string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = "!protectionInfo.txt";
                else if (string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = $"!protectionInfo{filenameSuffix}.txt";
                else if (!string.IsNullOrEmpty(outputDirectory) && string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, "!protectionInfo.txt");
                else if (!string.IsNullOrEmpty(outputDirectory) && !string.IsNullOrEmpty(filenameSuffix))
                    path = Path.Combine(outputDirectory, $"!protectionInfo{filenameSuffix}.txt");

                // Ensure the output directory has been created
                string? parentDirectory = Path.GetDirectoryName(path);
                if (parentDirectory is not null)
                    Directory.CreateDirectory(parentDirectory);

                using var sw = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write), Encoding.UTF8);

                List<string> sortedKeys = [.. info.DumpMetadata.FullProtections.Keys];
                sortedKeys.Sort();

                foreach (string key in sortedKeys)
                {
                    string scanPath = key;
                    if (hideDriveLetters)
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                        scanPath = Path.DirectorySeparatorChar + key[(Path.GetPathRoot(key) ?? string.Empty).Length..];
#else
                        scanPath = Path.DirectorySeparatorChar + key.Substring((Path.GetPathRoot(key) ?? string.Empty).Length);
#endif

                    List<string>? scanResult = info.DumpMetadata.FullProtections[key];

                    if (scanResult is null)
                        sw.WriteLine($"{scanPath}: None");
                    else
                        sw.WriteLine($"{scanPath}: {string.Join(", ", [.. scanResult])}");
                }
            }
            catch
            {
                // Absorb the exception
                return false;
            }

            return true;
        }
    }
}
