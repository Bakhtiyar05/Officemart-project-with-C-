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
        public async Task<List<OrderNumberDto>> GetOrders(DateTime begDate,DateTime endDate,string routeValue)
        {
            DateTime invalidDate = new DateTime(1 / 1 / 0001).Date;

            var orders = new List<OrderNumberDto>();

            if(routeValue == null)
            {
                if (begDate != invalidDate && endDate.Date == invalidDate)
                {
                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(x=>x.IsApproved == false)
                            .Where(m => m.RegDate >= begDate)
                            .ToListAsync();

                        orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                    }
                    return orders;
                }
                else if (begDate.Date == invalidDate && endDate != invalidDate)
                {

                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(x => x.IsApproved == false)
                            .Where(m => m.RegDate <= endDate)
                            .ToListAsync();

                        orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                    }
                    return orders;
                }
                else if (begDate != invalidDate && endDate != invalidDate)
                {
                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(x => x.IsApproved == false)
                            .Where(m => m.RegDate >= begDate && m.RegDate <= endDate)
                            .ToListAsync();

                        orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                    }
                    return orders;
                }
            }
            else
            {
                if (begDate != invalidDate && endDate.Date == invalidDate)
                {
                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(i => i.IsApproved == true)
                            .Where(m => m.RegDate >= begDate)
                            .ToListAsync();

                        try
                        {
                            orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                        }
                        catch(Exception) { }
                    }
                    return orders;
                }
                else if (begDate.Date == invalidDate && endDate != invalidDate)
                {

                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(x => x.IsApproved == true)
                            .Where(m => m.RegDate <= endDate)
                            .ToListAsync();

                        orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                    }
                    return orders;
                }
                else if (begDate != invalidDate && endDate != invalidDate)
                {
                    using (var context = TransactionConfig.AppDbContext)
                    {
                        var dbOrders = await context.OrderNumbers
                            .Include(i => i.Orders)
                            .Where(x => x.IsApproved == true)
                            .Where(m => m.RegDate >= begDate && m.RegDate <= endDate)
                            .ToListAsync();

                        orders = TransactionConfig.Mapper.Map<List<OrderNumberDto>>(dbOrders);
                    }
                    return orders;
                }
            }

            return orders;
        }
    }
}
