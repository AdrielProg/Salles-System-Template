using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;
using SallesApp.Models;

namespace SallesApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IEncryptionService _encryptionService;

        public ProductController(
            IProductRepository productRepository,
            IProductCategoryRepository categoryRepository,
            IEncryptionService encryptionService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _encryptionService = encryptionService;
        }
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
            viewResult.StatusCode = 200;
            
            return viewResult;
        }
    }
}