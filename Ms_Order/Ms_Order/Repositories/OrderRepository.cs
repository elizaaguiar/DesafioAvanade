using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ms_Order.AppDbContext;
using Ms_Order.DTOs;
using Ms_Order.Entities;
using Ms_Order.Interfaces;

namespace Ms_Order.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContexto _context;
        public OrderRepository(DbContexto context)
        {
            _context = context;
        }
        public async Task<Order> Create(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

    }
}