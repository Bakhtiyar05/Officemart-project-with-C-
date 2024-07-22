using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.UI.Models;
using OfficeMart.UI.Models.API;
using OfficeMart.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static OfficeMart.UI.API.NetworkManager;

namespace OfficeMart.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly OfficeMartContext _context;
        public ProductsController(IConfiguration configuration, OfficeMartContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [Route("/Məhsul/Məhsullarımız")]
        public async Task<IActionResult> ProductsList(string categoryGUID, string search)
        {
            CategoryData categoryData = await GetCategoriesAsync();

            var products = _context.Products.Where(p => p.Status).Take(12).ToList();
            var productsCount = _context.Products.Where(p => p.Status).ToList().Count;

            if (!string.IsNullOrEmpty(categoryGUID))
            {
                products = _context.Products.Where(p => p.CategoryGUID == categoryGUID && p.Status).Take(12).ToList();
                productsCount = _context.Products.Where(p => p.CategoryGUID == categoryGUID && p.Status).ToList().Count;
            }
            if (!string.IsNullOrEmpty(search))
            {
                products = _context.Products.Where(p => p.Name.Contains(search) && p.Status).Take(12).ToList();
                productsCount = _context.Products.Where(p => p.Name.Contains(search) && p.Status).ToList().Count;
            }

            if (!string.IsNullOrEmpty(categoryGUID) && !string.IsNullOrEmpty(search))
            {
                products = _context.Products.Where(p => p.Name.Contains(search) && p.CategoryGUID == categoryGUID && p.Status).Take(12).ToList();
                productsCount = _context.Products.Where(p => p.Name.Contains(search) && p.CategoryGUID == categoryGUID && p.Status).ToList().Count;
            }

            ViewBag.productsCount = productsCount;
            ViewBag.search = search;
            return View(new ProductsViewModel()
            {
                Products = products,
                Categories = categoryData.Categories.Where(c => c.Status).ToList(),
            });
        }

        private async Task<CategoryData> GetCategoriesAsync()
        {
            try
            {
                var response = await SendRequestAsync<CategoryData>("Category", _configuration, HttpMethod.Get);
                return response ?? new CategoryData();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching category data.", ex);
            }
        }

    }
}
