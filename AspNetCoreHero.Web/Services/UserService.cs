using AspNetCoreHero.Application.DTOs.User;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Services
{
    public class UserService : IUserService
    {
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            CurrentUserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            CurrentUsername = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
        }

        public string CurrentUserId { get; }
        public string CurrentUsername { get; }

        Task<Response<UserDetailsResponse>> IUserService.GetUserDetails(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
