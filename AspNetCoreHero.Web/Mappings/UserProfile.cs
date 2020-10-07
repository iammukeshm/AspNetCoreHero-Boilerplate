using AspNetCoreHero.Infrastructure.Persistence.Identity;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AspNetCoreHero.Web.Areas.Products.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
