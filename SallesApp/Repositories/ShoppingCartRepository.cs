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
            var ShoppingCartItem = _context.ShoppingCartItems
                .SingleOrDefault(sc => sc.Product.Id == product.Id && sc.ShoppingCartId == shoppingCartId);

            if (ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }        
        
        public void RemoveProduct(string shoppingCartId, Product product)
        {
            var ShoppingCartItem = _context.ShoppingCartItems
                .SingleOrDefault(sc => sc.Product.Id == product.Id &&
                 sc.ShoppingCartId == shoppingCartId);

            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Quantity > 1)
                    ShoppingCartItem.Quantity--;

                else _context.ShoppingCartItems.Remove(ShoppingCartItem);
                
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetAllItens(string shoppingCartId)
        {
            if (string.IsNullOrEmpty(shoppingCartId))
            {
                throw new ArgumentException("O ID do carrinho de compras não pode ser nulo ou vazio.", nameof(shoppingCartId));
            }

            var itens = _context.ShoppingCartItems
                .Where(sc => sc.ShoppingCartId == shoppingCartId)
                .Include(sc => sc.Product)
                .ToList();

            if (itens == null || !itens.Any())
            {
                return new List<ShoppingCartItem>();
            }

            return itens;
        }

        public void RemoveAllProducts(string shoppingCartId)
        {
            var shoppingCartItens = _context.ShoppingCartItems
                                    .Where(sc => sc.ShoppingCartId == shoppingCartId);

                _context.ShoppingCartItems.RemoveRange(shoppingCartItens);
                _context.SaveChanges();

                                    
        }

        public decimal GetTotalPrice(string shoppingCartId)
        {
            return _context.ShoppingCartItems.Where(sc => sc.ShoppingCartId == shoppingCartId)
                                             .Select(x => x.Product.Price * x.Quantity)
                                             .Sum();
        }
    }
}
