using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Core.Models;

namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(int id);
        ImmutableList<TEntity> GetAll(bool asNoTracking = true);
        int Count(Expression<Func<TEntity, bool>> predicate);
        ImmutableList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}