#nullable disable
using Microsoft.EntityFrameworkCore;
using OrderManager.Infrastructure.EntityFramework.Models;

namespace OrderManager.Infrastructure.EntityFramework
{
    public class OrderManagerDbContext : DbContext
    {
        public OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options) : base(options)
        {

        }

        internal DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<SpecialOfferItem> SpecialOfferItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderManagerDbContext).Assembly);

    }
}
