using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int OrderCount { get; set; }
        public decimal SaledPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string CheckoutNumber { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage ="Sahə tələbolunandır")]
        public string DeliveryAddress { get; set; }
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string BuyerPhone { get; set; }
        public DateTime RegDate { get; set; }
        public ProductDto Product { get; set; }
        public int ProductId { get; set; }
    }
}
