using OrderManager.Domain.Enums;

namespace OrderManager.Domain.Entites
{
    public class MainDish : Product
    {
        public MainDish(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override ProductType ProductType => ProductType.MainDish;
    }
}
