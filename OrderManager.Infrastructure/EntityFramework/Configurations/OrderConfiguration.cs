using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
               .UseIdentityColumn();

            builder.HasOne(e => e.Restaurant)
                .WithMany()
                .HasForeignKey(k => k.RestaurantId)
                .IsRequired();
        }
    }
}
