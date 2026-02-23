using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OrdersSystem.Models;
using OrdersSystem.Interfaces;

namespace OrdersSystem.Services
{
    public class Engine
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;

        public Engine(ICustomerRepository customerRepo, IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _customerRepo = customerRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
        }
        public List<Customer> GetCustomersList() => _customerRepo.GetAll();
        public List<Product> GetProductsList() => _productRepo.GetAll();
        public List<Order> GetOrdersList() => _orderRepo.GetAll();
        public bool CheckString(string str) => !string.IsNullOrEmpty(str) && str.All(char.IsLetter);
        public int CustomersListCount() => _customerRepo.GetAll().Count;
        public int ProductsListCount() => _productRepo.GetAll().Count;
        public int OrdersListCount() => _orderRepo.GetAll().Count;
        public bool AddCustomer(string name)
        {
            if (_customerRepo.Exists(name)) return false;
            var newCustomer = new Customer(_customerRepo.GetNextId(), name);
            _customerRepo.Add(newCustomer);
            return true;
        }
        public bool AddProduct(string name, decimal price)
        {
            if (_productRepo.Exists(name)) return false;
            var newProduct = new Product(_productRepo.GetNextId(), name, price);
            _productRepo.Add(newProduct);
            return true;
        }
        public bool AddOrder(int customerId, int productId, int quantity)
        {
            if (_customerRepo.Exists(customerId) && _productRepo.Exists(productId))
            {
                var newOrder = new Order(_orderRepo.GetNextId(), customerId, productId, quantity);
                _orderRepo.Add(newOrder);
                return true;
            }
            return false;
        }
        public bool RemoveOrder(int orderId)
        {
            if (_orderRepo.Exists(orderId))
            {
                var orderToRemove = _orderRepo.GetAll().FirstOrDefault(o => o.OrderId == orderId);
                if (orderToRemove != null)
                {
                    _orderRepo.Remove(orderToRemove);
                    return true;
                }
            }
            return false;
        }
        public bool RemoveCustomer(int customerId)
        {
            if (_customerRepo.Exists(customerId))
            {
                var customerToRemove = _customerRepo.GetAll().FirstOrDefault(c => c.CustomerId == customerId);
                var ordersToRemove = _orderRepo.GetAll().Where(o => o.CustomerId == customerId).ToList();
                if (customerToRemove != null)
                {
                    foreach (var order in ordersToRemove)
                    {
                        _orderRepo.Remove(order);
                    }
                    _customerRepo.Remove(customerToRemove);
                    return true;
                }
            }
            return false;
        }
        public bool RemoveProduct(int productId)
        {
            if (_productRepo.Exists(productId))
            {
                var productToRemove = _productRepo.GetAll().FirstOrDefault(p => p.ProductId == productId);
                var ordersToRemove = _orderRepo.GetAll().Where(o => o.ProductId == productId).ToList();
                if (productToRemove != null)
                {
                    foreach (var order in ordersToRemove)
                    {
                        _orderRepo.Remove(order);
                    }
                    _productRepo.Remove(productToRemove);
                    return true;
                }
            }
            return false;
        }
    }
}
