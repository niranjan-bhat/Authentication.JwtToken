using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Blog_JwtTokenSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private ITokenManager _tokenManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITokenManager tm)
        {
            _logger = logger;
            _tokenManager = tm;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Token")]
        public IActionResult Auth()
        {
            //we could Check for valid username & password here.

           var token = _tokenManager.GenerateJwtToken();

            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        [Route("Secret")]
        public IActionResult Secret()
        {
            return Ok();
        }
    }
}
