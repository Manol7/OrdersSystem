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

            Ui.Run();
        }
    }
}
