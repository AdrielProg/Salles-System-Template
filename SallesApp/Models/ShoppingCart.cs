using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SallesApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SallesApp.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItens { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart() 
        {
        }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetRequiredService<ApplicationDbContext>();
            
            // Obter ou criar ID do carrinho na sessão
            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();
            session.SetString("ShoppingCartId", cartId);
            
            // Verificar se o carrinho já existe no banco de dados
            var cartExists = context.ShoppingCarts.Any(c => c.ShoppingCartId == cartId);
            
            if (!cartExists)
            {
                var newCart = new ShoppingCart { ShoppingCartId = cartId };
                context.ShoppingCarts.Add(newCart);
                context.SaveChanges();
            }
 
            return new ShoppingCart(context) 
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
        
        private void EnsureCartExists()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Context não foi inicializado corretamente");
            }
            
            var cartExists = _context.ShoppingCarts.Any(c => c.ShoppingCartId == ShoppingCartId);
            if (!cartExists)
            {
                _context.ShoppingCarts.Add(new ShoppingCart { ShoppingCartId = ShoppingCartId });
                _context.SaveChanges();
            }
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

        public void RemoveAllProducts()
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