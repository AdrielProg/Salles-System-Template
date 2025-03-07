using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;

namespace SallesApp.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public IEnumerable<ProductCategory> Categories => _context.ProductCategories;
    }
}
