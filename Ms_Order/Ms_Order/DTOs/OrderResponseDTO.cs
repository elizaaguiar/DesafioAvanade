using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.DTOs
{
    public class OrderResponseDTO

    {
        public Guid Guid { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductDTO> Items { get; set; }
    }
}