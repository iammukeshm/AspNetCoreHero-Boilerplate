using AspNetCoreHero.Application.DTOs.User;
using AspNetCoreHero.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Shared
{
    public interface IUserService
    {
        string CurrentUserId { get; }
        string CurrentUsername { get; }

        Task<Response<UserDetailsResponse>> GetUserDetails(string userId);

    }
}
