using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ms_Products.AppDbContext;
using Ms_Products.Entities;
using Ms_Products.Interfaces;

namespace Ms_Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContexto _context;
        public ProductRepository(DbContexto context)
        {
            _context = context;
        }
        public void CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public async Task<Product> GetByGuid(Guid Guid)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.Guid == Guid);
        }
    }
}