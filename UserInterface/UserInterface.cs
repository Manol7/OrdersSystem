using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrdersSystem.Services;
using OrdersSystem.DTOs;

namespace OrdersSystem.UserInterface
{
    public class Ui
    {
        private readonly Engine _engine;

        public Ui(Engine engine)
        {
            _engine = engine;
        }

        private string ReadInputOrExit()
        {
            string? input = Console.ReadLine();

            if (input != null && input.Trim().ToLower() == "e")
            {
                throw new OperationCanceledException("User exited");
            }

            return input ?? "";
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("Welcome to the Orders System!");
                    Console.WriteLine("Please select an option:");
                    Console.WriteLine("===============================");
                    Console.WriteLine("1. Add Customer");
                    Console.WriteLine("2. Remove Customer");
                    Console.WriteLine("3. Show Customers");
                    Console.WriteLine("4. Add Product");
                    Console.WriteLine("5. Remove Product");
                    Console.WriteLine("6. Show Products");
                    Console.WriteLine("7. Add Order");
                    Console.WriteLine("8. Remove Order");
                    Console.WriteLine("9. Show Orders");
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("===============================");
                    Console.WriteLine("Press 'e' anytime to cancel current operation");

                    string choice = Console.ReadKey().KeyChar.ToString().ToLower();
                    Console.Clear();

                    switch (choice)
                    {
                        case "1": AddCustomer(); break;
                        case "2": RemoveCustomer(); break;
                        case "3": ShowCustomers(); break;
                        case "4": AddProduct(); break;
                        case "5": RemoveProduct(); break;
                        case "6": ShowProducts(); break;
                        case "7": AddOrder(); break;
                        case "8": RemoveOrder(); break;
                        case "9": ShowOrders(); break;
                        case "0": Console.WriteLine("Exiting the system. Goodbye!"); return;
                        default: Console.WriteLine("Invalid option. Please try again."); break;
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("\nOperation cancelled. Returning to main menu...");
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                }
            }
        }

        public void AddCustomer()
        {
            Console.WriteLine("Enter Customer Name (or 'e' to cancel):");

            string newCustomerName = ReadInputOrExit();
            while (!_engine.CheckString(newCustomerName))
            {
                Console.WriteLine("Invalid customer name. Try again, use just letters:");
                newCustomerName = ReadInputOrExit();
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
                List<CustomerDTO> customersList = _engine.GetCustomersList();

                Console.WriteLine("Customers:");
                Console.WriteLine("===============================");
                foreach (CustomerDTO c in customersList)
                {
                    Console.WriteLine($"ID: {c.Id}, Name: {c.Name}");
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
                List<ProductDTO> productsList = _engine.GetProductsList();
                Console.WriteLine("Products:");
                Console.WriteLine("===============================");
                foreach (ProductDTO p in productsList)
                {
                    Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}");
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

            List<OrderDTO> ordersList = _engine.GetOrdersList();
            List<ProductDTO> productsList = _engine.GetProductsList();
            List<CustomerDTO> customersList = _engine.GetCustomersList();

            var res =
                from o in ordersList
                join p in productsList on o.ProductId equals p.Id
                join c in customersList on o.CustomerId equals c.Id
                select new { o.Id, Customer = c, Product = p, o.Quantity };

            Console.WriteLine("Orders placed:");
            Console.WriteLine("===============================");
            foreach (var o in res)
            {
                Console.WriteLine($"ID: {o.Id}, Customer: {o.Customer.Name}, Product: {o.Product.Name}, Quantity: {o.Quantity}");
                Console.WriteLine("===============================");
            }
        }

        public void AddProduct()
        {
            Console.WriteLine("Enter new product name (or 'e' to cancel):");
            string newProductName = ReadInputOrExit();

            while (!_engine.CheckString(newProductName))
            {
                Console.WriteLine("Invalid product name. Try again, use just letters:");
                newProductName = ReadInputOrExit();
            }

            Console.WriteLine("Enter new product price (Use ',' as the decimal separator!)");
            decimal newProductPrice;

            while (true)
            {
                string input = ReadInputOrExit();
                if (decimal.TryParse(input, out newProductPrice) && newProductPrice >= 0)
                    break;

                Console.WriteLine("Invalid product price. Try again, use a positive decimal number:");
            }

            if (_engine.AddProduct(newProductName, newProductPrice))
                Console.WriteLine("Product added successfully.");
            else
                Console.WriteLine("Product with the same name already exists.");
        }

        public void AddOrder()
        {
            if (_engine.CustomersListCount() == 0 || _engine.ProductsListCount() == 0)
            {
                Console.WriteLine("Cannot add order. Please ensure there are customers and products available.");
                return;
            }

            var customers = _engine.GetCustomersList();
            var products = _engine.GetProductsList();

            ShowCustomers();
            ShowProducts();

            Console.WriteLine("Enter customer Id (or 'e' to cancel):");
            int customerId;

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out customerId) && customers.Exists(c => c.Id == customerId))
                    break;

                Console.WriteLine("Invalid customer Id. Try again, use the one from list above:");
            }

