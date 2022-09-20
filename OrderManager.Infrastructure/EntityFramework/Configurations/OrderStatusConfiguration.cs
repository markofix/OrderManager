using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatus");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
