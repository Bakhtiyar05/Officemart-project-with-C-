using Microsoft.AspNetCore.Http;
using OfficeMart.Business.Dtos.LibraryDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfficeMart.Business.Dtos
{
    public class ProductDto : BaseDto
    {
        public ProductDto()
        {
            ImagesBase64 = new List<string>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Sahə tələb olunandır")]
        [MaxLength(75, ErrorMessage = "Maksimum uzunluq 75 simvoldan ibarət ola bilər")]
        [MinLength(3, ErrorMessage = "Minumum uzunluq 3 simvoldan ibarət ola bilər")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Sahə tələb olunandır")]
        [RegularExpression(@"(\+|-)?[0-9]+(\.[0-9]*)?", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public decimal Price { get; set; }

        [RegularExpression(@"(\+|-)?[0-9]+(\.[0-9]*)?", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public decimal DiscountPrice { get; set; }

        [Required(ErrorMessage = "Sahə tələb olunandır")]
        public int Count { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Səkil tələb olunandır")]
        public IFormFile Image { get; set; }
        public string Size { get; set; }
        public IFormFile ImageForEdit { get; set; }
        public DateTime RegDate { get; set; }
        public List<string> ProductImages { get; set; }
        public List<string> ImagesBase64 { get; set; }
        public int CategoryCount { get; set; }
        public CategoryDto Category { get; set; }
        public List<CategoryDto> CategoryDtos { get; set; }

        [Required(ErrorMessage = "Sahə tələb olunandır")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Yalnız rəqəm daxil edə bilərsiz")]
        public int CategoryId { get; set; }
        public ColorDto Color { get; set; }
        public int ColorId { get; set; }
        public ProductSizeDto ProductSize { get; set; }
        public int ProductSizeId { get; set; }
        public PaginationDto PaginationDto { get; set; }
        public bool IsActive { get; set; }
        public bool IsSpecial { get; set; }
    }
}
