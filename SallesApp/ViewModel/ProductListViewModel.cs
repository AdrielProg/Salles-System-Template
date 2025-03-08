using SallesApp.Models;

namespace SallesApp.ViewModel
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }

        public List<ProductCategory> Categories { get; set; }
        
        public string CurrentCategory { get; set; }
        
    }
}
