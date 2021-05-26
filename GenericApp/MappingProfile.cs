using AutoMapper;
using GenericApp.Domain.Models;
using GenericApp.ViewModels;

namespace GenericApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeVM, Employee> ()
                .ForMember(e => e.CompanyId, o => o.MapFrom(v => v.Company.Id)).ReverseMap();
            CreateMap<Company, CompanyVM>().ReverseMap();
        }

        public MappingProfile(string profileName) : base(profileName)
        {
            
        }
    }
}
