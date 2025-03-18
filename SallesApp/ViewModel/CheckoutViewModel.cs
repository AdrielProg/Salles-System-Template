using SallesApp.Models;

namespace SallesApp.ViewModel
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public ShoppingCartViewModel CartViewModel { get; set; }
    }
}
