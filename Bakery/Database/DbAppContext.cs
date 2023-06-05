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
        public DbSet<Supply> Supply { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Raw> Raw { get; set; }
        public DbSet<Supplied_Raw> Supplied_Raw { get; set; }


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

            modelBuilder.Entity<Supply>().HasOne(p => p.ProviderEntity)
                                        .WithMany(p => p.SupplyEntities);
            modelBuilder.Entity<Supplied_Raw>().HasKey(op => new { op.SupplyId, op.RawId });
            modelBuilder.Entity<Supplied_Raw>().HasOne(p => p.SupplyEntity)
                                                  .WithMany(p => p.SuppliedRawEntities);
            modelBuilder.Entity<Supplied_Raw>().HasOne(p => p.RawEntity)
                                                  .WithMany(p => p.SuppliedRawEntities);
        }
    }
}
