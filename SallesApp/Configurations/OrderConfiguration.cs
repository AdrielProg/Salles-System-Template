
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SallesApp.Context;
using SallesApp.Models;

namespace SallesApp.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.Orders));
            
            builder.HasKey(order => order.OrderId);
            
            builder.HasMany(order => order.OrderDetails).WithOne(orderDetails => orderDetails.Order);
            
        }
        
    }
}