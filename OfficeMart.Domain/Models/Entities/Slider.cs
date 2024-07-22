using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace OfficeMart.Domain.Models.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string UrlPath { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Sahə tələb olunandır")]
        [MinLength(2, ErrorMessage = "Minimum uzunluq 2 hərfdən ibarət olmalıdır")]
        public string Title { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
