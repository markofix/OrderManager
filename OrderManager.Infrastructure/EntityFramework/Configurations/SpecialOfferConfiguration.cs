using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class SpecialOfferConfiguration : IEntityTypeConfiguration<SpecialOffer>
    {
        public void Configure(EntityTypeBuilder<SpecialOffer> builder)
        {
            builder.ToTable("SpecialOffer");

            builder.HasKey(e => e.ProductId);

            builder.HasOne(e => e.Product)
                .WithMany(e => e.SpecialOffers)
                .HasForeignKey(k => k.ProductId)
                .IsRequired();
        }
    }
}
