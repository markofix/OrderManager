using Microsoft.EntityFrameworkCore;
using OrderManager.Application.Repositories;
using OrderManager.Domain.Entites;

namespace OrderManager.Infrastructure.EntityFramework.Repositories
{
    internal class RestaurantRepository : IRestaurantRepository
    {
        private readonly OrderManagerDbContext _dbContext;

        public RestaurantRepository(OrderManagerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Restaurant?> GetRestaurantById(int restaurantId)
        {
            return await _dbContext.Restaurants
                .Where(r => r.Id == restaurantId)
                .Select(r => new Restaurant(r.Name, r.TimeZoneOffset)
                {
                    Id = r.Id,
                })
                .SingleOrDefaultAsync();
        }
    }
}
