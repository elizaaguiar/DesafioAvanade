using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.DTOs
{
    public class CreateOrderProductDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}