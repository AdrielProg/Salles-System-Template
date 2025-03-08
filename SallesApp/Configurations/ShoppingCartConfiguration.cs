using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SallesApp.Context;
using SallesApp.Models;

namespace SallesApp.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder) 
        {
            builder.ToTable(nameof(ApplicationDbContext.ShoppingCarts));
            builder.HasKey(x => x.ShoppingCartId);

            builder.HasMany(x => x.shoppingCartItens).WithOne(x => x.ShoppingCart).HasForeignKey(x => x.ShoppingCartId);
        }

    }
}
