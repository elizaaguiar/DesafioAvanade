using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ms_Products.Entities;

namespace Ms_Products.AppDbContext
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options): base(options)
        { }
        public DbSet<Product> Product { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("server=localhost;port=3309;database=ms_products;user=ms_products;password=root;",
                new MySqlServerVersion(new Version(8, 0, 30)));
    }
}