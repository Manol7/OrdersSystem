using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;

namespace OrdersSystem.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        bool Add(Product product);
        bool Remove(Product product);
        bool Exists(int id);
        bool Exists(string name);
        int GetNextId();
    }
}
