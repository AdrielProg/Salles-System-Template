using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.ViewModel;
using System.Linq;

namespace SallesApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        { 
            var itens = _shoppingCartRepository.GetAllItens(_shoppingCart.ShoppingCartId);
            _shoppingCart.shoppingCartItens = itens;

            var ShoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                TotalPrice = _shoppingCartRepository.GetTotalPrice(_shoppingCart.ShoppingCartId)
            };

            return View(ShoppingCartVM);
        }

        public IActionResult AddProductToCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(p => p.Id == productId);
            var shoppingCartId = _shoppingCart.ShoppingCartId;

            if (selectedProduct != null) 
            {
                _shoppingCartRepository.AddProduct(shoppingCartId, selectedProduct);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult RemoveProductToCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                  .FirstOrDefault(p => p.Id == productId);
            var shoppingCartId = _shoppingCart.ShoppingCartId;

            if (selectedProduct != null) 
            {
                _shoppingCartRepository.RemoveProduct(shoppingCartId, selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}
