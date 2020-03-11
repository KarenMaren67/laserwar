using Application.Validation.Department;
using AutoMapper;
using Departments.UI.Models.Departments;

namespace Departments.UI.MappingProfiles
{
    public class DepartmentViewModelMappingProfile : Profile
    {
        public DepartmentViewModelMappingProfile()
        {
            CreateMap<DepartmentDetailsViewModel, Department>();
        }
    }
}
