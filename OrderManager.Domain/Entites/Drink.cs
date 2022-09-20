using OrderManager.Domain.Enums;

namespace OrderManager.Domain.Entites
{
    public class Drink : Product
    {
        public override ProductType ProductType => ProductType.Drink;
    }
}
