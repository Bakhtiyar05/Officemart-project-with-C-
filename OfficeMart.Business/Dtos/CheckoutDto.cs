using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class CheckoutDto
    {
        public int Id { get; set; }
        public int OrderCount { get; set; }
        public decimal SaledPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string CheckoutNumber { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }

        [Required(ErrorMessage ="Sahə tələbolunandır")]
        public string DeliveryAddress { get; set; }
        [Required(ErrorMessage = "Sahə tələbolunandır")]
        public string BuyerPhone { get; set; }
    }
}
