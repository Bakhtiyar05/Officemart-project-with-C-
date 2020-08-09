using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
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
    }
}
