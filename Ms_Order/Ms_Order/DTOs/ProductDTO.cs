using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.DTOs
{
    public class ProductDTO
    {
        public double Price { get; set; }
        public Guid ProductId { get; set; }
    }
}