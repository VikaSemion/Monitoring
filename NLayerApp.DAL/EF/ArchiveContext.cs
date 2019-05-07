using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF
{
    public class ArchiveContext : DbContext
    {
        public DbSet<Report> Archives { get; set; }

        public ArchiveContext(string connectionString)
            : base(connectionString)
        {
        }
    }

}
