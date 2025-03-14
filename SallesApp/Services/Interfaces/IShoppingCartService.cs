using SallesApp.Models;
using SallesApp.Services;

namespace SallesApp.Services.Interfaces;

public interface IShoppingCartService 
{
  public void AddProduct(Product product);
    public void RemoveProduct(Product product);
    public int RemoveProduct(string productId);
    public List<ShoppingCartItem> GetAllItens();
    decimal GetTotalPrice();

}