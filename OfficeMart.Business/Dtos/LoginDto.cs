using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
