using Core;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using System;

namespace UI.TimeRecord
{
    internal class TimeRecorder
    {
        private readonly IDateTimeManager _dateTimeManager;
        private readonly ILogger _logger;
        private readonly IReader _reader;
        private readonly IUnitOfWork _unitOfWork;

        public TimeRecorder(
            IDateTimeManager dateTimeManager,
            ILogger logger,
            IReader reader,
            IUnitOfWork unitOfWork)
        {
            _dateTimeManager = dateTimeManager ?? throw new ArgumentNullException(nameof(dateTimeManager), $"{nameof(dateTimeManager)} must not be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(logger)} must not be null.");
            _reader = reader ?? throw new ArgumentNullException(nameof(reader), $"{nameof(reader)} must not be null.");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(reader), $"{nameof(reader)} must not be null.");
        }

        public void Run()
        {
            _logger.LogSuccess("Time Record started!");

            while (true)
            {
                var chipId = _reader.Read();
                var timeAtDestination = _dateTimeManager.Now;

                var runner = _unitOfWork.Runners.GetIfHasNoTimeWithCategory(chipId);

                if (runner is null)
                    continue;

                runner.TimeAtDestination = timeAtDestination;
                runner.RunningTime = timeAtDestination - runner.Category.Starttime;

                _logger.LogMessage($"{timeAtDestination.ToString("hh:mm:ss.fff")} | {$"{runner.Firstname} {runner.Lastname}",25} | {runner.Startnumber} | {runner.RunningTime}");

                _unitOfWork.Complete();
            }
        }
    }
}
