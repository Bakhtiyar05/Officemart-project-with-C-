using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class HomeLogic
    {
        public async Task<List<ProductDto>> GetProducts()
        {
            var productsDto = new List<ProductDto>();

            using(var context = TransactionConfig.AppDbContext)
            {
                var products = await context
                    .Products
                    .Include(x => x.ProductImages)
                    .Include(x=>x.Category)
                    .ToListAsync();

                productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(products);
            }

            return productsDto;
        } 
    }
}
