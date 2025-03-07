using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;

namespace SallesApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult List()
        {
            var productListViewModel = new ProductListViewModel();
            
            productListViewModel.Products = _productRepository.Products.ToList();
            productListViewModel.CurrentCategory = "Current";

            return View(productListViewModel);
        }
    }
}
