using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class CheckoutLogic
    {
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
