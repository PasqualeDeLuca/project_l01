using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_l01.Api.Models;

namespace Project_l01.Api.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        
        builder.ToTable("orderItems", table =>
        {
            table.HasCheckConstraint(
                "CK_order_items_quantity_positive",
                "\"quantity\" > 0"
            );

            table.HasCheckConstraint(
                "CK_order_items_unit_price_positive",
                "\"unit_price\" > 0" 
            );

        });

        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder.Property(orderItem => orderItem.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(orderItem => orderItem.Order)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(orderItem => orderItem.Product)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(orderItem => orderItem.OrderId);

        builder.HasIndex(orderItem => orderItem.ProductId);
    }
}