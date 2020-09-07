using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using stok_uygulaması.Models;
using stok_uygulaması.Repository;

namespace stok_uygulaması.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private DbContextTransaction transaction;
        //  private readonly EticaretContext _context;
        stoksqlEntities context = new stoksqlEntities();

        private EfRepository<tbl_Stok> _urunRepository;

        public EfRepository<tbl_Stok> UrunRepository
        {
            get
            {
                if (_urunRepository == null)
                {
                    _urunRepository = new EfRepository<tbl_Stok>(context);
                }

                return _urunRepository;
            }
        }

        private EfRepository<tbl_StokHareket> _markaRepository;

        public EfRepository<tbl_StokHareket> MarkaRepository
        {
            get
            {
                if (_markaRepository == null)
                {
                    _markaRepository = new EfRepository<tbl_StokHareket>(context);
                }

                return _markaRepository;
            }
        }


        public EfUnitOfWork()
        {
            Database.SetInitializer<stoksqlEntities>(null);
            if (context == null)
            {
                throw new ArgumentNullException("Context null olamaz");
            }

        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(context);
        }

        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch

            {
                throw;
            }
        }
    }
}