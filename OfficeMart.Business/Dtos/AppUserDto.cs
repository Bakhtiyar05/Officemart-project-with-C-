﻿using OfficeMart.Business.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeMart.Business.Dtos
{
    public class AppUserDto
    {
        [Required(ErrorMessage ="Sahə tələbolunandır")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [EmailAddress(ErrorMessage ="Email fortmata uyğun deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6,ErrorMessage ="Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        public string Password { get; set; }

        public string Adrress { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string PhoneNumber { get; set; }

        public LoginDto LoginDto { get; set; }
        public LogicResult LogicResult { get; set; }

        public DateTime PasswordResetDate { get; set; }
        public bool IsPasswordReset { get; set; }

        public bool IsPolicyAccepted { get; set; }
    }
}
