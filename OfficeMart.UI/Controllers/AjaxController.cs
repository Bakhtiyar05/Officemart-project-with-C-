using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Controllers
{
    public class AjaxController : ControllerBase
    {

        private readonly OfficeMartContext _context;
        public AjaxController(OfficeMartContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts([FromBody] Dictionary<string, int> requestBody, [FromQuery] string categoryGUID, [FromQuery] string search)
        {
            int skip = requestBody.ContainsKey("skip") ? requestBody["skip"] : 0;
            int productsPerPage = requestBody.ContainsKey("productsPerPage") ? requestBody["productsPerPage"] : 0;

            var productsQuery = _context.Products
                                        .Where(p => p.Status);

            if (!string.IsNullOrEmpty(categoryGUID))
                productsQuery = productsQuery.Where(p => p.CategoryGUID == categoryGUID);
            if (!string.IsNullOrEmpty(search))
                productsQuery = productsQuery.Where(p => p.Name.Contains(search));

            var products = await productsQuery
                                     .Skip(skip)
                                     .Take(productsPerPage)
                                     .ToListAsync();

            var response = new { products };

            return Ok(response);
        }


    }
}
