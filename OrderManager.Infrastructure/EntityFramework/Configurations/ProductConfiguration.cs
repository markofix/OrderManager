using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(e => e.ProductType)
                .WithMany()
                .HasForeignKey(k => k.ProductTypeId);
        }
    }
}
