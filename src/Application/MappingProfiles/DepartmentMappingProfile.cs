using Application.Validation.Department;
using AutoMapper;
using Domain.DbEntities;

namespace Application.MappingProfiles
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentEntity>()
               .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : 0))
               .ForMember(x => x.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId))
               .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(x => x.HasEmployees, opt => opt.MapFrom(src => src.HasEmployees));

            CreateMap<DepartmentEntity, Department>()
               .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(x => x.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId))
               .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(x => x.HasEmployees, opt => opt.MapFrom(src => src.HasEmployees));
        }
    }
}
