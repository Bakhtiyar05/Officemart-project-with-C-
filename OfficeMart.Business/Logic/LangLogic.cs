using Microsoft.EntityFrameworkCore;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class LangLogic
    {
        public async Task<LogicResult> AddResource(string name,string value)
        {
            var logicResult = new LogicResult();

            using(var context = TransactionConfig.AppDbContext)
            {
                var baseResource = context.Resources.Where(x => x.Name == name).FirstOrDefault();

                if(baseResource == null)
                {
                    var resource = new Resource
                    {
                        Name = name,
                        Value = value
                    };

                    await context.Resources.AddAsync(resource);
                    await context.SaveChangesAsync();
                    logicResult.OperationIsSuccessfull = true;
                    return logicResult;
                }
                else
                {
                    baseResource.Name = name;
                    baseResource.Value = value;
                    context.Resources.Update(baseResource);
                    context.SaveChanges();
                    logicResult.OperationIsSuccessfull = true;
                    return logicResult;
                }
                
            }
        }

        public async Task<List<Resource>> GetAllResources()
        {
            using(var context = TransactionConfig.AppDbContext)
            {
                var resourcesFile = await context.Resources.ToListAsync();
                return resourcesFile;
            }
        }
    }
}
