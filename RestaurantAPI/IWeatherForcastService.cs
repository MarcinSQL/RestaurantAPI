
namespace RestaurantAPI
{
    public interface IWeatherForcastService
    {
        IEnumerable<WeatherForecast> Get(int amount, int minTemperature, int maxTemperature);
    }
}