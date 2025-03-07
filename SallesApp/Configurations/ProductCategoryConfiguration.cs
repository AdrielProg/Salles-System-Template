using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SallesApp.Context;
using SallesApp.Models;

namespace SallesApp.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.ProductCategories));
           
            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.Name).IsRequired().HasMaxLength(200);
            builder.Property(pc => pc.Description).HasMaxLength(500);

            builder.HasMany(pc => pc.Products).WithOne(p => p.ProductCategory).HasForeignKey(p => p.ProductCategoryId);
        }
    }
}