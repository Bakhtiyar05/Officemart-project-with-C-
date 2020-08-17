using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
   public class OrderNumberDto
    {
        public OrderNumberDto()
        {
            Orders = new List<OrderDto>();
        }
        public int Id { get; set; }
        public string OrderCheckNumber { get; set; }
        public string BuyerUserId { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
