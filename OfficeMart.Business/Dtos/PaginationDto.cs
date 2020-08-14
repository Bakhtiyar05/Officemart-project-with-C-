using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class PaginationDto
    {
        public decimal TotalItemsCount { get; set; }
        public decimal ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int CategoryId { get; set; }
        public string AspAction { get; set; }
        public string AspController { get; set; }
    }
}
