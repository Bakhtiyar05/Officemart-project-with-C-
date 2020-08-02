using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
            var productsDto =await new ProductLogic().GetProducts();
            return View(productsDto);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProductDto productDto,List<string> src)
        {

            if (!ModelState.IsValid && src.Count == 0)
            {
                return View(productDto);
            }

            await new ProductLogic().Add(productDto, _environment.WebRootPath,src);
            ViewBag.IsSuccessAdded = true;
            return View();
        }


        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
