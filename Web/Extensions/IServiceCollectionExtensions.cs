using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAutoMapper<TProfile>(this IServiceCollection services, params TProfile[] mappingProfiles)
            where TProfile : Profile
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in mappingProfiles)
                    cfg.AddProfile(profile);
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
