using Core.Decorators;
using Core.Models;
using Core.Repositories;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using System;
using System.Linq;

namespace Logging
{
    public class LoggingRunnerRepository : RunnerRepositoryDecorator
    {
        private readonly IDateTimeManager _dateTimeManager;
        private readonly ILogger _logger;

        public LoggingRunnerRepository(IDateTimeManager dateTimeManager, ILogger logger, IRunnerRepository baseRepository)
            : base(baseRepository)
        {
            _dateTimeManager = dateTimeManager ?? throw new ArgumentNullException(nameof(dateTimeManager), $"{nameof(dateTimeManager)} must not be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(logger)} must not be null.");
        }

        public override Runner GetIfHasNoTimeWithCategory(string chipId)
        {
            try
            {
                var runner = base.GetIfHasNoTimeWithCategory(chipId);

                if (runner?.Category != null && runner.Category.Starttime is null)
                {
                    _logger.LogError($"{_dateTimeManager.Now.ToString("HH:mm:ss:fff")} | Category {runner.Category.Name} has not yet started.");
                    _logger.LogError($"\t Startnumber: {runner.Startnumber,3} | {chipId} | {runner.Firstname} {runner.Lastname}");
                    return null;
                }

                return runner;
            }
            catch (InvalidOperationException)
            {
                var invalidRunners = base.Find(r => r.ChipId == chipId);
                _logger.LogError($"{_dateTimeManager.Now.ToString("HH:mm:ss.fff")} | There where {invalidRunners.Count()} runners with the chipId {chipId}.");

                foreach (var runner in invalidRunners)
                    _logger.LogError($" - Id: {runner.Id, 3} | Startnumber: {runner.Startnumber} | {runner.Firstname} {runner.Lastname}");
            }
            return null;
        }
    }
}
