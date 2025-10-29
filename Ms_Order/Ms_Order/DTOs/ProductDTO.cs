using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set;  }
        public decimal Amount { get; set; }
        public int Stock { get; set; }
        public Guid ProductId { get; set; }
    }
}