using AspNetCoreHero.Application.DTOs.User;
using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreHero.PublicAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            CurrentUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            CurrentUsername = "";
        }

        public string CurrentUserId { get; }

        public string CurrentUsername { get; }

        public async Task<Response<UserDetailsResponse>> GetUserDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user==null) throw new NotFoundException(nameof(ApplicationUser),userId);
            var response =  new UserDetailsResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return new Response<UserDetailsResponse> { Data = response };

        }
    }
}
