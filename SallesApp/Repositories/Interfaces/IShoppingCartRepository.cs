using SallesApp.Models;
namespace SallesApp.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetShoppingCart(string shoppingCartId);
        void AddProduct(string shoppingCartId, Product product);
        void RemoveProduct(string shoppingCartId, Product product);
        void RemoveAllProducts(string shoppingCartId);
        decimal GetTotalPrice(string shoppingCartId);
        List<ShoppingCartItem> GetAllItens(string shoppingCartId);

    }
}
