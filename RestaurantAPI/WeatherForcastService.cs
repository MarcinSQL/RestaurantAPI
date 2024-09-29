namespace RestaurantAPI
{
    public class WeatherForcastService : IWeatherForcastService
    {
        public IEnumerable<WeatherForecast> Get(int amount, int minTemperature, int maxTemperature)
        {
            return Enumerable.Range(amount, amount).Select(index => new WeatherForecast
            {
                ID = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTemperature, maxTemperature + 1),
            })
            .ToArray();
        }
    }
}
