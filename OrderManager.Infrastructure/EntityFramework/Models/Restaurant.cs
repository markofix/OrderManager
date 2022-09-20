#nullable disable
namespace OrderManager.Infrastructure.EntityFramework.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeZoneOffset { get; set; }
    }
}
