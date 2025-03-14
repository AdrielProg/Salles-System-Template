using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SallesApp.Context;
using SallesApp.Models;

namespace SallesApp.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {

        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable(nameof(ApplicationDbContext.OrderDetails));

            builder.HasKey(orderDetails => orderDetails.OrderDetailsId);

            builder.HasOne(orderDetails => orderDetails.Product).WithMany(o => o.OrderDetails).HasForeignKey(orderDetails => orderDetails.ProductId);
            builder.HasOne(orderDetails => orderDetails.Order).WithMany(o => o.OrderDetails).HasForeignKey(orderDetails => orderDetails.OrderId);
        }
    }
}