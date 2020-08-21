using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class RestorePasswordDto
    {
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6, ErrorMessage = "Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6, ErrorMessage = "Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        public string ConfPassword { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6, ErrorMessage = "Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        [Compare("ConfPassword",ErrorMessage ="Yeni şifrələr uyumsuzdur")]
        public string SecondConfPassword { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string PhoneNumber { get; set; }
    }
}
