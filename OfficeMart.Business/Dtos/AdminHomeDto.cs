using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class AdminHomeDto
    {
        public int VisitorsCount { get; set; }
        public int UsersCount { get; set; }
        public decimal TotalSalesPrice { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}
