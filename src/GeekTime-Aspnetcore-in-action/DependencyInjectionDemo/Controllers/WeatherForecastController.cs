using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public int GetService([FromServices]IMySingletonService mySingletonService1,
                              [FromServices]IMySingletonService mySingletonService2,
                              [FromServices]IMyTransientService myTransientService1,
                              [FromServices]IMyTransientService myTransientService2,
                              [FromServices]IMyScopeService myScopeService1,
                              [FromServices]IMyScopeService myScopeService2)
        {

            Console.WriteLine($"mySingleton1:{mySingletonService1.GetHashCode()}");
            Console.WriteLine($"mySingleton2:{mySingletonService2.GetHashCode()}");
            Console.WriteLine($"myTransient1:{myTransientService1.GetHashCode()}");
            Console.WriteLine($"myTransient2:{myTransientService2.GetHashCode()}");
            Console.WriteLine($"myScoped1:{myScopeService1.GetHashCode()}");
            Console.WriteLine($"myScoped2:{myScopeService2.GetHashCode()}");

            

            return 0;
        }

        [HttpGet]
        public int GetServiceList([FromServices]IEnumerable<IOrderService> orderServices)
        {
            foreach (var item in orderServices)
            {
                Console.WriteLine($"{item.ToString()} => {item.GetHashCode()}");
            }
            return 0;
        }

        [HttpGet]
        public int GetGenericService([FromServices] IGenericService<IOrderService> genericService)
        {
            

            return 1;
        }
    }
}
