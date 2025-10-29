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
        public DbSet<Order> Orders { get; set; }
    }
}