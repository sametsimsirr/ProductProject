using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductProject.Entities;
using Microsoft.Extensions.Configuration;

namespace ProductProject.Repositories
{
    public class BaseDbContext : DbContext
    {
       
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
          : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<Entities.Attribute> Attributes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().ToTable("Product").HasQueryFilter(u=>!u.Deleted.HasValue || u.Deleted == 0);
            builder.Entity<ProductCategory>().ToTable("Product_Category").HasQueryFilter(u => !u.Deleted.HasValue || u.Deleted == 0);
            builder.Entity<ProductAttribute>().ToTable("Product_Attribute").HasQueryFilter(u => !u.Deleted.HasValue || u.Deleted == 0);
            builder.Entity<CategoryAttribute>().ToTable("Category_Attribute").HasQueryFilter(u => !u.Deleted.HasValue || u.Deleted == 0);
            builder.Entity<Entities.Attribute>().ToTable("Attribute").HasQueryFilter(u => !u.Deleted.HasValue || u.Deleted == 0);
        }
    }
}
