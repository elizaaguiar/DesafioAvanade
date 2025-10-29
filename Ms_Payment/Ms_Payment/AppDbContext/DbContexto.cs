using Microsoft.EntityFrameworkCore;
using Ms_Payment.Entity;

namespace Ms_Payment.AppDbContext
{
    public class DbContexto : DbContext
    {

        public DbContexto(DbContextOptions<DbContexto> options)
        : base(options)
        { }
        public DbSet<OrderTransaction> OrderTransaction { get; set; }
    }
}
