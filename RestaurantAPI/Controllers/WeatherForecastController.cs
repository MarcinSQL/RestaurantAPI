using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForcastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForcastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]

        public IEnumerable<WeatherForecast> Get([FromQuery]int amount, [FromQuery]int minTemperature, [FromQuery]int maxTemperature)
        {
            var result = _service.Get(amount, minTemperature, maxTemperature);
            return result;
        }

        //mapowanie parametrow
        //[HttpGet("currentDay/{max}")]
        //public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        //{
        //    var result = _service.Get();
        //    return "result";
        //}

        //wyswietlanie zapytanie Post
        [HttpPost]
        public ActionResult<string> Hello([FromBody]string name)
        {
            return StatusCode(200, $"Hello {name}");
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int amount, [FromBody]TemperatureRequest request)
        {
            if (amount > 0 && request.maxTemperature > request.minTemperature)
            {
                var result = _service.Get(amount, request.minTemperature, request.maxTemperature);
                return Ok(result);
            }
            else
            {
                return BadRequest("Warunki nie spe³nione");
            }
        }
    }
}
