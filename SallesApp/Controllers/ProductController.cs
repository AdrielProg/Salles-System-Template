using Microsoft.AspNetCore.Mvc;
using SallesApp.ViewModel;
using SallesApp.Repositories.Interfaces;

namespace SallesApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, IProductCategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List(int? categoryId)
        {
            var productListViewModel = new ProductListViewModel();

            productListViewModel.Products = categoryId.HasValue
                ? _productRepository.Products.Where(p => p.ProductCategoryId == categoryId.Value).ToList()
                : _productRepository.Products.ToList();

            productListViewModel.Categories = _categoryRepository.Categories.ToList();

            productListViewModel.CurrentCategory = categoryId.HasValue
                ? _categoryRepository.Categories.FirstOrDefault(c => c.Id == categoryId.Value)?.Name ?? "Categoria não encontrada"
                : "Todas as Categorias";

            return View(productListViewModel);
        }
    }
}
