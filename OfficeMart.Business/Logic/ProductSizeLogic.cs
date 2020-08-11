using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class ProductSizeLogic
    {
        public async Task Add(string productSize)
        {
            using (var context = TransactionConfig.AppDbContext)
            {
                var sizes = await context.ProductSizes.AddAsync(new Domain.Models.Entities.ProductSize { Size = productSize });
                await context.SaveChangesAsync();
            }

        }
        public async Task<List<ProductSizeDto>> GetProductSizes()
        {
            var sizes = new List<ProductSizeDto>();
            using (var content = TransactionConfig.AppDbContext)
            {
                var dbSizes = await content.ProductSizes.Where(m=>m.IsActive!=false).ToListAsync();
                sizes = TransactionConfig.Mapper.Map<List<ProductSizeDto>>(dbSizes);
            }
            return sizes;
        }
        public async Task<ProductSizeDto> GetSizeById(int id)
        {
            ProductSizeDto productSizeDto = new ProductSizeDto();
            using (var content = TransactionConfig.AppDbContext)
            {
                var dbSize =await content.ProductSizes.FindAsync(id);
                productSizeDto = TransactionConfig.Mapper.Map<ProductSizeDto>(dbSize);
            }
            return productSizeDto;
        }
        public async Task<bool> Edit (ProductSizeDto sizeDto)
        {
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var findedEntity = await context.ProductSizes.FindAsync(sizeDto.Id);
                    findedEntity = TransactionConfig.Mapper.Map(sizeDto, findedEntity);
                    findedEntity.IsActive = true;
                    context.ProductSizes.Update(findedEntity);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var deletedProductSize = await context.ProductSizes.FindAsync(id);
                    deletedProductSize.IsActive = false;
                    context.Update(deletedProductSize);
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
