using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Payment.Entity
{
    public class OrderTransaction
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public double TotalAmount { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime RemovedAt { get; set; }
        public string Status { get; set; }
    }
}