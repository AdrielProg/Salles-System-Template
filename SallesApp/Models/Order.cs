using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SallesApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public int TotalItems { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal TotalOrder { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
