using Microsoft.EntityFrameworkCore;
using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;

namespace SallesApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products => _context.Products.Include(p => p.ProductCategory);

      /*  public IEnumerable<Product> FavoriteProducts => _context.Products
                                                        .Where(p => p.IsFavoriteProduct == true)
                                                        .Include(p => p.ProductCategory);*/

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }
    }
}
