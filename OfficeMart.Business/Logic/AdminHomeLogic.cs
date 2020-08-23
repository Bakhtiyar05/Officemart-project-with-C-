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
    public class AdminHomeLogic
    {
        public async Task<AdminHomeDto> GetAdminStatistic()
        {
            var statisticDto = new AdminHomeDto();
            using(var context = TransactionConfig.AppDbContext)
            {
                statisticDto.TotalOrdersCount = await context.OrderNumbers.CountAsync();
                statisticDto.TotalSalesPrice =  context.Orders.Select(x => x.TotalPrice).Sum();
                statisticDto.UsersCount = await context.Users.CountAsync();
                return statisticDto;
            }
        }
    }
}
