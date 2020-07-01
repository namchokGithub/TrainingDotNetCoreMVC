using System;
using Microsoft.EntityFrameworkCore;

namespace Movie.Models
{
    public class MyProjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blog.db");
        }

        public DbSet<MovieModel> Movie { get; set; }
    }
}