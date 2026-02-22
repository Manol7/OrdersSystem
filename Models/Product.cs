using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Models
{
    public class Product
    {
        public int ProductId { get; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public Product(int id, string name, decimal price)
        {
            this.ProductId = id;
            this.ProductName = name;
            this.ProductPrice = price;
        }
    }
}
