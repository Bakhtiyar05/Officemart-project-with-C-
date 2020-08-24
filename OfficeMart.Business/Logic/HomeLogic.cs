﻿using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = new List<CategoryDto>();

            using(var context = TransactionConfig.AppDbContext)
            {
                var categoriesEntity = await context
                    .Categories
                    .Where(x => x.IsActive != false)
                    .ToListAsync();

                var specialProductEntity = await context
                    .Products
                    .Include(x=>x.ProductImages)
                    .Where(x => x.IsSpecial == true)
                    .FirstOrDefaultAsync();

                categories = TransactionConfig.Mapper.Map<List<CategoryDto>>(categoriesEntity);
                var specialProduct = TransactionConfig.Mapper.Map<ProductDto>(specialProductEntity);

                categories.ForEach(x =>
                {
                    x.ProductDto = new ProductDto();
                    x.ProductDto = specialProduct;
                });
            }

            return categories;
        }
    }
}
