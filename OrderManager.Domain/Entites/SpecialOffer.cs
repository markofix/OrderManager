using OrderManager.Domain.Enums;

namespace OrderManager.Domain.Entites
{
    public class SpecialOffer : Product
    {
        public SpecialOffer(IEnumerable<Product> products, string name, decimal discountPercentage)
        {
            Products = products;
            Name = name;
            DiscountPercentage = discountPercentage;
            Price = CalculatePrice();
            DiscountAmount = CalculateDiscountAmount();
            PriceWithDiscount = Price - DiscountAmount;
        }

        public decimal DiscountAmount { get; }
        public decimal DiscountPercentage { get; }
        public decimal PriceWithDiscount { get; }
        public IEnumerable<Product> Products { get; }
        public override ProductType ProductType => ProductType.SpecialOffer;

        private decimal CalculatePrice()
        {
            return Products.Sum(p => p.Price);
        }

        private decimal CalculateDiscountAmount()
        {
            return Price * DiscountPercentage / 100;
        }
    }
}
