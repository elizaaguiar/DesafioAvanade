using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ms_Order.DTOs
{
    public class CreateOrderDTO
    {
        public List<CreateOrderProductDTO> Products { get; set; }
    }
}