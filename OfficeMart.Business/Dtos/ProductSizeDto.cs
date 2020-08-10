using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
   public class ProductSizeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string Size { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
