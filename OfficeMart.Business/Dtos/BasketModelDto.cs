using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class BasketModelDto
    {
        public int ProductCount { get; set; }
        public decimal Sum
        {
            get
            {
                return 0;
            }
            set { }
        }
    }
}
