using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(75,ErrorMessage ="Maksimum uzunluq 75 simvoldan ibarət ola bilər")]
        [MinLength(3, ErrorMessage = "Minumum uzunluq 3 simvoldan ibarət ola bilər")]
        public string ProductName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public decimal DiscountPrice { get; set; }
        public string ImageName { get; set; }
        public DateTime RegDate { get; set; }
        public Category Category { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public int CategoryId { get; set; }
    }
}
