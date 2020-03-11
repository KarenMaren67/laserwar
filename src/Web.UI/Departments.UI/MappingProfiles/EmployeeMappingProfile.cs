using Application.Validation.Emplyee;
using AutoMapper;
using Departments.UI.Models.Employees;

namespace Departments.UI.MappingProfiles
{
    public class EmployeeViewModelMappingProfile : Profile
    {
        public EmployeeViewModelMappingProfile()
        {
            CreateMap<AddEmployeeViewModel, Employee>()
               .ForMember(x => x.Id, opt => opt.MapFrom(src => 0))
               .ForMember(x => x.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId))
               .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position))
               .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
