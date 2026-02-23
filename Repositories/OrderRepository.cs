using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;
using OrdersSystem.Interfaces;

namespace OrdersSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _ordersList = new();
        public List<Order> GetAll() => _ordersList;
        public List<Order> GetOrdersByCustomerId(int customerId) => _ordersList.FindAll(o => o.CustomerId == customerId);
        public bool Add(Order order)
        {
            if (Exists(order.OrderId)) return false;
            _ordersList.Add(order);
            return true;
        }
        public bool Remove(Order order)
        {
            if (!Exists(order.OrderId)) return false; 
            return _ordersList.Remove(order);
        }
        public bool Exists(int id) => _ordersList.Exists(o => o.OrderId == id);
        public int GetNextId() => _ordersList.Count + 1;
    }
}
