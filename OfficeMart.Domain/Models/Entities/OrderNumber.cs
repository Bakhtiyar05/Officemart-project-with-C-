using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class OrderNumber
    {
        public OrderNumber()
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        public string OrderCheckNumber { get; set; }
        public string BuyerUserId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime RegDate { get; set; }
    }
}
