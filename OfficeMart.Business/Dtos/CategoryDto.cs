using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsActive { get; set; }
    }
}
