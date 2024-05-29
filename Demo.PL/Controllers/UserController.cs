using AutoMapper;
using demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var User = await _userManager.Users.Select(
                    U=> new UserViewModel
                    {
                        Id = U.Id,
                        Fname =U.FName,
                        Lname=U.LName,
                        Email =U.Email,
                        PhoneNumber =U.PhoneNumber,
                        Roles= _userManager.GetRolesAsync(U).Result
                    }).ToListAsync();
                return View(User);
            }
            else
            {
                var User=await _userManager.FindByEmailAsync(searchValue);
                var mappeduser = new UserViewModel()
                {
                    Id = User.Id,
                    Fname = User.FName,
                    Lname = User.LName,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(User).Result
                };
                return View(new List<UserViewModel> { mappeduser});
            }
        }
        public async Task<IActionResult> Details(string Id,string ViewName = "Details")
        {
            if(Id is null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(Id);
            if (User is null)
                return NotFound();
            var MappedUser = _mapper.Map<ApplicationUser,UserViewModel>(User);
            return View(ViewName,MappedUser);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model,[FromRoute] string Id)
        {
            if(Id !=model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var User = await _userManager.FindByIdAsync(Id);
                    User.PhoneNumber = model.PhoneNumber;
                    User.FName = model.Fname;
                    User.LName= model.Lname;    
                    await _userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message); //mynf3sh ashel elcurly brack bta3 try catch 7ta lw str
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            return await Details(Id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string Id)
        {
            try
            {
                var User= await _userManager.FindByIdAsync(Id);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));

            }
            catch(Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
       
    }
}
