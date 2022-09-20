namespace OrderManager.Application.UseCases.GetOrderDetails
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
