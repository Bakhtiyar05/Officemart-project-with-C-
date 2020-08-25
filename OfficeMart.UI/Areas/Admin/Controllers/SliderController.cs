using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public SliderController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SliderDto sliderDto)
        {
            if (ModelState.IsValid)
            {
                var result = await new SliderLogic().AddImage(_environment.WebRootPath, sliderDto);
                return View(result);
            }
            return View(sliderDto);
        }

        public async Task<IActionResult> ImagesList()
        {
            var images = await new SliderLogic().GetAllImages();
            return View(images);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await new SliderLogic().GetImageById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SliderDto sliderDto)
        {
            var result = await new SliderLogic().EditImage(_environment.WebRootPath, sliderDto);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await new SliderLogic().RemoveImage(id);
            if (result)
                return RedirectToAction("ImagesList");
            else
                throw new Exception("Image removing exception");
        }
    }
}
