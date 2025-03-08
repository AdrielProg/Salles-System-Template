using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services;
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

            var productListViewModel = new ProductListViewModel
            {
                Products = decryptedCategoryId.HasValue
                    ? _productRepository.Products.Where(p => p.ProductCategoryId == decryptedCategoryId.Value).ToList()
                    : _productRepository.Products.ToList(),

                Categories = _categoryRepository.Categories.ToList(),

                CurrentCategory = decryptedCategoryId.HasValue
                    ? _categoryRepository.Categories.FirstOrDefault(c => c.Id == decryptedCategoryId.Value)?.Name ?? "Categoria não encontrada"
                    : "Todas as Categorias"
            };

            return View(productListViewModel);
        }
    }
}