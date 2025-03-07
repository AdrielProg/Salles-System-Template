    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SallesApp.Context;
    using SallesApp.Models;

    namespace SallesApp.Configurations
    {
        public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.ToTable(nameof(ApplicationDbContext.Products));

                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
                builder.Property(p => p.Price).IsRequired();
                builder.Property(p => p.ShortDescription).HasMaxLength(150);
                builder.Property(p => p.LongDescription).HasMaxLength(300);
                builder.Property(p => p.IsAvailable).HasDefaultValue(true);

                builder.HasOne(p => p.ProductCategory).WithMany(p => p.Products).HasForeignKey(p => p.ProductCategoryId);
            }
        }
    }
