using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Interfaces;
using System.Data.Entity;

namespace NLayerApp.DAL.Repositories
{
    public class ReportRepository : IRepository<Report>
    {
        public Context db;

        public ReportRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Report> GetAll()
        {
            return db.Reports;
        }

        public Report Get(int id)
        {
            return db.Reports.Find(id);
        }

        public void Create(Report r)
        {
            db.Reports.Add(r);
        }

        public void Update(Report r)
        {
            db.Entry(r).State = EntityState.Modified;
        }

        public IEnumerable<Report> Find(Func<Report, Boolean> predicate)
        {
            return db.Reports.Where(predicate).ToList();
        }

        public void Delete(Report r)
        {
            //Report r = db.Reports.Find(id);
            //if (r != null)
                db.Reports.Remove(r);
        }
    }
}