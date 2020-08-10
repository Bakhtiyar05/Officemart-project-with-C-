using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public CategoryController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                bool isSuccessfull = await new CategoryLogic().AddCategory(categoryDto,_environment.WebRootPath);
                if (isSuccessfull)
                    categoryDto.IsSuccessfull = true;
                else
                    categoryDto.IsSuccessfull = false;
            }
            return View(categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var categories = await new CategoryLogic().GetCategories();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await new CategoryLogic().GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var result = await new CategoryLogic().EditCategory(categoryDto);
                if (result)
                    categoryDto.IsSuccessfull = true;
                else
                    categoryDto.IsSuccessfull = false;
            }
            return View(categoryDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await new CategoryLogic().RemoveCategory(id))
            {
                return Json(new { status = "200", data = "/Admin/Category/CategoryList" });
            }
            else
            {
                return Json(new { status = "400" });
            }

        }
    }
}
