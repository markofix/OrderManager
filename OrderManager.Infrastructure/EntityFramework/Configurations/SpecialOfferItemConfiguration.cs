using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class SpecialOfferItemConfiguration : IEntityTypeConfiguration<SpecialOfferItem>
    {
        public void Configure(EntityTypeBuilder<SpecialOfferItem> builder)
        {
            builder.ToTable("SpecialOffer");

            builder.HasKey(e => new { e.ProductId, e.SpecialOfferId });

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId)
                .IsRequired();

            builder.HasOne(e => e.SpecialOffer)
                .WithMany(e => e.SpecialOfferItems)
                .HasForeignKey(k => k.SpecialOfferId)
                .IsRequired();
        }
    }
}
