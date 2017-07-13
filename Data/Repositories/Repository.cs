﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : Entity
    {
        public Repository(TContext context)
        {
            Context = context;
            Entries = context.Set<TEntity>();
        }

        protected TContext Context { get; set; }
        public DbSet<TEntity> Entries { get; set; }

        public TEntity Get(int id)
        {
            return Entries.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entries.ToList();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.Count(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.Where(predicate).ToList();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.FirstOrDefault(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entries.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Entries.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entries.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Entries.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entries.RemoveRange(entities);
        }
    }
}