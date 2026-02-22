using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;

namespace OrdersSystem.Repositories
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customersList = new();
        public List<Customer> GetAll() => _customersList;
        public void Add(Customer customer) => _customersList.Add(customer);
        public bool Exists(string name) => _customersList.Exists(c => c.CustomerName == name);
        public bool Exists(int id) => _customersList.Exists(c => c.CustomerId == id);
        public int GetNextId() => _customersList.Count + 1;
    }
}
