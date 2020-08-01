using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class ProductImages
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
    }
}
