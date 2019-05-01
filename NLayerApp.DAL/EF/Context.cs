using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF
{
    public class Context : DbContext
    {
        public DbSet<Report> Reports { get; set; }

        static Context()
        {
            Database.SetInitializer<Context>(new StoreDbInitializer());
        }
        public Context(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context db)
        {
            db.Reports.Add(new Report { Id = 1, Date = "01.02.2019", City = "Kyiv", Worker = "JohnUolker@gmail.com",
                O3 = 220, NO2 = 160 , SO2 = 16 });
            db.Reports.Add(new Report { Id = 2, Date = "20.02.1029", City = "Kherson", Worker = "OlegButin@gmail.com",
                O3 = 200, NO2 = 100 , SO2 = 89 });
            db.Reports.Add(new Report { Id = 3, Date = "16.04.2019", City = "Poltava", Worker = "AnnaSavina@gmail.com",
                O3 = 250, NO2 = 89 , SO2 = 65 });
            db.Reports.Add(new Report { Id = 4, Date = "18.03.2019", City = "Lviv", Worker = "OrnestLutyi@gmail.com",
                O3 = 180, NO2 = 96 , SO2 = 91 });
            db.Reports.Add(new Report { Id = 5, Date = "19.01.2019", City = "Odessa", Worker = "TomHalper@gmail.com",
                O3 = 163, NO2 = 120 , SO2 = 101 });
            db.SaveChanges();
        }
    }
}
