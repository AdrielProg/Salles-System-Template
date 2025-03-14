
using Microsoft.EntityFrameworkCore;

using SallesApp.Context;
using SallesApp.Services.Interfaces;


namespace SallesApp.Models
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItens { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCartService() 
        {
        }

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public static ShoppingCartService GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetRequiredService<ApplicationDbContext>();
            
            // Obter ou criar ID do carrinho na sessão
            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();
            session.SetString("ShoppingCartId", cartId);
 
            return new ShoppingCartService(context) 
            {
                 ShoppingCartId = cartId 
            };
        }
           
        public void AddProduct(Product product)
        {
            
            var shoppingCartItem = _context.ShoppingCartItems
                .Include(sc => sc.Product)
                .SingleOrDefault(sc => sc.Product.Id == product.Id && sc.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }
        

        public int RemoveProduct(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                .SingleOrDefault(sc => sc.Product.Id == product.Id &&
                 sc.ShoppingCartId == ShoppingCartId);
         
            var currentQuantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    currentQuantity = shoppingCartItem.Quantity;
                }
                else 
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
                _context.SaveChanges();
            }
            
            return currentQuantity;
        }

        public List<ShoppingCartItem> GetAllItens()
        {
            return _context.ShoppingCartItems
                .Where(sc => sc.ShoppingCartId == ShoppingCartId)
                .Include(sc => sc.Product)
                .ToList() ?? new List<ShoppingCartItem>();
        }

        public void ClearCart()
        {
            var shoppingCartItens = _context.ShoppingCartItems
                                    .Where(sc => sc.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItens);
            _context.SaveChanges();
        }
        
        public decimal GetTotalPrice()
        {
            var total = _context.ShoppingCartItems
                .Where(sc => sc.ShoppingCartId == ShoppingCartId)
                .Select(sc => sc.Product.Price * sc.Quantity).Sum();
            return total;
        }
    }
}