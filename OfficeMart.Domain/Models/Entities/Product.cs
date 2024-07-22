using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string GUID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string DescriptionImages { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public bool Status { get; set; }
        public string CategoryGUID { get; set; }
        public string CategoryName { get; set; }
    }
}
