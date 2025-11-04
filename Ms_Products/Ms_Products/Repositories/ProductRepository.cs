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
        public Product GetByGuid(Guid productId)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                throw new Exception("Produto indisponível");
            }
            return product;
        }
        public async Task<Product> StockUpdate(Guid productId, int quantity)
        {
            var product = GetByGuid(productId);
            if (product.Stock < quantity)
            {
                throw new Exception("Estoque indisponível");
            } 
            else
            {
                product.Stock -= quantity;
            }
            _context.Product.Update(product);
            _context.SaveChanges();

            return product;
        }
    }
}
