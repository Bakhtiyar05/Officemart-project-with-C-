using System;
using System.Collections.Generic;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.ViewModels
{
	public class ProductDetailViewModel
	{
		public Product MainProduct { get; set; }
		public List<Product> RelatedProducts { get; set; }
	}
}

