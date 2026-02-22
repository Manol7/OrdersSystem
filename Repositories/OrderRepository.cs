using System;
using OrdersSystem.Models;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Repositories
{
    public class OrderRepository
    {
        private readonly List<Order> _ordersList = new();
        public List<Order> GetAll() => _ordersList;
        public void Add(Order order) => _ordersList.Add(order);
        public void Remove(Order order) => _ordersList.Remove(order);
        public bool Exists(int id) => _ordersList.Exists(o => o.OrderId == id);
        public int GetNextId() => _ordersList.Count + 1;
    }
}
