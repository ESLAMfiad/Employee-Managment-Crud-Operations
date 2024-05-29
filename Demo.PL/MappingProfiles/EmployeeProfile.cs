using AutoMapper;
using demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap(); // .ForMember(d=>d.Name,O=>O.MapFrom(S=>S.EmpName)
        }
    }
}
