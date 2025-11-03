using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ms_Order.Entities;

namespace Ms_Order.AppDbContext
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options)
        : base(options)
        { }
        public DbSet<Order> Order { get; set; }
        public DbSet<Order> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<OrderProducts>().ToTable("OrderProducts");
        }
    }
}