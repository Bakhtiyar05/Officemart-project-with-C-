
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LivingPlace { get; set; }
        public DateTime PasswordResetDate { get; set; }
        public bool IsPasswordReset { get; set; }
        public bool IsAdmin { get; set; }
    }
}
