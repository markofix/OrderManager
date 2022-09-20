namespace OrderManager.Domain.Entites
{
    public class Restaurant
    {
        public Restaurant(string name, int timeZoneOffset)
        {
            Name = name;
            TimeZoneOffset = timeZoneOffset;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeZoneOffset { get; }
    }
}
