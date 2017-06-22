using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;
using Core.Repositories;

namespace Logic.Common.Decorators
{
    public abstract class RunnerRepositoryDecorator : IRunnerRepository
    {
        protected IRunnerRepository BaseRepository { get; }

        public RunnerRepositoryDecorator(IRunnerRepository baseRepository) => BaseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository), $"{nameof(baseRepository)} must not be null.");

        public abstract Runner Get(int id);
        public abstract Runner GetIfHasNoTimeWithCategory(string chipId);
        public abstract IEnumerable<Runner> GetAll();
        public abstract Runner FirstOrDefault(Expression<Func<Runner, bool>> predicate);
        public abstract Runner SingleOrDefault(Expression<Func<Runner, bool>> predicate);
        public abstract void Add(Runner entity);
        public abstract void AddRange(IEnumerable<Runner> entities);
        public abstract void Remove(Runner entity);
        public abstract void RemoveRange(IEnumerable<Runner> entities);
    }
}
