using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : Entity
    {
        public Repository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context), $"{nameof(context)} can not be null.");
            Entries = context.Set<TEntity>();
        }

        protected TContext Context { get; }
        public DbSet<TEntity> Entries { get; }

        public TEntity Get(int id) => Entries.Find(id);
        public ImmutableList<TEntity> GetAll(bool asNotTracking = false)
            => asNotTracking
            ? Entries.AsNoTracking().ToImmutableList()
            : Entries.ToImmutableList();
        public int Count(Expression<Func<TEntity, bool>> predicate) => Entries.Count(predicate);
        public ImmutableList<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => Entries.Where(predicate).ToImmutableList();
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => Entries.FirstOrDefault(predicate);
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) => Entries.SingleOrDefault(predicate);
        public void Add(TEntity entity) => Entries.Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => Entries.AddRange(entities);
        public void Remove(TEntity entity) => Entries.Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => Entries.RemoveRange(entities);
    }
}