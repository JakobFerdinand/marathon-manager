using Core.Models;
using Core.Repositories;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logic.Common.Decorators
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

        public override Runner Get(int id)
        {
            return BaseRepository.Get(id);
        }
        public override Runner GetIfHasNoTimeWithCategory(string chipId)
        {
            try
            {
                var runner = BaseRepository.GetIfHasNoTimeWithCategory(chipId);

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
                var invalidRunners = BaseRepository.Find(r => r.ChipId == chipId);
                _logger.LogError($"{_dateTimeManager.Now.ToString("HH:mm:ss.fff")} | There where {invalidRunners.Count()} runners with the chipId {chipId}.");

                foreach (var runner in invalidRunners)
                    _logger.LogError($" - Id: {runner.Id, 3} | Startnumber: {runner.Startnumber} | {runner.Firstname} {runner.Lastname}");
            }
            return null;
        }
        public override IEnumerable<Runner> Find(Expression<Func<Runner, bool>> predicate)
        {
            return BaseRepository.Find(predicate);
        }
        public override IEnumerable<Runner> GetAll(bool withTracking = true)
        {
            return BaseRepository.GetAll(withTracking);
        }
        public override int Count(Expression<Func<Runner, bool>> predicate)
        {
            return BaseRepository.Count(predicate);
        }
        public override Runner FirstOrDefault(Expression<Func<Runner, bool>> predicate)
        {
            return BaseRepository.FirstOrDefault(predicate);
        }
        public override Runner SingleOrDefault(Expression<Func<Runner, bool>> predicate)
        {
            return BaseRepository.SingleOrDefault(predicate);
        }
        public override void Add(Runner entity)
        {
            BaseRepository.Add(entity);
        }
        public override void AddRange(IEnumerable<Runner> entities)
        {
            BaseRepository.AddRange(entities);
        }
        public override void Remove(Runner entity)
        {
            BaseRepository.Remove(entity);
        }
        public override void RemoveRange(IEnumerable<Runner> entities)
        {
            BaseRepository.RemoveRange(entities);
        }
    }
}
