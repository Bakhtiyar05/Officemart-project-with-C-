using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ProductController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var productsDto = await new ProductLogic().GetProducts();
            return View(productsDto);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(ProductDto productDto, List<string> src)
        {

            if (!ModelState.IsValid && src.Count == 0)
            {
                return View(productDto);
            }

            await new ProductLogic().Add(productDto, _environment.WebRootPath, src);
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = new ProductLogic().GetProductById(id).Result;
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductDto product, List<string> src)
        {

            bool imageOpsIsOkay = new ProductLogic().ProductEditOperations(product.Id, _environment.WebRootPath, src).Result;
            if (imageOpsIsOkay)
            {
                _ = new ProductLogic().Edit(product).Result;
            }
            else
            {
                ModelState.AddModelError("ImageForEdit", "Səkil tələb olunandır");
                return View(product);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (await new ProductLogic().DeleteProduct(id))
            {
                return Json(new { status = "200", data = "/Admin/Product/Index" });
            }
            else
            {
                return Json(new { status = "400" });
            }
        }
    }
}
