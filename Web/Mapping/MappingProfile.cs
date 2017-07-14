using AutoMapper;
using Core.Models;
using Web.Controllers.Resources;

namespace Web.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Gender, GenderResource>();
            CreateMap<Runner, RunnerResource>()
                .ForMember(rr => rr.CategoryName, opt => opt.MapFrom(r => r.Category.Name));
        }
    }
}
