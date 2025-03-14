using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;

namespace SallesApp.ViewModel
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartService ShoppingCart { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
