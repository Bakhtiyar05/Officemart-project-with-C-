using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class AjaxLogic
    {
        public async Task<ProductDto> GetProductForQuickView(int id)
        {
            var productDto = new ProductDto();
            using (var context = TransactionConfig.AppDbContext)
            {
                var product = await context
                    .Products
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync();

                productDto = TransactionConfig.Mapper.Map<ProductDto>(product);
                string fullFilePath = Path.Combine((new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath.Split(new string[] { "/bin" }, StringSplitOptions.None)[0]
                          , "wwwroot","img").Replace("%20"," ");

                foreach (var photo in productDto.ProductImages)
                {
                    var photoBase = "data:image/jpeg;base64," + Convert.ToBase64String(File.ReadAllBytes(Path.Combine(fullFilePath, photo)));
                    productDto.ImagesBase64.Add(photoBase);
                }

                return productDto;
            }
        }
    }
}
