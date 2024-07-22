using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.AppDbContext;
using static OfficeMart.UI.IFormFileExtensions;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AjaxController : Controller
    {
        private readonly OfficeMartContext _context;
        private readonly IWebHostEnvironment _env;
        public AjaxController(OfficeMartContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSlider([FromBody] Dictionary<string, int> requestBody)
        {
            int sliderId = requestBody.ContainsKey("sliderId") ? requestBody["sliderId"] : 0;
            if (sliderId == 0)
                return BadRequest();
            var slider = await _context.Sliders.FindAsync(sliderId);
            if (slider == null)
                return NotFound();

            string baseUrl = $"{Request.Scheme}://{Request.Host}/";
            string relativePath = slider.ImageUrl.Replace(baseUrl, "");

            if (string.IsNullOrWhiteSpace(relativePath))
                return BadRequest("No image to delete.");

            bool removed = RemovePhoto(_env.WebRootPath, relativePath);
            if (!removed)
                return StatusCode(500, "Could not delete the image.");

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
