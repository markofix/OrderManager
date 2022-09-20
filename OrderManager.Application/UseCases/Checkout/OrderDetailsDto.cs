namespace OrderManager.Application.UseCases.Checkout
{
    public class CheckoutResponse
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
