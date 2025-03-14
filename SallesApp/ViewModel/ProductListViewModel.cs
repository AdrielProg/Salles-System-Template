using SallesApp.Models;
using SallesApp.Services.Interfaces;

namespace SallesApp.ViewModel
{
    public class ProductListViewModel : Product
    {
        public ProductListViewModel() { }
        public ProductListViewModel(Product product, IEncryptionService encryptionService)
        {
            Id = encryptionService.Encrypt(product.Id.ToString());
            ProductCategoryId = product.ProductCategoryId;
            Name = product.Name;
            IsAvailable = product.IsAvailable;
            Price = product.Price;
            ShortDescription = product.ShortDescription;
            LongDescription = product.LongDescription;
            ImageUrl = product.ImageUrl;
        }

        public new string Id { get; set; }
        public List<ProductListViewModel> Products { get; set; }
    }
}