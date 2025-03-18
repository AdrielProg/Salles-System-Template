using SallesApp.Context;
using SallesApp.Models;
using SallesApp.Repositories.Interfaces;
using SallesApp.Services.Interfaces;

namespace SallesApp.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IEncryptionService _encryptionService;
    private readonly IShoppingCartService _shoppingCartService;

    public OrderRepository(ApplicationDbContext context, IEncryptionService encryptionService, IShoppingCartService shoppingCartService)
    {
        _context = context;
        _encryptionService = encryptionService;
        _shoppingCartService = shoppingCartService;
    }

    public void CreateOrder(Order order)
    {
        order.ShippedDate = DateTime.Now;
        _context.Orders.Add(order);
        _context.SaveChanges();

        var shoppingCartItems = _shoppingCartService.GetAllItens()
                                .Select(item =>
        new OrderDetails
        {
            OrderId = order.OrderId,
            ProductId = item.Product.Id,
            Quantity = item.Quantity,
            Price = item.Product.Price
        });

        _context.OrderDetails.AddRange(shoppingCartItems);
        _context.SaveChanges();
    }
}
