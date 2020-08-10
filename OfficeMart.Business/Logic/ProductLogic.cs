using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            using (var context = TransactionConfig.AppDbContext)
            {
                foreach (var image in src)
                {
                    if (image.ByteArrayIsImage())
                    {
                        byte[] srcByte = Convert.FromBase64String(image.Substring(image.IndexOf(";base64,") + 8));
                        var imagePath = srcByte.ByteArraySaveImage(rootPath, "Products").Result;
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
        public async Task<bool> Edit(ProductDto editableProduct)
        {
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var baseProduct = context.Products.Find(editableProduct.Id);
                    baseProduct = TransactionConfig.Mapper.Map(editableProduct, baseProduct);
                    context.Products.Update(baseProduct);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<ProductDto>> GetProducts()
        {
            var productsDto = new List<ProductDto>();

            using (var context = TransactionConfig.AppDbContext)
            {
                //var a = await context.ProductSizes.ToListAsync();
                var products = await context
                    .Products
                    .Where(m => m.IsActive != false)
                    .Include(m => m.Category)
                    .Include(m=>m.Color)
                    .Include(m=>m.ProductSize)
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
                    .Products.Where(x => x.Id == id)
                    .Include(m => m.Category)
                    .Include(m => m.ProductImages)
                    .FirstOrDefaultAsync();
                productDto = TransactionConfig.Mapper.Map<ProductDto>(product);
            }
            return productDto;
        }
        public async Task<List<string>> GetProductImagesById(int id)
        {
            var productImages = new List<string>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var proImg = await context.ProductImages.Where(m => m.ProductId == id).Select(m => m.ImageName.Replace(@"\", "").Replace("/", "")).ToListAsync();
                productImages = proImg;
            }
            return productImages;
        }
        public async Task<bool> ProductEditOperations(int id, string rootPath, List<string> src)
        {
            bool value = false;
            if (src.Count == 0)
            {
                return value;
            }
            var productImages = GetProductImagesById(id).Result;
            var newExistedImages = new List<string>();
            using (var context = TransactionConfig.AppDbContext)
            {
                foreach (var image in src)
                {
                    if (image.Contains("/img/"))
                    {
                        string imageName = image.Substring(image.IndexOf("/img/") + 4).Replace(@"\", "").Replace("/", "");
                        newExistedImages.Add(imageName);
                    }
                    else
                    {
                        if (image.ByteArrayIsImage())
                        {
                            byte[] srcByte = Convert.FromBase64String(image.Substring(image.IndexOf(";base64,") + 8));
                            var imagePath = srcByte.ByteArraySaveImage(rootPath, "Products").Result;
                            var productImage = new ProductImage
                            {
                                ImageName = imagePath,
                                ProductId = id
                            };
                            await context.ProductImages.AddAsync(productImage);
                        }
                    }
                }
                for (int i = 0; i < productImages.Count; i++)
                {
                    string currentImage = productImages[i];
                    bool isExist = newExistedImages.Any(m => m == currentImage);
                    if (!isExist)
                    {
                        var deletedImage = context.ProductImages.Where(m => m.ImageName.Replace(@"\", "").Replace("/", "") == currentImage).FirstOrDefault();
                        context.ProductImages.Remove(deletedImage);
                    }
                }
                await context.SaveChangesAsync();
                value = true;
            }

            return value;

        }
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var deletedProduct = await context.Products.FindAsync(id);
                    deletedProduct.IsActive = false;
                    context.Update(deletedProduct);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
