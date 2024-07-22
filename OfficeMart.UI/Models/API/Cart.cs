using System.Collections.Generic;
using Newtonsoft.Json;

namespace OfficeMart.UI.Models.API
{
	public class Cart
	{
        public string ClientCode { get; set; }
        public List<CartProduct> Products { get; set; }
    }
}

