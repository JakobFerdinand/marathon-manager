using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.Decorators
{
    public abstract class RunnerRepositoryDecorator : IRunnerRepository
    {
        private readonly IRunnerRepository baseRepository;

        public RunnerRepositoryDecorator(IRunnerRepository baseRepository) => this.baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository), $"{nameof(baseRepository)} must not be null.");

        public virtual Runner Get(int id) => baseRepository.Get(id);
        public virtual Runner GetIfHasNoTimeWithCategory(string chipId) => baseRepository.GetIfHasNoTimeWithCategory(chipId);
        public virtual ImmutableList<Runner> GetAll(bool asNoTracking = true) => baseRepository.GetAll(asNoTracking);
        public virtual ImmutableList<Runner> GetAllWithCategories(bool asNoTracking = false) => baseRepository.GetAllWithCategories(asNoTracking);
        public virtual int Count(Expression<Func<Runner, bool>> predicate) => baseRepository.Count(predicate);
        public virtual ImmutableList<Runner> Find(Expression<Func<Runner, bool>> predicate) => baseRepository.Find(predicate);
        public virtual Runner FirstOrDefault(Expression<Func<Runner, bool>> predicate) => baseRepository.FirstOrDefault(predicate);
        public virtual Runner SingleOrDefault(Expression<Func<Runner, bool>> predicate) => baseRepository.SingleOrDefault(predicate);
        public virtual void Add(Runner entity) => baseRepository.Add(entity);
        public virtual void AddRange(IEnumerable<Runner> entities) => baseRepository.AddRange(entities);
        public virtual void Remove(Runner entity) => baseRepository.Remove(entity);
        public virtual void RemoveRange(IEnumerable<Runner> entities) => baseRepository.RemoveRange(entities);
    }
}
