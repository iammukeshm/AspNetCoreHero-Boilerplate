using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AspNetCoreHero.Web.Helpers;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHero.Web.Areas.Admin.Pages
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesModel : HeroPageModel<RolesModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public IEnumerable<RolesViewModel> Roles { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Roles = Mapper.Map<IEnumerable<RolesViewModel>>(roles);
            return new PartialViewResult
            {
                ViewName = "_ViewAllRoles",
                ViewData = new ViewDataDictionary<IEnumerable<RolesViewModel>>(ViewData, Roles)
            };

        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<RolesViewModel>("_CreateOrEditRoles", new RolesViewModel()) });
            else
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) throw new Exception();
                var roleviewModel = Mapper.Map<RolesViewModel>(role);
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<RolesViewModel>("_CreateOrEditRoles", roleviewModel) });
            }


        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(string id, RolesViewModel role)
        {
            if (ModelState.IsValid && role.Name!= "SuperAdmin" && role.Name != "Basic")
            {
                if (string.IsNullOrEmpty(id))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role.Name));
                    Notify.AddSuccessToastMessage("Role Created");
                }
                else
                {
                    var existingRole = await _roleManager.FindByIdAsync(id);
                    existingRole.Name = role.Name;
                    existingRole.NormalizedName = role.Name.ToUpper();
                    await _roleManager.UpdateAsync(existingRole);
                    Notify.AddSuccessToastMessage("Role Updated");
                }

                var roles = await _roleManager.Roles.ToListAsync();
                Roles = Mapper.Map<IEnumerable<RolesViewModel>>(roles);
                var html = await Renderer.RenderPartialToStringAsync("_ViewAllRoles", Roles);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await Renderer.RenderPartialToStringAsync<RolesViewModel>("_CreateOrEdit", role);
                return new JsonResult(new { isValid = false, html = html });
            }


        }
        public async Task<JsonResult> OnPostDeleteAsync(string id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id);
            if(existingRole.Name!="SuperAdmin" && existingRole.Name != "Basic")
            {
                //TODO Check if Any Users already uses this Role
                bool roleIsNotUsed = true;
                var allUsers = await _userManager.Users.ToListAsync();
                foreach(var user in allUsers)
                {
                    if(await _userManager.IsInRoleAsync(user,existingRole.Name))
                    {
                        roleIsNotUsed = false;
                    }
                }   
                if(roleIsNotUsed)
                {
                    await _roleManager.DeleteAsync(existingRole);
                }
                else
                {
                    Notify.AddErrorToastMessage("Role is being Used by another User. Cannot Delete");
                }
               
            }
            var roles = await _roleManager.Roles.ToListAsync();
            Roles = Mapper.Map<IEnumerable<RolesViewModel>>(roles);
            var html = await Renderer.RenderPartialToStringAsync("_ViewAllRoles", Roles);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
