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

        
    }
}
