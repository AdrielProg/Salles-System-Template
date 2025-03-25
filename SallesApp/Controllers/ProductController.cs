using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;
using SallesApp.Models;

namespace SallesApp.Controllers
{
    public class ProductController(
        IProductRepository productRepository,
        IProductCategoryRepository categoryRepository,
        IEncryptionService encryptionService) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductCategoryRepository _categoryRepository = categoryRepository;
        private readonly IEncryptionService _encryptionService = encryptionService;

        [HttpGet]
        [Route("product/list/{categoryId?}")]
        public IActionResult List(string categoryId = null)
        {   
            var products = _productRepository.GetProductsByCategoryId(categoryId);
            if (products == null || !products.Any())
            {
                return NotFound("NotFound");
            }
            var productListViewModel = new ProductListViewModel();
            productListViewModel.Products = products.Select(p => new ProductListViewModel(p, _encryptionService)).ToList();

            var currentCategoryName = _categoryRepository.GetCurrentCategoryName(categoryId) ?? "Todos os produtos";
            ViewBag.CurrentCategory = currentCategoryName;

            var viewResult = View(productListViewModel);            
            return viewResult;
        }
    
        public ViewResult Search(string searchTerm)
        {
            var products = _productRepository.SearchProducts(searchTerm);
            var productListViewModel = new ProductListViewModel();
            
            productListViewModel.Products = products.Select(p => new ProductListViewModel(p, _encryptionService)).ToList();
            ViewBag.CurrentCategory = "Resultados para: " + searchTerm;

            if (productListViewModel.Products == null || !productListViewModel.Products.Any())
            {
                ViewBag.CurrentCategory = "Nenhum resultado encontrado para: " + searchTerm;
                return View("List", productListViewModel);
            }
            return View("List", productListViewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}