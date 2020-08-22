using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OfficeMart.Business.Logic
{
    public class ExportLogic
    {
        public async Task<List<OrderDto>> GetDatasForExport(DateTime startDate, DateTime endDate)
        {
            var products = new List<OrderDto>();

            using (var context = TransactionConfig.AppDbContext)
            {
                var dbProducts =await context.OrderNumbers
                    .Include(m => m.Orders)
                    .ThenInclude(p=>p.Product)
                    .Where(m => m.IsApproved == true && m.RegDate >= startDate && m.RegDate <= endDate)
                    .SelectMany(m=>m.Orders)
                    .ToListAsync();

              products=TransactionConfig.Mapper.Map<List<OrderDto>>(dbProducts);
            }

            return products;

        }
    }
}
