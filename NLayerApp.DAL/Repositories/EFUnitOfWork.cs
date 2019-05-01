using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public Context db;
        public ReportRepository ReportRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new Context(connectionString);
        }
        public IRepository<Report> Reports
        {
            get
            {
                if (ReportRepository == null)
                    ReportRepository = new ReportRepository(db);
                return ReportRepository;
            }
        }

        

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}