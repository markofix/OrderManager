using OrderManager.Domain.Entites;

namespace OrderManager.Application.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<int> productIds);
        Task<IEnumerable<SpecialOffer>> GetSpecialOffersByProductIds(IEnumerable<int> productIds);
    }
}
