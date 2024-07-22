using System;
using System.Collections.Generic;
using OfficeMart.UI.Models.API;

namespace OfficeMart.UI.ViewModels
{
	public class ProductsViewModel
	{
        public List<Domain.Models.Entities.Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}

