using OrderManager.Domain.Entites;

namespace OrderManager.Application.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetRestaurantById(int restaurantId);
    }
}
