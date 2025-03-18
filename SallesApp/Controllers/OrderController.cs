using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.ViewModel;


namespace SallesApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCartService _shoppingCartService;

        public OrderController(IOrderRepository orderRepository, ShoppingCartService shoppingCartService)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Checkout()
        {
            var shoppingCartItems = _shoppingCartService.GetAllItens();
            if (shoppingCartItems == null || !shoppingCartItems.Any())
            {
                 var shoppingCartId = _shoppingCartService.GetShoppingCartId();
                TempData["Error"] = "Seu carrinho está vazio!" + shoppingCartId;
                return RedirectToAction("Error", "Product");

            }
            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCartService,
                ShoppingCartItens = shoppingCartItems,
                TotalPrice = _shoppingCartService.GetTotalPrice()
            };

            _shoppingCartService.shoppingCartItens = shoppingCartItems;

            var checkoutViewModel = new CheckoutViewModel
            {
                Order = new Order(),
                CartViewModel = shoppingCartVM
            };

            return View(checkoutViewModel);
        }
    }

}