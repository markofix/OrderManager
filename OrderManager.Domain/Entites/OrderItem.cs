using OrderManager.Domain.ValueObjects;

namespace OrderManager.Domain.Entites
{
    public class OrderItem
    {
        public OrderItem(Product product, Quantity quantity, decimal amount, decimal discountAmount)
        {
            Product = product;
            Quantity = quantity;
            Amount = amount;
            DiscountAmount = discountAmount;
        }

        public int Id { get; set; }
        public decimal Amount { get; }
        public decimal DiscountAmount { get; }
        public Product Product { get; }
        public Quantity Quantity { get; }
    }
}
