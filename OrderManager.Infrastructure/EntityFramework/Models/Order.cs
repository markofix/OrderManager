#nullable disable
namespace OrderManager.Infrastructure.EntityFramework.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? CompletedOnUtc { get; set; }
        public byte OrderStatusId { get; set; }
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
