using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {
        public DutchContext(DbContextOptions<DutchContext> options) 
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasData(new Order()
                {
                    Id = 1,
                    OrderDate = DateTime.Now,
                    OrderNumber = "1234"
                });
        }
    }
}
