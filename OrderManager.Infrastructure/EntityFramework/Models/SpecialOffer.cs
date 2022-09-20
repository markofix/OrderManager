#nullable disable
namespace OrderManager.Infrastructure.EntityFramework.Models
{
    public class SpecialOffer
    {
        public int ProductId { get; set; }
        public decimal DiscountPercentage { get; set; }

        public Product Product { get; set; }
        public ICollection<SpecialOfferItem> SpecialOfferItems { get; set; }
    }
}
