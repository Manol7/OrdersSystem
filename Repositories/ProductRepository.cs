using System;
using System.Collections.Generic;
using System.Text;
using OrdersSystem.Models;
using OrdersSystem.Interfaces;

namespace OrdersSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _productsList = new();
        public List<Product> GetAll() => _productsList;
        public bool Add(Product product)
        {   
            if (_productsList.Exists(p => p.ProductId == product.ProductId)) return false;
            _productsList.Add(product);
            return true;
        }
        public bool Remove(Product product)
        {
            if (!_productsList.Exists(p => p.ProductId == product.ProductId)) return false;
            _productsList.Remove(product);
            return true;
        }
        public bool Exists(string name) => _productsList.Exists(p => p.ProductName == name);
        public bool Exists(int id) => _productsList.Exists(p => p.ProductId == id);
        public int GetNextId() => _productsList.Count + 1;
    }
}
