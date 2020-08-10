using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class ProductSize
    {
        public ProductSize()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Size { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
