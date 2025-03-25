using Microsoft.AspNetCore.Mvc;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;
using SallesApp.ViewModel;

namespace SallesApp.Controllers
{
    public class ShoppingCartController(
        IEncryptionService encryptionService,
        IProductRepository productRepository,
        ShoppingCartService shoppingCart) : Controller
    {
        private readonly ShoppingCartService _shoppingCart = shoppingCart;
        private readonly IEncryptionService _encryptionService = encryptionService;
        private readonly IProductRepository _productRepository = productRepository;

        public IActionResult Index()
        { 
            var itens = _shoppingCart.GetAllItens();
            _shoppingCart.shoppingCartItens = itens;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                TotalPrice = _shoppingCart.GetTotalPrice()
            };

            return View(shoppingCartVM);
        }

        public IActionResult AddProductToCart(string productId)
        {
            int? decryptedProductId = _encryptionService.TryDecryptToInt(productId);
            var selectedProduct = _productRepository.Products
                                .FirstOrDefault(p => p.Id == decryptedProductId);

            if (selectedProduct != null) 
            {
                _shoppingCart.AddProduct(selectedProduct);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult RemoveProductToCart(int productId)
        {
            var selectedProduct = _productRepository.Products
                                .FirstOrDefault(p => p.Id == productId);

            if (selectedProduct != null) 
            {
                _shoppingCart.RemoveProduct(selectedProduct);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
