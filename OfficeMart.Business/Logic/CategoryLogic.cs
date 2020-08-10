using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.Extensions;
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
        public async Task<bool> AddCategory(CategoryDto categoryDto,string root)
        {
            try
            {
                if (categoryDto.Image.IsImage())
                {
                    var imageName = await categoryDto.Image.SaveImage(root, "Category");
                    categoryDto.ImageName = imageName;
                }

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
                var categoriesEntity = await context
                    .Categories
                    .Where(x=>x.IsActive != false)
                    .ToListAsync();
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

        public async Task<bool> EditCategory(CategoryDto categoryDto,string root)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var baseCategory = await context.Categories.FindAsync(categoryDto.Id);

                if (categoryDto.ImageForEdit != null)
                {
                    if (categoryDto.ImageForEdit.IsImage())
                    {
                        IFormFileExtensions.RemoveImage(root, baseCategory.ImageName);
                        var imageName = await categoryDto.ImageForEdit.SaveImage(root, "Category");
                        categoryDto.ImageName = imageName;
                    }
                }
                else
                    categoryDto.ImageName = baseCategory.ImageName;

                baseCategory = TransactionConfig.Mapper.Map(categoryDto, baseCategory);
                baseCategory.IsActive = true;
                context.Categories.Update(baseCategory);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> RemoveCategory(int id,string root)
        {
            var categoryDto = new CategoryDto();
            try
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var baseCategory = await context.Categories.FindAsync(id);
                    IFormFileExtensions.RemoveImage(root, baseCategory.ImageName);
                    baseCategory.IsActive = false;
                    context.Update(baseCategory);
                    await context.SaveChangesAsync();
                    categoryDto.IsSuccessfull = true;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
