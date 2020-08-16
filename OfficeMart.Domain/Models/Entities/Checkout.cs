using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        public int OrderCount { get; set; }
        public decimal SaledPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string CheckoutNumber { get; set; }
    }
}
