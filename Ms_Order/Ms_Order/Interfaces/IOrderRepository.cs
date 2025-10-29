using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ms_Order.DTOs;
using Ms_Order.Entities;

namespace Ms_Order.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
    }
}