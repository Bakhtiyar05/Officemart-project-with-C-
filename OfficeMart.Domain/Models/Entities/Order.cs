using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderCount { get; set; }
        public decimal SaledPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string DeliveryAddress { get; set; }
        public string BuyerPhone { get; set; }
        public OrderNumber OrderNumber { get; set; }
        public int OrderNumberId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime RegDate { get; set; }
    }
}
