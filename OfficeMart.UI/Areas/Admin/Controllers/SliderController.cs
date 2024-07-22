using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using static OfficeMart.UI.IFormFileExtensions;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly OfficeMartContext _context;

        public SliderController(IWebHostEnvironment env, OfficeMartContext context)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
                return View(slider);

            if (!IsValidImage(slider.Image, out string validationError))
            {
                ModelState.AddModelError("Image", validationError);
                return View(slider);
            }

            try
            {
                var imageUrl = await SaveImageAsync(slider.Image);
                slider.ImageUrl = imageUrl;

                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("Image", "Şəkli saxlama zamanı xəta baş verdi.");
                return View(slider);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slider slider)
        {
            if (!ModelState.IsValid)
                return View(slider);

            var sliderFromDb = await _context.Sliders.FindAsync(slider.Id);
            if (sliderFromDb == null)
                return NotFound();

            sliderFromDb.Status = slider.Status;
            sliderFromDb.Title = slider.Title;
            sliderFromDb.UrlPath = slider.UrlPath;

            if (slider.Image != null)
            {
                if (!IsValidImage(slider.Image, out string validationError))
                {
                    ModelState.AddModelError("Image", validationError);
                    return View(slider);
                }

                try
                {
                    await ReplaceImageAsync(sliderFromDb, slider.Image);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Image", "Şəkli saxlama zamanı xəta baş verdi.");
                    return View(slider);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsValidImage(IFormFile image, out string errorMessage)
        {
            errorMessage = null;
            if (image == null)
            {
                errorMessage = "Sahə tələb olunandır.";
                return false;
            }

            if (!image.ContentType.Contains("image/"))
            {
                errorMessage = "Şəklin formatı düzgün deyil. JPEG, PNG, və ya GIF olmalıdır.";
                return false;
            }

            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}/";
            return await image.SavePhotoAsync(_env.WebRootPath, "Slider", baseUrl);
        }

        private async Task ReplaceImageAsync(Slider slider, IFormFile newImage)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}/";

            if (!string.IsNullOrWhiteSpace(slider.ImageUrl))
            {
                string relativePath = slider.ImageUrl.Replace(baseUrl, "");
                RemovePhoto(_env.WebRootPath, relativePath);
            }

            string newImageUrl = await SaveImageAsync(newImage);
            slider.ImageUrl = newImageUrl;
        }
    }
}
