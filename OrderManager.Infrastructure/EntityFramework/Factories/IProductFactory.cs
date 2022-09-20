using OrderManager.Domain.Entites;

namespace OrderManager.Infrastructure.EntityFramework.Factories
{
    internal interface IProductFactory
    {
        Product BuildProductFromDbModel(Models.Product dbProduct);
    }
}
