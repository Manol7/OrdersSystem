using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;
using OrdersSystem.Interfaces;

namespace OrdersSystem.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customersList = new();
        public List<Customer> GetAll() => _customersList;
        public bool Add(Customer customer)
        {
            if (_customersList.Exists(c => c.CustomerId == customer.CustomerId)) return false;
            _customersList.Add(customer);
            return true;
        }
        public bool Remove(Customer customer)
        {
            if (!_customersList.Exists(c => c.CustomerId == customer.CustomerId)) return false;
             _customersList.Remove(customer);
             return true;
        }
        public bool Exists(string name) => _customersList.Exists(c => c.CustomerName == name);
        public bool Exists(int id) => _customersList.Exists(c => c.CustomerId == id);
        public int GetNextId() => _customersList.Count + 1;
    }
}
