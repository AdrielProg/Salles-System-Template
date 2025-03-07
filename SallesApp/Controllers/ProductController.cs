using Microsoft.AspNetCore.Mvc;
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
            return View(_productRepository.Products);
        }
    }
}
