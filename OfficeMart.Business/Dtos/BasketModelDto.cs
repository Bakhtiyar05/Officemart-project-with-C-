using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class BasketModelDto
    {
        public int ProductCount { get; set; }
        public ProductDto Product { get; set; }

        public decimal Sum
        {
            get
            {
                return ProductCount * Product.Price;
            }
            set { }
        }

        public CheckoutDto CheckoutDto { get; set; }
    }
}
