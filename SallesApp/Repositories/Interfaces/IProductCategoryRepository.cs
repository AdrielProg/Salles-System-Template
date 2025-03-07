using SallesApp.Models;

namespace SallesApp.Repositories.Interfaces
{
    public interface IProductCategoryRepository 
    {
        IEnumerable<ProductCategory> Categories { get; }
    }
}
