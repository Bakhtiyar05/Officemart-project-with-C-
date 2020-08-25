using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage ="Şəkil tələbolunandır")]
        public IFormFile Image { get; set; }
        public IFormFile ImageForEdit { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
