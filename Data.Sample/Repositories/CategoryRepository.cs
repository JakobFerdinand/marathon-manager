using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;
using Core.Repositories;
using System.Linq;

namespace Data.Sample.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Laufen 10km", PlannedStartTime = new DateTime(2017, 10, 08, 10, 00, 00) },
            new Category { Id = 1, Name = "Laufen 3.8km", PlannedStartTime = new DateTime(2017, 10, 08, 10, 00, 00) },
            new Category { Id = 1, Name = "Walken 10km", PlannedStartTime = new DateTime(2017, 10, 08, 09, 30, 00) },
            new Category { Id = 1, Name = "Walken 3.8km", PlannedStartTime = new DateTime(2017, 10, 08, 09, 30, 00) },
        };

        public void Add(Category entity) => _categories.Add(entity);
        public void AddRange(IEnumerable<Category> entities) => _categories.AddRange(entities);
        public int Count(Expression<Func<Category, bool>> predicate) => _categories.Count(predicate.Compile());
        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate) => _categories.Where(predicate.Compile()).ToList();
        public Category FirstOrDefault(Expression<Func<Category, bool>> predicate) => _categories.FirstOrDefault();
        public Category Get(int id) => _categories.SingleOrDefault(c => c.Id == id);
        public IEnumerable<Category> GetAll(bool asNotTracking = false)
        {
            if (!asNotTracking)
                return _categories;

            return _categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                PlannedStartTime = c.PlannedStartTime,
                Starttime = c.Starttime
            }).ToList();
        }
        public IEnumerable<Category> GetNotStarted() => _categories.Where(c => c.Starttime == null).ToList();
        public void Remove(Category entity) => _categories.Remove(entity);
        public void RemoveRange(IEnumerable<Category> entities)
        {
            foreach (var c in entities)
                _categories.Remove(c);
        }
        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate) => _categories.SingleOrDefault(predicate.Compile());
    }
}
