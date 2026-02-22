using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersSystem.Models
{
    public class Order
    {
        public int OrderId { get; }
        public int CustomerId { get; }
        public int ProductId { get; }
        public int Quantity { get; }

        public Order(int orderId, int customerId, int productId, int quantity)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
