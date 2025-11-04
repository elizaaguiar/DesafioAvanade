using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Enums;
using Ms_Payment.AppDbContext;
using Ms_Payment.Entity;
using Ms_Payment.Interfaces;

namespace Ms_Payment.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbContexto _context;
        public PaymentRepository(DbContexto context)
        {
            _context = context;
        }
        public async Task Create(OrderTransaction orderTransaction)
        {
            _context.Add(orderTransaction);
            await _context.SaveChangesAsync();
        }
        public async Task Update(OrderTransaction orderTransaction, Status newStatus)
        {
            orderTransaction.Status = newStatus;
            _context.OrderTransaction.Update(orderTransaction);
            await _context.SaveChangesAsync();
        }

    }
}