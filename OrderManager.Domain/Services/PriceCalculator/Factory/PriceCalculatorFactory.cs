namespace OrderManager.Domain.Services.PriceCalculator.Factory
{
    internal class PriceCalculatorFactory
    {
        private const int STARTING_HOUR_OF_HAPPY_HOUR = 13;
        private const int ENDING_HOUR_OF_HAPPY_HOUR = 15;

        public IPriceCalculator CreatePriceCalculator(DateTime restaurantLocalTime)
        {
            if (restaurantLocalTime.Hour >= STARTING_HOUR_OF_HAPPY_HOUR && restaurantLocalTime.Hour < ENDING_HOUR_OF_HAPPY_HOUR)
            {
                return new HappyHourPriceCalculator();
            }

            return new RegularPriceCalculator();
        }
    }
}
