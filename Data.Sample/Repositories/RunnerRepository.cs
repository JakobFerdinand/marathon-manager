using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Tynamix.ObjectFiller;

namespace Data.Sample.Repositories
{
    public class RunnerRepository : IRunnerRepository
    {
        private List<Runner> _runners;

        public RunnerRepository(CategoryRepository categoryRepository)
        {
            var runnerFiller = new Filler<Runner>();
            runnerFiller.Setup()
                .OnProperty(r => r.Id).IgnoreIt()
                .OnProperty(r => r.Category).IgnoreIt()
                .OnProperty(r => r.CategoryId).IgnoreIt()
                .OnProperty(r => r.ChipId).IgnoreIt()
                .OnProperty(r => r.RunningTime).IgnoreIt()
                .OnProperty(r => r.TimeAtDestination).IgnoreIt()
                .OnProperty(r => r.Startnumber).Use(new IntRange(1, 500))
                .OnProperty(r => r.YearOfBirth).Use(new IntRange(DateTime.Now.Year - 70, DateTime.Now.Year - 3))
                .OnProperty(r => r.Firstname).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(r => r.Lastname).Use(new RealNames(NameStyle.LastName))
                .OnProperty(r => r.Email).Use(new EmailAddresses())
                .OnProperty(r => r.SportsClub).Use(new RandomListItem<string>(
                    "SC Mining", null, "SV Ried", "SV Altheim"));

            var runners = runnerFiller.Create(30);
            var random = new Random();
            var categories = categoryRepository.GetAll().ToList();
            foreach (var r in runners)
            {
                r.Category = categories[random.Next(0, categories.Count)];
                r.CategoryId = r.Category.Id;
            }
            _runners = runners.ToList();
        }

        public void Add(Runner entity) => _runners.Add(entity);
        public void AddRange(IEnumerable<Runner> entities) => _runners.AddRange(entities);
        public int Count(Expression<Func<Runner, bool>> predicate) => _runners.Count(predicate.Compile());
        public IEnumerable<Runner> Find(Expression<Func<Runner, bool>> predicate) => _runners.Where(predicate.Compile()).ToList();
        public Runner FirstOrDefault(Expression<Func<Runner, bool>> predicate) => _runners.FirstOrDefault(predicate.Compile());
        public Runner Get(int id) => _runners.SingleOrDefault(r => r.Id == id);
        public IEnumerable<Runner> GetAll(bool asNotTracking = false)
        {
            if (asNotTracking)
                return _runners;

            var temp = new List<Runner>();
            foreach (var r in _runners)
                temp.Add(new Runner
                {
                    Id = r.Id,
                    Gender = r.Gender,
                    Firstname = r.Firstname,
                    Lastname = r.Lastname,
                    Category = r.Category,
                    CategoryId = r.CategoryId,
                    ChipId = r.ChipId,
                    City = r.City,
                    Email = r.Email,
                    RunningTime = r.RunningTime,
                    SportsClub = r.SportsClub,
                    Startnumber = r.Startnumber,
                    TimeAtDestination = r.TimeAtDestination,
                    YearOfBirth = r.YearOfBirth
                });
            return temp;
        }
        public Task<IEnumerable<Runner>> GetAllWithRelated(bool asNoTracking = false) => throw new NotImplementedException();
        public Runner GetIfHasNoTimeWithCategory(string chipId) => _runners.SingleOrDefault(r => r.RunningTime == null && r.ChipId == chipId);
        public void Remove(Runner entity) => _runners.Remove(entity);
        public void RemoveRange(IEnumerable<Runner> entities)
        {
            foreach (var r in entities)
                _runners.Remove(r);
        }
        public Runner SingleOrDefault(Expression<Func<Runner, bool>> predicate) => _runners.SingleOrDefault(predicate.Compile());
    }
}
