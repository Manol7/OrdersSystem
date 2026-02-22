using OrdersSystem.Models;
using OrdersSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.UserInterface
{
    public class Ui
    {
        private readonly Engine _engine;
        public Ui(Engine engine)
        {
            _engine = engine;
        }
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("Welcome to the Orders System!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("===============================");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Show Customers");
                Console.WriteLine("3. Add Product");
                Console.WriteLine("4. Show Products");
                Console.WriteLine("5. Add Order");
                Console.WriteLine("6. Show Orders");
                Console.WriteLine("7. Exit");
                Console.WriteLine("===============================");

                string choice = Console.ReadKey().KeyChar.ToString();
                Console.Clear();

                switch (choice)
                {
                    case "1": AddCustomer(); break;
                    case "2": ShowCustomers(); break;
                    case "3": AddProduct(); break;
                    case "4": ShowProducts(); break;
                    case "5": AddOrder();break;
                    case "6": ShowOrders(); break;
                    case "7": Console.WriteLine("Exiting the system. Goodbye!"); return;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
        public void AddCustomer()
        {
            string newCustomerName;
            Console.WriteLine("Enter Customer Name:");

            newCustomerName = Console.ReadLine() ?? "";
            while (!_engine.CheckString(newCustomerName))
            {
                Console.WriteLine("Invalid customer name. Try again, use just letters:");
                newCustomerName = Console.ReadLine() ?? "";
            }
            if (_engine.AddCustomer(newCustomerName))
                Console.WriteLine("Customer added successfully.");
            else
                Console.WriteLine("Customer with the same name already exists.");
        }
        public void ShowCustomers()
        {
            if (_engine.CustomersListCount() > 0)
            {
                List<Customer> customersList = _engine.GetCustomersList();

                Console.WriteLine("Customers:");
                Console.WriteLine("===============================");
                foreach (Customer c in customersList)
                {
                    Console.WriteLine($"ID: {c.CustomerId}, Name: {c.CustomerName}");
                }
                Console.WriteLine("===============================");
            }
            else
                Console.WriteLine("No customers found.");
        }
        public void ShowProducts()
        {
            if (_engine.ProductsListCount() > 0)
            {
                List<Product> productsList = _engine.GetProductsList();
                Console.WriteLine("Products:");
                Console.WriteLine("===============================");
                foreach (Product p in productsList)
                {
                    Console.WriteLine($"ID: {p.ProductId}, Name: {p.ProductName}, Price: {p.ProductPrice}");
                }
                Console.WriteLine("===============================");
            }
            else
                Console.WriteLine("No products found.");
        }
        public void ShowOrders()
        {
            if (_engine.OrdersListCount() == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }
            else 
            {
                List<Order> ordersList = _engine.GetOrdersList();
                List<Product> productsList = _engine.GetProductsList();
                List<Customer> customersList = _engine.GetCustomersList();
                var res =
                    from o in ordersList
                    join p in productsList on o.ProductId equals p.ProductId
                    join c in customersList on o.CustomerId equals c.CustomerId
                    select new { o.OrderId, Customer = c, Product = p, o.Quantity };
                Console.WriteLine("Orders placed:");
                Console.WriteLine("===============================");
                foreach (var o in res)
                {
                    Console.WriteLine($"ID: {o.OrderId}, Customer: {o.Customer.CustomerName}, Product: {o.Product.ProductName}, Quantity: {o.Quantity}");
                    Console.WriteLine("===============================");
                }
            }
        }
        public void AddProduct()
        {
            string newProductName;
            decimal newProductPrice;
            Console.WriteLine("Enter new product name:");
            newProductName = Console.ReadLine() ?? "";
            while (!_engine.CheckString(newProductName))
            {
                Console.WriteLine("Invalid product name. Try again, use just letters:");
                newProductName = Console.ReadLine() ?? "";
            }
            Console.WriteLine("Enter new product price (Use ',' as the decimal separator!)");
            while (!decimal.TryParse(Console.ReadLine(), out newProductPrice) || newProductPrice < 0)
            {
                Console.WriteLine("Invalid product price. Try again, use a positive decimal number:");   
            }
            if (_engine.AddProduct(newProductName, newProductPrice))
            {
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine("Product with the same name already exists.");
            }
        }
        public void AddOrder()
        {
            if (_engine.CustomersListCount() == 0 || _engine.ProductsListCount() == 0)
            {
                Console.WriteLine("Cannot add order. Please ensure there are customers and products available.");
                return;
            }
            else
            {
                int customerId, productId, quantity;
                var customers = _engine.GetCustomersList();
                var products = _engine.GetProductsList();
                ShowCustomers();
                ShowProducts();
                Console.WriteLine("Enter customer Id:");
                while(!int.TryParse(Console.ReadLine(), out customerId) || !customers.Exists(c => c.CustomerId == customerId))
                {
                    Console.WriteLine("Invalid customer Id. Try again, use the one from list above:");
                }

                Console.WriteLine("Enter product Id:");
                while(!int.TryParse(Console.ReadLine(), out productId) || !products.Exists(p => p.ProductId == productId))
                {
                    Console.WriteLine("Invalid product Id. Try again, use the one from list above:");
                }

                Console.WriteLine("Enter quantity:");
                while(!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Try again, use a positive integer:");
                }

                if (_engine.AddOrder(customerId, productId, quantity))
                {
                    Console.WriteLine("Order added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add order. Please check the customer and product IDs.");
                }
            }
        }
    }
}
