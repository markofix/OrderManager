#nullable disable
namespace OrderManager.Infrastructure.EntityFramework.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
        public ICollection<SpecialOffer> SpecialOffers { get; set; }
    }
}
