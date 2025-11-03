using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime RemovedAt { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }
    }
}