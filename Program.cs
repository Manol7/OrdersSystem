using OrdersSystem.Models;
using OrdersSystem.Repositories;
using OrdersSystem.Services;
using OrdersSystem.UserInterface;

namespace OrdersSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var CustomerRepo = new CustomerRepository();
            var ProductRepo = new ProductRepository();
            var OrderRepo = new OrderRepository();

            var Engine = new Engine(CustomerRepo, ProductRepo, OrderRepo);
            var Ui = new Ui(Engine);
            Engine.AddProduct("Laptop", 1500);
            Engine.AddProduct("Phone", 800);
            Engine.AddCustomer("Alice");
            Engine.AddCustomer("John");
            Engine.AddOrder(1, 1, 2);
            Engine.AddOrder(1, 2, 5);
            Engine.AddOrder(2, 2, 15);
            Ui.Run();
        }
    }
}
