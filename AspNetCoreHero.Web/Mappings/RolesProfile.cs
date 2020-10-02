using AspNetCoreHero.Web.Areas.Admin.Pages;
using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Mappings
{
    class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<IdentityRole, RolesViewModel>().ReverseMap();
        }
    }
}
