using OrderManager.Domain.Enums;

namespace OrderManager.Domain.Entites
{
    public class Dessert : Product
    {
        public override ProductType ProductType => ProductType.Dessert;
    }
}
