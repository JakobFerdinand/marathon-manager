using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;
using Core.Repositories;
using System.Linq;
using System.Collections.Immutable;

namespace Data.Sample.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ImmutableList<Category> _categories = ImmutableList.Create(
            new Category { Id = 1, Name = "Laufen 10km", PlannedStartTime = new DateTime(2017, 10, 08, 10, 00, 00) },
            new Category { Id = 1, Name = "Laufen 3.8km", PlannedStartTime = new DateTime(2017, 10, 08, 10, 00, 00) },
            new Category { Id = 1, Name = "Walken 10km", PlannedStartTime = new DateTime(2017, 10, 08, 09, 30, 00) },
            new Category { Id = 1, Name = "Walken 3.8km", PlannedStartTime = new DateTime(2017, 10, 08, 09, 30, 00) });

        public void Add(Category entity) => _categories.Add(entity);
        public void AddRange(IEnumerable<Category> entities) => _categories.AddRange(entities);
        public int Count(Expression<Func<Category, bool>> predicate) => _categories.Count(predicate.Compile());
        public ImmutableList<Category> Find(Expression<Func<Category, bool>> predicate) => _categories.Where(predicate.Compile()).ToImmutableList();
        public Category FirstOrDefault(Expression<Func<Category, bool>> predicate) => _categories.FirstOrDefault();
        public Category Get(int id) => _categories.SingleOrDefault(c => c.Id == id);
        public ImmutableList<Category> GetAll(bool asNotTracking = false)
            => !asNotTracking
            ? _categories
            : _categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                PlannedStartTime = c.PlannedStartTime,
                Starttime = c.Starttime
            }).ToImmutableList();

        public ImmutableList<Category> GetNotStarted() => _categories.Where(c => c.Starttime == null).ToImmutableList();
        public void Remove(Category entity) => _categories.Remove(entity);
        public void RemoveRange(IEnumerable<Category> entities)
        {
            foreach (var c in entities)
                _categories.Remove(c);
        }
        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate) => _categories.SingleOrDefault(predicate.Compile());
    }
}
