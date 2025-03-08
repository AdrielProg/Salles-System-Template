using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;

namespace SallesApp.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart GetShoppingCart(string shoppingCartId)
        {
            return new ShoppingCart { ShoppingCartId = shoppingCartId };
        }

        public void AddProduct(string shoppingCartId, Product product)
        {
            var itemShoppingCart = _context.ItemShoppingCarts
                .SingleOrDefault(sc => sc.Product.Id == product.Id && sc.ShoppingCartId == shoppingCartId);

            if (itemShoppingCart == null)
            {
                itemShoppingCart = new ItemShoppingCart
                {
                    ShoppingCartId = shoppingCartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ItemShoppingCarts.Add(itemShoppingCart);
            }
            else
            {
                itemShoppingCart.Quantity++;
            }
            _context.SaveChanges();
        }        
        
        public void RemoveProduct(string shoppingCartId, Product product)
        {
            var itemShoppingCart = _context.ItemShoppingCarts
                .SingleOrDefault(sc => sc.Product.Id == product.Id &&
                 sc.ShoppingCartId == shoppingCartId);

            if (itemShoppingCart != null)
            {
                if (itemShoppingCart.Quantity > 1)
                    itemShoppingCart.Quantity--;

                else _context.ItemShoppingCarts.Remove(itemShoppingCart);
                
            }
            _context.SaveChanges();
        }

        public List<ItemShoppingCart> GetAllItens(string shoppingCartId)
        {
            if (string.IsNullOrEmpty(shoppingCartId))
            {
                throw new ArgumentException("O ID do carrinho de compras não pode ser nulo ou vazio.", nameof(shoppingCartId));
            }

            var itens = _context.ItemShoppingCarts
                .Where(sc => sc.ShoppingCartId == shoppingCartId)
                .Include(sc => sc.Product)
                .ToList();

            if (itens == null || !itens.Any())
            {
                return new List<ItemShoppingCart>();
            }

            return itens;
        }

        public void RemoveAllProducts(string shoppingCartId)
        {
            var itensShoppingCart = _context.ItemShoppingCarts
                                    .Where(sc => sc.ShoppingCartId == shoppingCartId);

                _context.ItemShoppingCarts.RemoveRange(itensShoppingCart);
                _context.SaveChanges();

                                    
        }

        public decimal GetTotalPrice(string shoppingCartId)
        {
            return _context.ItemShoppingCarts.Where(sc => sc.ShoppingCartId == shoppingCartId)
                                             .Select(x => x.Product.Price * x.Quantity)
                                             .Sum();
        }
    }
}
