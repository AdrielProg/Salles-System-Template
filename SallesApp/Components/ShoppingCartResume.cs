using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.ViewModel;

namespace SallesApp.Components
{
    public class ShoppingCartResume : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCart;

        public ShoppingCartResume(ShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke() 
        {
            var itens = _shoppingCart.GetAllItens();

/*           TEST
 *           var itens = new List<ShoppingCartItem> 
            {
                new ShoppingCartItem(),
                new ShoppingCartItem()
            };*/

            _shoppingCart.shoppingCartItens = itens;

            var ShoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                TotalPrice = _shoppingCart.GetTotalPrice()
            };

            return View(ShoppingCartVM);
        }
    }
}
