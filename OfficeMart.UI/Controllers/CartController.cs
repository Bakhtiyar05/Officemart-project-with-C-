using Microsoft.AspNetCore.Mvc;
using OfficeMart.UI.Models.API;
using System.Collections.Generic;
using Newtonsoft.Json;
using static OfficeMart.UI.API.NetworkManager;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OfficeMart.Domain.Models.AppDbContext;
using System.Net.Http;
using System.Linq;

namespace OfficeMart.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly OfficeMartContext _context;

        public CartController(IConfiguration configuration, OfficeMartContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Checkout(string productsJson)
        {
            List<CartProduct> products = JsonConvert.DeserializeObject<List<CartProduct>>(productsJson);
            ViewBag.policy = _context.OrderPolicy.FirstOrDefault().Policy;
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Order([FromBody] List<CartProduct> products)
        {
            string clientCode = Request.Cookies["ClientCode"];
            if (string.IsNullOrEmpty(clientCode)) return Json(new { success = false, message = "Sifarişi təsdiq etmək üçün qeydiyyatdan keçin." });
            if (products == null || products.Count <= 0) return Json(new { success = false, message = "Səbət boşdur." });
            var request = new Cart { ClientCode = clientCode, Products = products };

            string jsonBody = JsonConvert.SerializeObject(request);

            CartResponse response = await OrderAsync(jsonBody);
            if (response.Success)
                return Json(new { success = true, message = "Sifarişi təsdiq olundu." });
            else
                return Json(new { success = true, message = response.Message });
        }

        private async Task<CartResponse> OrderAsync(string jsonBody)
        {
            var cartAPIResponse = await SendRequestAsync<CartResponse>("Sale", _configuration, HttpMethod.Post, jsonBody);
            return cartAPIResponse ?? new CartResponse();
        }
    }
}
