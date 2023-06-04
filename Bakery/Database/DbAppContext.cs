using bakery.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Database
{
    public class DbAppContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Ordered_Product> Ordered_Product { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Username=postgres;Password=root;Database=Bakery_Database");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(p => p.RoleEntity)
                                       .WithMany(p => p.UserEntities);
            modelBuilder.Entity<Order>().HasOne(p => p.CustomerEntity)
                                        .WithMany(p => p.OrderEntities);
            modelBuilder.Entity<Ordered_Product>().HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<Ordered_Product>().HasOne(p => p.OrderEntity)
                                                  .WithMany(p => p.OrderedProductEntities);
            modelBuilder.Entity<Ordered_Product>().HasOne(p => p.ProductEntity)
                                                  .WithMany(p => p.OrderedProductEntities);

        }
    }
}
