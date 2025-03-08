
namespace SallesApp.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItens { get; set; } = new List<ShoppingCartItem>();

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();
            session.SetString("ShoppingCartId", cartId);

            return new ShoppingCart { ShoppingCartId = cartId };
        }
    }
}