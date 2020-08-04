using OfficeMart.DAL.Infrastructure.Abstract;
using OfficeMart.DAL.Infrastructure.Concrete;
using OfficeMart.Domain.Models.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.DAL.Infrastructure.Design
{
    public class UnitOfWork : IUnitOfWork
    {
        private OfficeMartContext _officeMartContext = null;
        public UnitOfWork()
        {
            _officeMartContext = new OfficeMartContext();
        }

        public async Task Commit()
        {
            await _officeMartContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _officeMartContext.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_officeMartContext);
        }
    }
}
