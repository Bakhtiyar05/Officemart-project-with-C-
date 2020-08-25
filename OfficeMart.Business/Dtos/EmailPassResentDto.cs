using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
   public class EmailPassResentDto
    {
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6, ErrorMessage = "Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        public string ConfPassword { get; set; }

        [Required(ErrorMessage = "Sahə tələbolunandır")]
        [MinLength(6, ErrorMessage = "Şifrənin uzunluğu minimum 6 simvoldan ibarət olmalıdır")]
        [Compare("ConfPassword", ErrorMessage = "Yeni şifrələr uyumsuzdur")]
        public string SecondConfPassword { get; set; }

        public string email { get; set; }
    }
}
