using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            catch
            {
                return false;
            }
        }
        public async Task<List<ProductDto>> GetProducts(int page)
        {
            var productsDto = new List<ProductDto>();

            using (var context = TransactionConfig.AppDbContext)
            {
                int itemsPerPage = 3;
                var productsCount = await context.Products.Where(x=>x.IsActive != false).CountAsync();

                var products = await context
                    .Products
                    .Where(m => m.IsActive != false)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(3)
                    .Include(m => m.Category)
                    .Include(m=>m.Color)
                    .Include(m=>m.ProductSize)
                    .Include(m => m.ProductImages)
                    .ToListAsync();

                var categoryCount = await context.Categories.CountAsync();

                productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(products);

                productsDto.ForEach(x =>
                {
                    x.PaginationDto = new PaginationDto();
                    x.PaginationDto.CurrentPage = page;
                    x.PaginationDto.ItemsPerPage = 3;
                    x.PaginationDto.TotalItemsCount = productsCount;
                    x.PaginationDto.AspAction = "ProductsList";
                    x.PaginationDto.AspController = "Products";
                });
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
        public async Task<List<ProductDto>> GetCategoryProducts(int categoryId)
        {
            var productsDto = new List<ProductDto>();

            using(var context = TransactionConfig.AppDbContext)
            {
                var productsEntity = await context
                    .Products
                    .Where(m => m.IsActive != false && m.CategoryId == categoryId)
                    .Include(m => m.Category)
                    .Include(m => m.Color)
                    .Include(m => m.ProductSize)
                    .Include(m => m.ProductImages)
                    .ToListAsync();

                productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(productsEntity);
            }
            return productsDto;
        }
        public async Task<List<ProductDto>> GetProductsPerPage(int categoryId, int page)
        {
            int itemsPerPage = 3;
            var products = new List<ProductDto>();

            using(var context = TransactionConfig.AppDbContext)
            {
                var productsCount = await context.Products.Where(x=>x.CategoryId == categoryId && x.IsActive != false).CountAsync();

                var productsEntity = await context
                    .Products
                    .Where(m => m.IsActive != false && m.CategoryId == categoryId)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(3)
                    .Include(m => m.Category)
                    .Include(m => m.Color)
                    .Include(m => m.ProductSize)
                    .Include(m => m.ProductImages)
                    .ToListAsync();

                products = TransactionConfig.Mapper.Map<List<ProductDto>>(productsEntity);

                products.ForEach(x =>
                {
                    x.PaginationDto = new PaginationDto();
                    x.PaginationDto.CurrentPage = page;
                    x.PaginationDto.ItemsPerPage = 3;
                    x.PaginationDto.TotalItemsCount = productsCount;
                    x.PaginationDto.CategoryId = categoryId;
                    x.PaginationDto.AspAction = "CategoryProducts";
                    x.PaginationDto.AspController = "Products";
                });
            }
            return products;
        }
        public async Task<CategoryProductsDto> GetProductsForDetailPage(int productId)
        {
            var categoryProduct = new CategoryProductsDto();

            using(var context = TransactionConfig.AppDbContext)
            {
                var product = await context
                    .Products.Where(x => x.Id == productId)
                    .Include(m => m.Category)
                    .Include(m => m.Color)
                    .Include(m => m.ProductSize)
                    .Include(m => m.ProductImages)
                    .FirstOrDefaultAsync();

                categoryProduct.ProductDto = new ProductDto();
                categoryProduct.ProductDto = TransactionConfig.Mapper.Map<ProductDto>(product);

                var categoryProducts = await context
                    .Products
                    .Where(x => x.IsActive != false && x.CategoryId == product.CategoryId)
                    .Include(m => m.Category)
                    .Include(m => m.ProductImages)
                    .Take(4)
                    .OrderByDescending(x => x.RegDate)
                    .ToListAsync();

                categoryProduct.ProducstDto = new List<ProductDto>();
                var similerProducts = TransactionConfig.Mapper.Map<List<ProductDto>>(categoryProducts);
                categoryProduct.ProducstDto.AddRange(similerProducts);
            }

            return categoryProduct;
        }
    }
}
