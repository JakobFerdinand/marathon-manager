using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;
using Core.Repositories;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using System.Globalization;

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
                _logger.LogError($"{_dateTimeManager.Now.ToString("HH:mm:ss.fff")} | There where more than one runner with the chipId {chipId}.");
            }
            return null;
        }
        public override IEnumerable<Runner> GetAll()
        {
            return BaseRepository.GetAll();
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
