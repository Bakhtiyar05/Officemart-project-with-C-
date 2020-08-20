using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class OrdersLogic
    {
        public async Task<List<OrderNumberDto>> GetNotApprovedOrders()
        {
            var orders = new List<OrderNumberDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.OrderNumbers
                    .Include(i => i.Orders)
                    .Where(m => m.IsApproved == false).ToListAsync();
                orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
            }
            return orders;
        }

        public async Task<List<OrderNumberDto>> GetApprovedOrders()
        {
            var orders = new List<OrderNumberDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.OrderNumbers
                    .Include(i => i.Orders)
                    .Where(m => m.IsApproved == true).ToListAsync();
                orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
            }
            return orders;
        }

        public async Task<List<OrderNumberDto>> GetNotApprovedOrdersByCheckNumber(string checkNumber)
        {
            var orders = new List<OrderNumberDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.OrderNumbers
                    .Include(i => i.Orders)
                    .Where(m => m.OrderCheckNumber == checkNumber).ToListAsync();
                orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
            }
            return orders;
        }
        public async Task<List<OrderDto>> GetOrderDetails(int id)
        {
            var orderDetail = new List<OrderDto>();
            using (var context = TransactionConfig.AppDbContext)
            {
                var dbOrders = await context.Orders
                    .Include(i => i.Product)
                    .Include(x=>x.OrderNumber)
                    .Where(m => m.OrderNumberId == id).ToListAsync();
                orderDetail = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);

                orderDetail.ForEach(x =>
                {
                    x.IsOverflow = x.Product.Count < x.OrderCount ? true : false;
                    x.OrderNumberId = id;
                    x.IsApproved = dbOrders.Where(x => x.OrderNumber.Id == id).Select(x => x.OrderNumber.IsApproved).FirstOrDefault();
                });
            }
            return orderDetail;
        }

        public async Task<bool> ApproveOrder(int orderNumberId)
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var orderNumber = await context
                    .OrderNumbers
                    .FindAsync(orderNumberId);
                orderNumber.IsApproved = true;

                context.Update(orderNumber);

                var orderNumberProducts = await context
                    .OrderNumbers
                    .Where(x => x.Id == orderNumberId)
                    .Include(x => x.Orders)
                    .ThenInclude(x=>x.Product)
                    .ToListAsync();

                var orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(orderNumberProducts);

                foreach (var item in orders)
                {
                    foreach (var order in item.Orders)
                    {
                        order.Product.Count = order.OrderCount >= order.Product.Count ? 0 : order.Product.Count - order.OrderCount;
                        var productEntity = orderNumberProducts
                            .Select(x => x.Orders.Select(x => x.Product).Where(x => x.Id == order.Product.Id).FirstOrDefault())
                            .FirstOrDefault();
                        productEntity.Count = order.Product.Count;

                        var product = TransactionConfig.Mapper.Map(order.Product, productEntity);

                        context.Update(product);
                    }
                }

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<List<string>> GetCheckOutNumbersForNotification()
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var checkoutNumbers = await context
                    .OrderNumbers
                    .Where(x => x.IsApproved == false)
                    .Select(x => x.OrderCheckNumber)
                    .ToListAsync();

                return checkoutNumbers;
            }
        }

    }
}
