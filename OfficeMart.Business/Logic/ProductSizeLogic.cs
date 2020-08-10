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
                var dbSizes = await content.ProductSizes.ToListAsync();
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
    }
}
