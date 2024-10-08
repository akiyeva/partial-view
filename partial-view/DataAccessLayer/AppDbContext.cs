﻿using Microsoft.EntityFrameworkCore;
using partial_view.DataAccessLayer.Entities;

namespace partial_view.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Footer> Footers { get; set; }
    }
}
