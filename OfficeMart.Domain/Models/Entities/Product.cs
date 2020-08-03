using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
