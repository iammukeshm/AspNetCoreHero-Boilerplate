using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
        public string ActivatedBy { get; set; }
    }
}
