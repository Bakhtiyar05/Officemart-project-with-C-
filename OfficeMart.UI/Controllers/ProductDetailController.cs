using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.UI.ViewModels;

namespace OfficeMart.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly OfficeMartContext _context;
        public ProductDetailController(OfficeMartContext context)
        {
            _context = context;
        }
        [Route("/Məhsul_Haqqında/{id?}")]
        public IActionResult Index(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.GUID == id);
            if (product == null) return NotFound();

            var randomProducts = _context.Products
                .Where(p => p.CategoryGUID == product.CategoryGUID && p.GUID != id)
                .OrderBy(x => Guid.NewGuid())
                .Take(8)
                .ToList();

            var model = new ProductDetailViewModel
            {
                MainProduct = product,
                RelatedProducts = randomProducts
            };

            return View(model);
        }
    }
}
