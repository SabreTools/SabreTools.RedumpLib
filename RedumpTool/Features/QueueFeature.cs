using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class QueueFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "queue";

        private static readonly string[] _flags = ["queue"];

        private const string _description = "Download pages and related files from the submission queue";

        #endregion

        #region Inputs

        private const string _discIdName = "disc-id";
        internal readonly Int32Input DiscIDInput = new(_discIdName, ["--disc-id"], "Add disc ID to filter");

        private const string _isDiscHistoryName = "is-disc-history";
        internal readonly BooleanInput IsDiscHistoryInput = new(_isDiscHistoryName, ["--is-disc-history"], "Add disc history status to filter [true, false]");

        private const string _maximumName = "maximum";
        internal readonly Int32Input MaximumInput = new(_maximumName, ["-max", "--maximum"], "Upper bound for page numbers (incompatible with --onlynew)");

        private const string _minimumName = "minimum";
        internal readonly Int32Input MinimumInput = new(_minimumName, ["-min", "--minimum"], "Lower bound for page numbers (incompatible with --onlynew)");

        private const string _orderName = "order";
        internal readonly StringInput OrderInput = new(_orderName, ["--order"], "Add sort order to filter [asc, desc]");

        private const string _sortName = "sort";
        internal readonly StringInput SortInput = new(_sortName, ["--sort"], "Add sort category to filter [title, added, region, system, version, edition, language, serial, status, modified]");

        private const string _statusName = "status";
        internal readonly StringInput StatusInput = new(_statusName, ["--status"], "Add status to filter [grey, red, yellow, blue, green]");

        private const string _submitterName = "submitter";
        internal readonly StringInput SubmitterInput = new(_submitterName, ["--submitter"], "Add submitter to filter");

        private const string _subTypeName = "sub-type";
        internal readonly StringInput SubTypeInput = new(_subTypeName, ["--sub-type"], "Add submission type to filter [edit, new disc, verification]");

        private const string _systemName = "system";
        internal readonly StringInput SystemInput = new(_systemName, ["--system"], "Add system to filter");

        #endregion

        public QueueFeature()
           : base(DisplayName, _flags, _description)
        {
            RequiresInputs = false;

            // Common
            Add(DebugInput);
            Add(OutputInput);
            Add(UsernameInput);
            Add(PasswordInput);
            Add(AttemptCountInput);
            Add(TimeoutInput);
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Specific
            Add(MinimumInput);
            Add(MaximumInput);

            // Filter
            Add(DiscIDInput);
            Add(IsDiscHistoryInput);
            Add(OrderInput);
            Add(SortInput);
            Add(StatusInput);
            Add(SubmitterInput);
            Add(SubTypeInput);
            Add(SystemInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get common values
            string? outDir = OutputInput.Value;
            string? username = UsernameInput.Value;
            string? password = PasswordInput.Value;
            int? attemptCount = AttemptCountInput.Value;
            int? timeout = TimeoutInput.Value;
            bool forceDownload = ForceDownloadInput.Value;
            bool forceContinue = ForceContinueInput.Value;

            // Get specific values
            int? minId = MinimumInput.Value;
            int? maxId = MaximumInput.Value;

            // Get filter values
            long? discId = DiscIDInput.Value;
            bool? isDiscHistory = IsDiscHistoryInput.Value;
            SortDirection? order = OrderInput.Value.ToSortDirection();
            SortCategory? sort = SortInput.Value.ToSortCategory();
            DumpStatus? status = StatusInput.Value.ToDumpStatus();
            string? submitter = SubmitterInput.Value;
            SubmissionType? subType = SubTypeInput.Value.ToSubmissionType();
            PhysicalSystem? system = SystemInput.Value.ToPhysicalSystem();

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Range verification
            if ((minId is null) ^ (maxId is null))
            {
                Console.WriteLine("Both a maximum and minimum ID are required");
                return false;
            }
            else if (minId is not null && minId < 0)
            {
                Console.WriteLine($"{minId} is an invalid minimum ID");
                return false;
            }
            else if (maxId is not null && maxId < 0)
            {
                Console.WriteLine($"{maxId} is an invalid maximum ID");
                return false;
            }

            // Update redump.info properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;
            if (timeout != null && timeout > 0)
                _client.Timeout = TimeSpan.FromSeconds(timeout.Value);
            _client.Overwrite = forceDownload;
            _client.IgnoreErrors = forceContinue;

            // Login to redump.info, if necessary
            _client.Login(username, password).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (minId is null || maxId is null)
            {
                processingTask = _client.DownloadQueueResults(outDir,
                    discId,
                    isDiscHistory,
                    order,
                    sort,
                    status,
                    submitter,
                    subType,
                    system);
            }
            else
            {
                processingTask = _client.DownloadQueueRange(outDir, minId.Value, maxId.Value);
            }

            // Retrieve the result
            processingTask.Wait();
            var processedIds = processingTask.Result;

            // Display the processed IDs
            return PrintProcessedIds(processedIds);
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
