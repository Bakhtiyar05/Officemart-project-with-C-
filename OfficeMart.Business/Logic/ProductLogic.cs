using AutoMapper;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static OfficeMart.Extensions.IFormFileExtensions;

namespace OfficeMart.Business.Logic
{
    public class ProductLogic
    {
        public async Task Add(ProductDto productDto, string rootPath, List<string> src)
        {
            int productId = 0;
            using (var context = TransactionConfig.AppDbContext)
            {
                var mappedProduct = TransactionConfig.Mapper.Map<Product>(productDto);
                await context.Products.AddAsync(mappedProduct);
                await context.SaveChangesAsync();
                productId = mappedProduct.Id;
            }

            using(var context = TransactionConfig.AppDbContext)
            {
                foreach (var image in src)
                {
                    if (image.ByteArrayIsImage())
                    {
                        byte[] srcByte = Convert.FromBase64String(image.Substring(image.IndexOf(";base64,") + 8));
                        var imagePath =  srcByte.ByteArraySaveImage(rootPath, "Products").Result;
                        var productImage = new ProductImage
                        {
                            ImageName = imagePath,
                            ProductId = productId
                        };
                        await context.ProductImages.AddAsync(productImage);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var productsDto = new List<ProductDto>();

            using (var context =  TransactionConfig.AppDbContext)
            {
                var products = await context
                    .Products
                    .Include(m => m.Category)
                    .Include(m => m.ProductImages)
                    .ToListAsync();
                productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(products);
            }
            return productsDto;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            ProductDto productDto = new ProductDto();

            using (var context = TransactionConfig.AppDbContext)
            {
                var product = await context
                    .Products
                    .Include(m => m.Category)
                    .Include(m => m.ProductImages)
                    .FirstOrDefaultAsync();
                productDto = TransactionConfig.Mapper.Map<ProductDto>(product);
            }
            return productDto;
        }
    }
}
