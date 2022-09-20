#nullable disable
namespace OrderManager.Infrastructure.EntityFramework.Models
{
    public class SpecialOfferItem
    {
        public int ProductId { get; set; }
        public int SpecialOfferId { get; set; }

        public Product Product { get; set; }
        public SpecialOffer SpecialOffer { get; set; }
    }
}
