using OrdersSystem.Models;
using OrdersSystem.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OrdersSystem.Services
{
    public class Engine
    {
        private readonly CustomerRepository _customerRepo;
        private readonly ProductRepository _productRepo;
        private readonly OrderRepository _orderRepo;

        public Engine(CustomerRepository customerRepo, ProductRepository productRepo, OrderRepository orderRepo)
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
            if(_productRepo.Exists(name)) return false;
            var newProduct = new Product(_productRepo.GetNextId(), name, price);
            _productRepo.Add(newProduct);
            return true;
        }
        public bool AddOrder(int customerId, int productId, int quantity)
        {
            if (_orderRepo.Exists(customerId)) return false;
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
    }
}
