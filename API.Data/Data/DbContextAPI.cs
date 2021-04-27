using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Data
{
    public class DbContextAPI : DbContext
    {
        public DbContextAPI(DbContextOptions<DbContextAPI> options) : base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Literature> Literature { get; set; }
        public DbSet<Subject> Subject { get; set; }

    }
}
