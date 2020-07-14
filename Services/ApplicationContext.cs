using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wiki_server.Models;

namespace wiki_server.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<WikiItem> Items { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Filename = wiki.db");
        }
    }
}
