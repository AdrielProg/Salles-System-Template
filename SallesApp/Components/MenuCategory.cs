
using Microsoft.AspNetCore.Mvc;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;

namespace SallesApp.Components;

    public class MenuCategory : ViewComponent
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IEncryptionService _encryptionService;

        public MenuCategory(IProductCategoryRepository categoryRepository, IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _categoryRepository = categoryRepository;
        
        }
        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(c => c.Id)
                .Select(c => new { Id = _encryptionService.Encrypt(c.Id.ToString()), c.Name }).ToList();
            return View(categories);
    }
}