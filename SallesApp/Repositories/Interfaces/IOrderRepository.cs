using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SallesApp.Models;

namespace SallesApp.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}