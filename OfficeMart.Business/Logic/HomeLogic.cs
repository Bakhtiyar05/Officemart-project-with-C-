using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    .Where(x=>x.IsActive != false)
                    .Include(x => x.ProductImages)
                    .Include(x=>x.Category)
                    .ToListAsync();

                productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(products);
            }

            return productsDto;
        } 

        public async Task<ProductDto> GetProductForQuickView(int id)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var product = await context
                    .Products
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .ToListAsync();

                var result = TransactionConfig.Mapper.Map<ProductDto>(product);

                return result;
            }
        }
    }
}
