using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string LivingPlace { get; set; }
    }
}
