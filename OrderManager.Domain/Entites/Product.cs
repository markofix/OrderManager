using OrderManager.Domain.Enums;

namespace OrderManager.Domain.Entites
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public abstract ProductType ProductType { get; }
    }
}
