namespace OrderManager.Domain.Dates
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow();
    }
}
