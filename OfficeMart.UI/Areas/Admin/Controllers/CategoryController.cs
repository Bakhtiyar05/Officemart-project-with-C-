using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
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
                bool isSuccessfull = await new CategoryLogic().AddCategory(categoryDto);
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
    }
}
