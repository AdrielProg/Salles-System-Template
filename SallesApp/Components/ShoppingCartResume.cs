using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.ViewModel;

namespace SallesApp.Components
{
    public class ShoppingCartResume : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartResume(ShoppingCart shoppingCart, IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCart = shoppingCart;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public IViewComponentResult Invoke() 
        {
            var itens = _shoppingCartRepository.GetAllItens(_shoppingCart.ShoppingCartId);

/*           TEST
 *           var itens = new List<ItemShoppingCart> 
            {
                new ItemShoppingCart(),
                new ItemShoppingCart()
            };*/

            _shoppingCart.itensShoppingCart = itens;

            var ShoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                TotalPrice = _shoppingCartRepository.GetTotalPrice(_shoppingCart.ShoppingCartId)
            };

            return View(ShoppingCartVM);
        }
    }
}
