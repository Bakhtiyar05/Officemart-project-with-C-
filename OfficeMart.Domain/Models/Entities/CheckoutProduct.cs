using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class CheckoutProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime RegDate { get; set; }
    }
}
