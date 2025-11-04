using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Enums;
using Ms_Payment.Entity;

namespace Ms_Payment.Interfaces
{
    public interface IPaymentRepository
    {
        Task Create (OrderTransaction orderTransaction);
       
        Task Update (OrderTransaction orderTransaction, Status newStatus);
      
    }
}