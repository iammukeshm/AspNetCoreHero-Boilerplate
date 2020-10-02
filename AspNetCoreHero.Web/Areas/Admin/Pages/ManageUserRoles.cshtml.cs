using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreHero.Web.Areas.Admin.Pages
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManageUserRolesModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public IList<ManageUserRolesViewModel> UserRoles { get; set; }
        public void OnGet()
        {
           
        }
        public async Task OnGetViewAll(string userId)
        {
            UserId = userId;
            var viewModel = new List<ManageUserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            ViewData["Title"] = $"{user.UserName} - Roles";
            ViewData["Caption"] = $"Manage {user.Email}'s Roles.";
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            UserRoles = viewModel;
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);
            //Clean up Existing Roles
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Infrastructure.Persistence.Seeds.IdentityContextSeed.SeedAdminAsync(_userManager, _roleManager);
            return RedirectToPage("/users", new { area = "Admin" });
        }

    }
}
