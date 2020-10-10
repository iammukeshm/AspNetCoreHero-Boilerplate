using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AspNetCoreHero.Web.Helpers;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AspNetCoreHero.Web.Areas.Admin.Pages
{
    public class ClaimsModel : HeroPageModel<ClaimsModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ClaimsModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public string RoleId { get; set; }
        [BindProperty]
        public IList<RoleClaimsViewModel> RoleClaims { get; set; }
        public void OnGet()
        {
        }
        public async Task OnGetViewAll(string roleId)
        {
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(MasterPermissions), roleId);
            allPermissions.GetPermissions(typeof(ProductPermissions), roleId);
            allPermissions.GetPermissions(typeof(ProductCategoryPermissions), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var claimsModel = Mapper.Map<List<RoleClaimsViewModel>>(claims);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claimsModel.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            RoleClaims = Mapper.Map<List<RoleClaimsViewModel>>(allPermissions);
            ViewData["Title"] = $"{role.Name} Claims";
            ViewData["Caption"] = $"Manage {role.Name} Claims";
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            //Remove all Claims First
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = RoleClaims.Where(a => a.Selected).ToList();
            foreach(var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            var user = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(user);
            Notify.AddSuccessToastMessage($"Updated Claims / Permissions for Role '{role.Name}'");
            return RedirectToPage("/roles", new { area = "Admin" });
        }
       


    }
}
