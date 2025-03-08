using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;

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

        public IActionResult List(string categoryId)
        {
            int? decryptedCategoryId = _encryptionService.TryDecryptToInt(categoryId);
            if (categoryId != null && decryptedCategoryId == null)
                return BadRequest("Categoria inválida.");

            var products = decryptedCategoryId.HasValue
                ? _productRepository.Products.Where(p => p.ProductCategoryId == decryptedCategoryId.Value).ToList()
                : _productRepository.Products.ToList();

            var productListViewModels = products.Select(p => new ProductListViewModel(p, _encryptionService)).ToList();

            ViewBag.Categories = _categoryRepository.Categories
                .Select(c => new { Id = _encryptionService.Encrypt(c.Id.ToString()), c.Name })
                .ToList();

            var categories = (IEnumerable<dynamic>)ViewBag.Categories;
            TempData["CurrentCategory"] = categories.Where(c => c.Id == categoryId)
                                        .FirstOrDefault()?.Name ?? "Todos os produtos";

            var productListViewModel = new ProductListViewModel
            {
                Products = productListViewModels
            };

            return View(productListViewModel);
        }
    }
}