using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Products.DTOs
{
    public class VerifyStockDTO
    {
        public Guid ProductId {get; set;}
        public int Quantity { get; set; }
    }
}