using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class AppUserDto
    {
        [Required(ErrorMessage ="Sahə tələbolunandır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Password { get; set; }

        public string LivinPlace { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string PhoneNumber { get; set; }
    }
}
