using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Models
{
    public class Customer
    {
        public int CustomerId { get;}
        public string CustomerName { get; set; }
        
        public Customer(int id, string name)
        {
            this.CustomerId = id;
            this.CustomerName = name; 
        }
    }
}
