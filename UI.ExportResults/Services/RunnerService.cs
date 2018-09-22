using Core.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Linq;
using UI.ExportResults.Models;

namespace UI.ExportResults.Services
{
    public class RunnerService
    {
        private readonly RunnerDbContext dbContext;

        public RunnerService(RunnerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private ExportRunner exportRunners(Runner runner, int index)
            => new ExportRunner(
                rang: index + 1,
                startnummer: runner.Startnumber ?? 0,
                vorname: runner.Firstname,
                nachname: runner.Lastname,
                geschlecht: runner.Gender,
                verein: runner.SportsClub,
                geburtsjahr: runner.YearOfBirth,
                zeit: runner.RunningTime ?? throw new InvalidOperationException("The query returned a runner without runningtime."));

        public (ImmutableList<ExportRunner> männer, ImmutableList<ExportRunner> frauen) GetResultsForCategory(int categoryId)
        {
            var runners = dbContext.Runners.Include(r => r.Category)
                .Where(r => r.CategoryId == categoryId)
                .Where(r => r.RunningTime != null)
                .OrderBy(r => r.RunningTime)
                .ToImmutableList();

            return (
                runners.Where(r => r.Gender == Gender.Mann).Select(exportRunners).ToImmutableList(),
                runners.Where(r => r.Gender == Gender.Frau).Select(exportRunners).ToImmutableList()
                );
        }

        public (ImmutableList<ExportRunner> männer, ImmutableList<ExportRunner> frauen) GetOldestRunner()
        {
            var maleRunnersQuery = dbContext.Runners.Include(r => r.Category)
                .Where(r => r.RunningTime != null)
                .Where(r => r.Gender == Gender.Mann);
            var femaleRunnersQuery = dbContext.Runners.Include(r => r.Category)
                .Where(r => r.RunningTime != null)
                .Where(r => r.Gender == Gender.Frau);

            var yearOfBirthOfOldesdMaleRunner = maleRunnersQuery.Min(r => r.YearOfBirth);
            var yearOfBirthOfOldesdFemaleRunner = femaleRunnersQuery.Min(r => r.YearOfBirth);

            return (
                maleRunnersQuery.Where(r => r.YearOfBirth == yearOfBirthOfOldesdMaleRunner).Select(exportRunners).ToImmutableList(),
                femaleRunnersQuery.Where(r => r.YearOfBirth == yearOfBirthOfOldesdFemaleRunner).Select(exportRunners).ToImmutableList()
                );
        }

        public ImmutableList<ExportSportsclub> GetSportclubsRangs()
            => dbContext.Runners
            .Where(r => r.RunningTime != null)
            .GroupBy(r => r.SportsClub)
            .Select((g, i) => new ExportSportsclub(i + 1, g.Key, g.Count()))
            .ToImmutableList();
    }
}
