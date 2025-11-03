using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.Entities
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Guid OrderProuctsId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        
    }
}