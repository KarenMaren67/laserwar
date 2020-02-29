using Application.Validation.Emplyee;
using AutoMapper;
using Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeEntity>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.HasValue ? src.Id.Value : 0))
                .ForMember(x => x.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<EmployeeEntity, Employee>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.ParentDepartmentId, opt => opt.MapFrom(src => src.ParentDepartmentId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
