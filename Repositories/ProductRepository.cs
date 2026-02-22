using OrdersSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Repositories
{
    public class ProductRepository
    {
        private readonly List<Product> _productsList = new();
        public List<Product> GetAll() => _productsList;
        public void Add(Product product) => _productsList.Add(product);
        public bool Exists(string name) => _productsList.Exists(p => p.ProductName == name);
        public bool Exists(int id) => _productsList.Exists(p => p.ProductId == id);
        public int GetNextId() => _productsList.Count + 1;
    }
}
