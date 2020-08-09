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
                    categoryDto.IsSuccessfullAdded = true;
                else
                    categoryDto.IsSuccessfullAdded = false;
            }
            return View(categoryDto);
        }
    }
}
