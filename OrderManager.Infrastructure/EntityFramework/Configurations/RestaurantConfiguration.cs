using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
               .UseIdentityColumn();
        }
    }
}
