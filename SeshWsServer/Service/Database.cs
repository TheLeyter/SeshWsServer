using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SeshWsServer.Service
{
    class Database : DbContext
    {
        public DbSet<User> users { get; set; }

        public Database(DbContextOptions<Database> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
