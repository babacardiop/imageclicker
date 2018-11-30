using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageClicker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageClicker.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
