using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class CheckoutLogic
    {
        public async Task<List<BasketModelDto>> GetProducts(List<int> ids, List<int> counts/*, int page*/)
        {
            var productsDto = new List<BasketModelDto>();

            using (var context = TransactionConfig.AppDbContext)
            {
                //int itemsPerPage = 3;
                var productsCount = await context.Products.Where(x => x.IsActive != false).CountAsync();
                for (int i = 0; i < ids.Count; i++)
                {
                    try
                    {
                        var product = await context
                       .Products
                       .Where(m => m.IsActive != false && m.Id == ids[i])
                       //.Skip((page - 1) * itemsPerPage)
                       //.Take(3)
                       .Include(m => m.Category)
                       .Include(m => m.Color)
                       .Include(m => m.ProductSize)
                       .Include(m => m.ProductImages)
                       .FirstOrDefaultAsync();
                       productsDto.Add(new BasketModelDto { Product = TransactionConfig.Mapper.Map<ProductDto>(product), ProductCount = counts[i] });

                    }
                    catch (Exception)
                    {}
                }

                var categoryCount = await context.Categories.CountAsync();

                //productsDto = TransactionConfig.Mapper.Map<List<ProductDto>>(products);

                //productsDto.ForEach(x =>
                //{
                //    x.PaginationDto = new PaginationDto();
                //    x.PaginationDto.CurrentPage = page;
                //    x.PaginationDto.ItemsPerPage = 3;
                //    x.PaginationDto.TotalItemsCount = productsCount;
                //    x.PaginationDto.AspAction = "ProductsList";
                //    x.PaginationDto.AspController = "Products";
                //});
            }
            return productsDto;
        }
        public List<int> ConvertInt(string baseStr)
        {
            var intIds = new List<int>();
            string[] baseIds = baseStr.Split(',');
            foreach (var item in baseIds)
            {
                intIds.Add(int.Parse(item));
            }
            return intIds;
        }
    }
}
