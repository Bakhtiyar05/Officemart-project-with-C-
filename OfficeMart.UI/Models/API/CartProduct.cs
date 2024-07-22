
namespace OfficeMart.UI.Models.API
{
	public class CartProduct
	{
		public string ProductCode { get; set; }
		public int Quantity { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string CategoryName { get; set; }
        public string CategoryGUID { get; set; }
    }
}

