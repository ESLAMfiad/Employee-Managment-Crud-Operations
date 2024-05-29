using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,  IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {

            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles =await _roleManager.Roles.ToListAsync();
                var MappedRole= _mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RoleView> >(Roles);
                return View(MappedRole);
            }
            else
            {
               var Roles= await _roleManager.FindByIdAsync(SearchValue);
               var MappedRole = _mapper.Map<IdentityRole,RoleView>(Roles);
               return View(new List <RoleView>() { MappedRole});

            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleView model)
        {
            if (ModelState.IsValid)
            {
                var mappedrole= _mapper.Map<RoleView,IdentityRole>(model);
                await _roleManager.CreateAsync(mappedrole);
                return RedirectToAction("Index");
            }
            return View(model); 
        }


        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role is null)
                return NotFound();
            var MappedRole = _mapper.Map<IdentityRole, RoleView>(Role);
            return View(ViewName, MappedRole);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleView model, [FromRoute] string Id)
        {
            if (Id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(Id);
                    Role.Name = model.RoleName;
                    await _roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
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
                var Role = await _roleManager.FindByIdAsync(Id);
                await _roleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
