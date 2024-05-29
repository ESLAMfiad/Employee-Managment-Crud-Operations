using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.MappingProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleView>().ForMember(d=>d.RoleName,o=>o.MapFrom(s=>s.Name)).ReverseMap();
        }
    }
}
