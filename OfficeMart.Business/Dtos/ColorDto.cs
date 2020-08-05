using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Business.Dtos
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
