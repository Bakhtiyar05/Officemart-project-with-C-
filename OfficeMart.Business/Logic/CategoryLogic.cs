﻿using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class CategoryLogic
    {
        public async Task<bool> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var categoryEntity = TransactionConfig.Mapper.Map<Category>(categoryDto);
                    await context.Categories.AddAsync(categoryEntity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = new List<CategoryDto>();
            using(var context = TransactionConfig.AppDbContext)
            {
                var categoriesEntity = await context.Categories.ToListAsync();
                categories = TransactionConfig.Mapper.Map<List<CategoryDto>>(categoriesEntity);
            }
            return categories;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var categoryEntity = await context.Categories.FindAsync(id);
                var category = TransactionConfig.Mapper.Map<CategoryDto>(categoryEntity);
                return category;
            }
        }

        public async Task<bool> EditCategory(CategoryDto categoryDto)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var baseCategory = await context.Categories.FindAsync(categoryDto.Id);
                baseCategory = TransactionConfig.Mapper.Map(categoryDto, baseCategory);
                context.Categories.Update(baseCategory);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
