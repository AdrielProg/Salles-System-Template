using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SallesApp.Context;
using SallesApp.Models;

namespace SallesApp.Configurations
{
    public class ItemShoppingCartConfiguration : IEntityTypeConfiguration<ItemShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ItemShoppingCart> builder) 
        { 
            builder.ToTable(nameof(ApplicationDbContext.ItemShoppingCarts));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ShoppingCartId).HasMaxLength(200);
            builder.Property(x => x.Quantity);

            builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.Product.Id);
        }
    }
}
