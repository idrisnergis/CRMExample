using CRMExample.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.DataAccess.Context
{
   public  class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client>  Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Lead> Leads { get; set; }

    }
}
