using AutoMapper;
using Core.Models;
using Core.Repositories;
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using UI.JsonImport.Services;

namespace UI.JsonImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\jw2\Desktop\runnerdata.json";

            var filereader = new StreamReader(path);
            var json = filereader.ReadToEnd();

            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True")
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                var runnerRepository = new RunnerRepository(context);
                using (var unitOfWork = new UnitOfWork(context, categoryRepository, runnerRepository))
                {
                    var mapperConfiguration = GetMapperConfiguration(unitOfWork.Categories);
                    var mapper = new Mapper(mapperConfiguration);

                    var deserializer = new JsonDeserializer(mapper);

                    var runners = deserializer.Deserialize(json);

                    runnerRepository.AddRange(runners);
                    unitOfWork.Complete();
                }
            }
        }

        private static IConfigurationProvider GetMapperConfiguration(ICategoryRepository categoryRepository)
        {
            var categories = categoryRepository.GetAll(asNotTracking: true);

            return new MapperConfiguration(c =>
            {
                c.CreateMap<Models.Runner, Runner>()
                    .ForMember(r => r.Gender, map => map.MapFrom(r => r.Geschlecht == "Mann" ? Gender.Mann : Gender.Frau))
                    .ForMember(r => r.Firstname, map => map.MapFrom(r => r.Vorname))
                    .ForMember(r => r.Lastname, map => map.MapFrom(r => r.Nachname))
                    .ForMember(r => r.YearOfBirth, map => map.MapFrom(r => int.Parse(r.Geburtsdatum.Substring(0, 4))))
                    .ForMember(r => r.City, map => map.MapFrom(r => r.Wohnort))
                    .ForMember(r => r.Email, map => map.MapFrom(r => r.eMail))
                    .ForMember(r => r.SportsClub, map => map.MapFrom(r => r.Verein))
                    .ForMember(r => r.CategoryId, map => map.MapFrom(r => categories.Single(category => category.Name == r.Strecken).Id))
                    .ForMember(r => r.Id, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.Category, map => map.Ignore())
                    .ForMember(r => r.ChipId, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.TimeAtDestination, map => map.Ignore())
                    .ForMember(r => r.Startnumber, map => map.Ignore());

            });
        }
    }
}
