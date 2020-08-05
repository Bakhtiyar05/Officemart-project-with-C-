using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
   public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public ICollection<Product>  Products { get; set; }
    }
}
