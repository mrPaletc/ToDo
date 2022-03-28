using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo
{
    public class AppDbContent : DbContext
    {
        public DbSet<Job> Task { get; set; }

        public AppDbContent(DbContextOptions<AppDbContent> options ) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
