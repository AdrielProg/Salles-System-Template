using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SallesApp.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}