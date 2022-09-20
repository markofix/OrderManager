using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId)
                .IsRequired();

            builder.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(k => k.OrderId)
                .IsRequired();
        }
    }
}
