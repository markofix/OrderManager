using Microsoft.EntityFrameworkCore;
using OrderManager.Application.Repositories;
using OrderManager.Domain.Entites;
using OrderManager.Infrastructure.EntityFramework.Factories;

namespace OrderManager.Infrastructure.EntityFramework.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly OrderManagerDbContext _dbContext;
        private readonly IProductFactory _productFactory;

        public ProductRepository(OrderManagerDbContext dbContext, IProductFactory productFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _productFactory = productFactory ?? throw new ArgumentNullException(nameof(productFactory));
        }

        public async Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<int> productIds)
        {
            var dbProducts = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .Include(p => p.SpecialOffers)
                    .ThenInclude(s => s.SpecialOfferItems)
                        .ThenInclude(item => item.Product)
                .ToListAsync();

            var products = new List<Product>();
            foreach (var dbProduct in dbProducts)
            {
                products.Add(_productFactory.BuildProductFromDbModel(dbProduct));
            }

            return products;
        }

        public async Task<IEnumerable<SpecialOffer>> GetSpecialOffersByProductIds(IEnumerable<int> productIds)
        {
            return await _dbContext.SpecialOffers
                .Where(x => productIds.Contains(x.ProductId))
                .Select(sp => new SpecialOffer(
                    sp.SpecialOfferItems.Select(specialOfferItem => _productFactory.BuildProductFromDbModel(specialOfferItem.Product)),
                    sp.Product.Name,
                    sp.DiscountPercentage)
                {
                    Id = sp.ProductId,
                })
                .ToListAsync();
        }
    }
}
