using SallesApp.Models;
using SallesApp.Services;

namespace SallesApp.Services.Interfaces;

public interface IShoppingCartService 
{
  public void AddProduct(Product product);
    public int RemoveProduct(Product product);
    public List<ShoppingCartItem> GetAllItens();
    decimal GetTotalPrice();

}