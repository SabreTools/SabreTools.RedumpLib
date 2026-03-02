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

        private const string _attemptCountName = "attemptcount";
        internal readonly Int32Input AttemptCountInput = new(_attemptCountName, ["-a", "--attempts"], "Number of attempts for web requests (default 3");

        private const string _debugName = "debug";
        internal readonly FlagInput DebugInput = new(_debugName, ["-d", "--debug"], "Enable debug mode");

        private const string _forceContinueName = "forcecontinue";
        internal readonly FlagInput ForceContinueInput = new(_forceContinueName, ["-c", "--continue"], "Force continuing downloads through errors");

        private const string _forceDownloadName = "forcedownload";
        internal readonly FlagInput ForceDownloadInput = new(_forceDownloadName, ["-f", "--force"], "Force downloading contents even if they already exist");

        private const string _outputName = "output";
        internal readonly StringInput OutputInput = new(_outputName, ["-o", "--output"], "Set the base output directory");

        private const string _passwordName = "password";
        internal readonly StringInput PasswordInput = new(_passwordName, ["-p", "--password"], "Redump password");

        private const string _timeoutName = "timeout";
        internal readonly Int32Input TimeoutInput = new(_timeoutName, ["-t", "--timeout"], "Request timeout in whole seconds (default 30)");

        private const string _usernameName = "username";
        internal readonly StringInput UsernameInput = new(_usernameName, ["-u", "--username"], "Redump username");

        #region Discs Path Filter Inputs

        private const string _antiModchipName = "antimodchip";
        internal readonly BooleanInput AntiModchipInput = new(_antiModchipName, ["--anti-modchip"], "Filter by anti-modchip status [true, false, null]");

        private const string _barcodeName = "barcode";
        internal readonly FlagInput BarcodeInput = new(_barcodeName, ["--barcode"], "Filter by missing barcodes");

        private const string _categoryName = "category";
        internal readonly StringInput CategoryInput = new(_categoryName, ["--category"], "Filter by disc category");

        private const string _commentsName = "comments";
        internal readonly FlagInput CommentsInput = new(_commentsName, ["--comments"], "Filter by comments only, incompatible with --contents and --protection");

        private const string _contentsName = "contents";
        internal readonly FlagInput ContentsInput = new(_contentsName, ["--contents"], "Filter by contents only, incompatible with --comments and --protection");

        private const string _discTypeName = "disctype";
        internal readonly StringInput DiscTypeInput = new(_discTypeName, ["--disc-type"], "Filter by disc type, requires --system [cd, dvd]");

        private const string _dumperName = "dumper";
        internal readonly StringInput DumperInput = new(_dumperName, ["--dumper"], "Filter by dumper");

        private const string _edcName = "edc";
        internal readonly BooleanInput EdcInput = new(_edcName, ["--edc"], "Filter by EDC status [true, false, null]");

        private const string _editionName = "edition";
        internal readonly StringInput EditionInput = new(_editionName, ["--edition"], "Filter by edition");

        private const string _errorsName = "errors";
        internal readonly StringInput ErrorsInput = new(_errorsName, ["--errors"], "Filter by errors");

        private const string _languageName = "language";
        internal readonly StringInput LanguageInput = new(_languageName, ["--language"], "Filter by language");

        private const string _letterName = "letter";
        internal readonly StringInput LetterInput = new(_letterName, ["--letter"], "Filter by first letter");

        private const string _libCryptName = "libcrypt";
        internal readonly BooleanInput LibCryptInput = new(_libCryptName, ["--libcrypt"], "Filter by LibCrypt status [true, false, null]");

        private const string _mediaName = "media";
        internal readonly StringInput MediaInput = new(_mediaName, ["--media"], "Filter by media type");

        private const string _offsetName = "offset";
        internal readonly Int32Input OffsetInput = new(_offsetName, ["--offset"], "Filter by disc offset");

        private const string _pageName = "page";
        internal readonly Int32Input PageInput = new(_pageName, ["--page"], "Retrieve specific result page");

        private const string _protectionName = "protection";
        internal readonly FlagInput ProtectionInput = new(_protectionName, ["--protection"], "Filter by protection only, incompatible with --comments and --contents");

        private const string _quickSearchName = "quicksearch";
        internal readonly StringInput QuickSearchInput = new(_quickSearchName, ["--quicksearch"], "Filter by quicksearch");

        private const string _regionName = "region";
        internal readonly StringInput RegionInput = new(_regionName, ["--region"], "Filter by region");

        private const string _ringcodeName = "ringcode";
        internal readonly StringInput RingcodeInput = new(_ringcodeName, ["--ringcode"], "Filter by ringcode");

        private const string _sortName = "sort";
        internal readonly StringInput SortInput = new(_sortName, ["--sort"], "Sort results by criteria [added, region, system, version, edition, languages, serial, status, modified]");

        private const string _sortDirName = "sortdir";
        internal readonly StringInput SortDirInput = new(_sortDirName, ["--sort-dir"], "Set sorting direction [asc, desc]");

        private const string _statusName = "status";
        internal readonly StringInput StatusInput = new(_statusName, ["--status"], "Filter by status [grey, red, yellow, blue, green]");

        private const string _systemName = "system";
        internal readonly StringInput SystemInput = new(_systemName, ["--system"], "Filter by system");

        private const string _tracksName = "tracks";
        internal readonly Int32Input TracksInput = new(_tracksName, ["--tracks"], "Filter by track count [1-99]");

        #endregion

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
