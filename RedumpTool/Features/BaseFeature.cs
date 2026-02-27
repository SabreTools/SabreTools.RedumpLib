using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal abstract class BaseFeature : Feature
    {
        #region Inputs

        private const string _debugName = "debug";
        internal readonly FlagInput DebugInput = new(_debugName, ["-d", "--debug"], "Enable debug mode");

        private const string _outputName = "output";
        internal readonly StringInput OutputInput = new(_outputName, ["-o", "--output"], "Set the base output directory");

        private const string _passwordName = "password";
        internal readonly StringInput PasswordInput = new(_passwordName, ["-p", "--password"], "Redump password");

        private const string _usernameName = "username";
        internal readonly StringInput UsernameInput = new(_usernameName, ["-u", "--username"], "Redump username");

        #endregion

        #region Fields

        /// <summary>
        /// Client to use for external connections
        /// </summary>
        protected RedumpClient _client;

        #endregion

        public BaseFeature(string name, string[] flags, string description, string? detailed = null)
           : base(name, flags, description, detailed)
        {
            _client = new RedumpClient();
        }

        #region Helpers

        /// <summary>
        /// Validate and create the possible supplied output directory
        /// </summary>
        /// <param name="directory">Full directory path</param>
        /// <returns>True if the output directory was validated and created, false otherwise</returns>
        protected static bool ValidateAndCreateOutputDirectory(string? directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                Console.Error.WriteLine("No output directory set!");
                return false;
            }
            else
            {
                // Create the output directory, if it doesn't exist
                try
                {
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    return true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An exception has occurred: {ex}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Output the set of processed IDs to console, if possible
        /// </summary>
        /// <param name="processedIds">Set of processed IDs</param>
        /// <returns>True if there were any IDs to process, false otherwise</returns>
        protected static bool PrintProcessedIds(List<int> processedIds)
        {
            if (processedIds.Count > 0)
            {
                string formattedIds = string.Join(", ", [.. processedIds.ConvertAll(i => i.ToString())]);
                Console.WriteLine($"Processed IDs: {formattedIds}");
                return true;
            }
            else
            {
                Console.WriteLine("No results were found");
                return false;
            }
        }

        #endregion
    }
}
