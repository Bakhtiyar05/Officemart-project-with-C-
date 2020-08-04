using OfficeMart.DAL.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.DAL.Infrastructure.Design
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task Commit();
    }
}
