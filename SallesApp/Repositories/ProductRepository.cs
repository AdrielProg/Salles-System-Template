using Microsoft.EntityFrameworkCore;
using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;

namespace SallesApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;

        public ProductRepository(ApplicationDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public IEnumerable<Product> Products => _context.Products.Include(p => p.ProductCategory);

      /*  public IEnumerable<Product> FavoriteProducts => _context.Products
                                                        .Where(p => p.IsFavoriteProduct == true)
                                                        .Include(p => p.ProductCategory);*/

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public List<Product> FindAllProudcts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsByCategoryId(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                return FindAllProudcts();

            int? decryptedCategoryId = _encryptionService.TryDecryptToInt(categoryId);
            if (decryptedCategoryId == null)
                return FindAllProudcts();

            return _context.Products.Where(p => p.ProductCategoryId == decryptedCategoryId).ToList();
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Product>();
                
            searchTerm = searchTerm.ToLower();
            
            return _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.Name.ToLower().Contains(searchTerm) || 
                           p.ShortDescription.ToLower().Contains(searchTerm) ||
                           p.ProductCategory.Name.ToLower().Contains(searchTerm)) 
                .ToList();
        }
    }
}
