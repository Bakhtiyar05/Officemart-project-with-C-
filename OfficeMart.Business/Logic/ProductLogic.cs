using AutoMapper;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static OfficeMart.Extensions.IFormFileExtensions;

namespace OfficeMart.Business.Logic
{
    public class ProductLogic
    {
        public async Task Add(ProductDto productDto, string rootPath)
        {

            using(var context = TransactionConfig.AppDbContext)
            {
                var mappedProduct = TransactionConfig.Mapper.Map<Product>(productDto);
                await context.Products.AddAsync(mappedProduct);
                await context.SaveChangesAsync();

                foreach (var image in productDto.Images)
                {
                    if (image.IsImage())
                    {
                        var imagePath = await image.SaveImage(rootPath, "Products");
                        var productImage = new ProductImage
                        {
                            ImageName = imagePath,
                            ProductId = mappedProduct.Id
                        };
                        await context.ProductImages.AddAsync(productImage);
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
