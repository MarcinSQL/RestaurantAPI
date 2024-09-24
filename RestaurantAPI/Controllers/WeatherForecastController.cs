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

        public IEnumerable<WeatherForecast> Get()
        {
            var result = _service.Get();
            return result;
        }

        //mapowanie parametrow
        [HttpGet("currentDay/{max}")]
        public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        {
            var result = _service.Get();
            return result;
        }

        //wyswietlanie zapytanie Post
        [HttpPost]
        public ActionResult<string> Hello([FromBody]string name)
        {
            //ZAPYTAC MATEUSZA DLACZEGO NIE WYSWIETLA 401
            //HttpContext.Response.StatusCode = 401;

            //return StatusCode(401, $"Hello {name}");

            return NotFound($"Error");
        }
    }
}
