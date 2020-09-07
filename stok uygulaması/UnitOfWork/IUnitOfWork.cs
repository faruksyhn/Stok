using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using stok_uygulaması.Repository;

namespace stok_uygulaması.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}