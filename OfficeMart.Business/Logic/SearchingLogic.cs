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
    public class SearchingLogic
    {
        public async Task<List<OrderDto>> GetOrders(DateTime begDate,DateTime endDate,string pattern)
        {
            DateTime invalidDate = new DateTime(1 / 1 / 0001).Date;

            var orders = new List<OrderDto>();

            if (begDate != invalidDate && endDate.Date == invalidDate && pattern == null)
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.OrderNumbers
                        .Include(i => i.Orders)
                        .Where(m => m.RegDate >= begDate)
                        .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            else if(begDate.Date == invalidDate && endDate != invalidDate && pattern == null)
            {

                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.OrderNumbers
                        .Include(i => i.Orders)
                        .Where(m => m.RegDate <= endDate)
                        .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            else if(begDate.Date == invalidDate && endDate.Date == invalidDate && pattern != null)
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.Orders
                        .Include(x => x.OrderNumber)
                        .Where(x => 
                        x.BuyerName.Contains(pattern)
                        || x.BuyerSurname.Contains(pattern)
                        || x.BuyerPhone.Contains(pattern)
                        || x.DeliveryAddress.Contains(pattern))
                        .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            else if(begDate != invalidDate && endDate != invalidDate && pattern == null)
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.OrderNumbers
                        .Include(i => i.Orders)
                        .Where(m => m.RegDate >= begDate && m.RegDate <= endDate)
                        .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            else if (begDate != invalidDate && endDate.Date == invalidDate && pattern != null)
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.Orders
                       .Include(x => x.OrderNumber)
                       .Where(x =>x.RegDate >= begDate
                       && x.BuyerName.Contains(pattern)
                       || x.BuyerSurname.Contains(pattern)
                       || x.BuyerPhone.Contains(pattern)
                       || x.DeliveryAddress.Contains(pattern))
                       .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            else if (begDate.Date == invalidDate && endDate != invalidDate && pattern != null)
            {
                using (var context = TransactionConfig.AppDbContext)
                {
                    var dbOrders = await context.Orders
                       .Include(x => x.OrderNumber)
                       .Where(x => x.RegDate <= endDate
                       && x.BuyerName.Contains(pattern)
                       || x.BuyerSurname.Contains(pattern)
                       || x.BuyerPhone.Contains(pattern)
                       || x.DeliveryAddress.Contains(pattern))
                       .ToListAsync();

                    orders = TransactionConfig.Mapper.Map<List<OrderDto>>(dbOrders);
                }
                return orders;
            }
            return null;
        }
    }
}
