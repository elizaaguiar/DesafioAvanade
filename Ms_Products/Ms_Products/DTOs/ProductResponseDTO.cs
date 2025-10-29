using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Products.DTOs
{
    public class ProductResponseDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set;  }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}