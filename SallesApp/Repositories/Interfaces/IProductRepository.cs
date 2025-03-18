using SallesApp.Models;

namespace SallesApp.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProductById(int productId);
        List<Product> FindAllProudcts();
        List<Product> GetProductsByCategoryId(string categoryId);
        
        // New method for searching products by name or description
        IEnumerable<Product> SearchProducts(string searchTerm);
    }
}
