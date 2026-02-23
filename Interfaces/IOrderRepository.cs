using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;

namespace OrdersSystem.Interfaces
{
    public interface IOrderRepository
    {
            List<Order> GetAll();
            bool Add(Order order);
            bool Remove(Order order);
            bool Exists(int id);
            int GetNextId();
    }
}
