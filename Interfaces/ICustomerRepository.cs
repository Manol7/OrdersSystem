using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;

namespace OrdersSystem.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        bool Add(Customer customer);
        bool Remove(Customer customer);

        bool Exists(int id);
        bool Exists(string name);
        int GetNextId();
    }
}