            Console.WriteLine("Enter product Id (or 'e' to cancel):");
            int productId;

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out productId) && products.Exists(p => p.Id == productId))
                    break;

                Console.WriteLine("Invalid product Id. Try again, use the one from list above:");
            }

            Console.WriteLine("Enter quantity (or 'e' to cancel):");
            int quantity;

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out quantity) && quantity > 0)
                    break;

                Console.WriteLine("Invalid quantity. Try again, use a positive integer:");
            }

            if (_engine.AddOrder(customerId, productId, quantity))
                Console.WriteLine("Order added successfully.");
            else
                Console.WriteLine("Failed to add order. Please check the customer and product IDs.");
        }

        public void RemoveOrder()
        {
            if (_engine.OrdersListCount() == 0)
            {
                Console.WriteLine("No orders to remove.");
                return;
            }

            ShowOrders();
            Console.WriteLine("Enter order Id to remove (or 'e' to cancel):");

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out int orderId) && _engine.GetOrdersList().Exists(o => o.Id == orderId))
                {
                    if (_engine.RemoveOrder(orderId))
                        Console.WriteLine("Order removed successfully.");
                    else
                        Console.WriteLine("Failed to remove order.");

                    return;
                }

                Console.WriteLine("Invalid order Id. Try again, use the one from list above:");
            }
        }

        public void RemoveCustomer()
        {
            if (_engine.CustomersListCount() == 0)
            {
                Console.WriteLine("No customers to remove.");
                return;
            }

            ShowCustomers();
            Console.WriteLine("Enter customer Id to remove (or 'e' to cancel):");

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out int customerId) && _engine.GetCustomersList().Exists(c => c.Id == customerId))
                {
                    if (_engine.RemoveCustomer(customerId))
                        Console.WriteLine("Customer removed successfully.");
                    else
                        Console.WriteLine("Failed to remove customer.");

                    return;
                }

                Console.WriteLine("Invalid customer Id. Try again, use the one from list above:");
            }
        }

        public void RemoveProduct()
        {
            if (_engine.ProductsListCount() == 0)
            {
                Console.WriteLine("No products to remove.");
                return;
            }

            ShowProducts();
            Console.WriteLine("Enter product Id to remove (or 'e' to cancel):");

            while (true)
            {
                string input = ReadInputOrExit();
                if (int.TryParse(input, out int productId) && _engine.GetProductsList().Exists(p => p.Id == productId))
                {
                    if (_engine.RemoveProduct(productId))
                        Console.WriteLine("Product removed successfully.");
                    else
                        Console.WriteLine("Failed to remove product.");

                    return;
                }

                Console.WriteLine("Invalid product Id. Try again, use the one from list above:");
            }
        }
    }
}