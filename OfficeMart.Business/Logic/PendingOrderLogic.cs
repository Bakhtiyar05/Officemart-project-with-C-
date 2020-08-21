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
    public class PendingOrderLogic
    {
        public async Task<List<OrderNumberDto>> GetUserPendingOrders(string userId, bool IsApprover)
        {
            var productsDto = new List<OrderNumberDto>();

            using (var context = TransactionConfig.AppDbContext)
            {
                var orders = await context.OrderNumbers
                    .Include(m => m.Orders)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(i=>i.ProductImages)
                    .ThenInclude(c=>c.Product.Category)
                    .Where(u => u.BuyerUserId == userId && u.IsApproved == IsApprover).ToListAsync();
                productsDto = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(orders);
            }

            return productsDto;
        }
    }
}
