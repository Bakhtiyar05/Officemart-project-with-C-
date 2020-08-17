using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OfficeMart.Business.Logic
{
    public class OrdersLogic
    {
        public async Task<List<OrderNumberDto>> GetNotApprovedOrders()
        {
            var order = new List<OrderNumberDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.OrderNumbers
                    .Include(i => i.Orders)
                    .Where(m => m.IsApproved == false).ToListAsync();
                order = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
            }
            return order;
        }
        public async Task<List<OrderDto>> GetOrderDetails(int id)
        {
            var orderDetail = new List<OrderDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.Orders
                    .Include(i => i.Product)
                    .Where(m => m.OrderNumberId == id).ToListAsync();
                orderDetail = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
            }
            return orderDetail;
        }
    }
}
