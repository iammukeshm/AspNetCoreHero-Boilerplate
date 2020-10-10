using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AspNetCoreHero.Application.Enums.Identity;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCoreHero.Web.Areas.Admin.Pages
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersModel : HeroPageModel<UsersModel>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersModel> _logger;
        private readonly IEmailSender _emailSender;
        public IEnumerable<UserViewModel> Users { get; set; }
        public UsersModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<UsersModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAll()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            Users = Mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);
            return new PartialViewResult
            {
                ViewName = "_ViewAllUsers",
                ViewData = new ViewDataDictionary<IEnumerable<UserViewModel>>(ViewData, Users)
            };

        }
        public async Task<JsonResult> OnGetCreateAsync()
        {
            return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<UserViewModel>("_CreateUser", new UserViewModel()) });
        }
        public async Task<JsonResult> OnPostCreateAsync(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(userModel.Email);
                string userName = address.User;
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
                    Users = Mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);
                    var htmlData = await Renderer.RenderPartialToStringAsync("_ViewAllUsers", Users);
                    return new JsonResult(new { isValid = true, html = htmlData });
                }
                foreach (var error in result.Errors)
                {
                    Notify.AddErrorToastMessage(error.Description);
                }
                var html = await Renderer.RenderPartialToStringAsync<UserViewModel>("_CreateUser", userModel);
                return new JsonResult(new { isValid = false, html = html });

            }

            return default;
        }
    }
}
