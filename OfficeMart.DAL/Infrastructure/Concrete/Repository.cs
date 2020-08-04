using Microsoft.EntityFrameworkCore;
using OfficeMart.DAL.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.DAL.Infrastructure.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public DbSet<TEntity> Fetch()
        {
            return _dbSet;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
