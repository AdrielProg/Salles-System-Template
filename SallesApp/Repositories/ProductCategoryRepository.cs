using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;

namespace SallesApp.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;
        public ProductCategoryRepository(ApplicationDbContext context, IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _context = context;
        }
        public IEnumerable<ProductCategory> Categories => _context.ProductCategories;

        public string GetCurrentCategoryName (string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            int decryptedId = _encryptionService.TryDecryptToInt(id) ?? 0;
            if (decryptedId == 0)
                return null;

            var CurrentCategory = _context.ProductCategories.FirstOrDefault(c => c.Id == decryptedId);
            if (CurrentCategory == null)
                return null;

            return CurrentCategory.Name;
        }
    }
}
