using AutoMapper;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Models;

namespace GenericApp.Infra.CC.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
        }

        public MappingProfile(string profileName) : base(profileName)
        {

        }
    }
}
