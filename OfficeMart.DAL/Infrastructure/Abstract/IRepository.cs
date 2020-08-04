using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.DAL.Infrastructure.Abstract
{

    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Fetch();
        Task Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}
