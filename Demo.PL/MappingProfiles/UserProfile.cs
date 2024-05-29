using AutoMapper;
using demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}   
