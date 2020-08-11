using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Business.Models;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSizeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var sizes = await new ProductSizeLogic().GetProductSizes();
            return View(sizes);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(string ProductSize)
        {
            await new ProductSizeLogic().Add(ProductSize);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dbSize = await new ProductSizeLogic().GetSizeById(id);
            return View(dbSize);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductSizeDto sizeDto)
        {
            _ = await new ProductSizeLogic().Edit(sizeDto);

            return View(sizeDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await new ProductSizeLogic().Delete(id))
            {
                return Json(new { status = "200", data = "/Admin/ProductSize/Index" });
            }
            else
            {
                return Json(new { status = "400" });
            }
        }
    }
}
