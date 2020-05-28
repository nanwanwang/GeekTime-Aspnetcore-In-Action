using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        [HttpGet]
        public int GetDisposableService(
            [FromServices] IHostApplicationLifetime hostApplicationLifetime,
            [FromQuery] bool stop)
        {
            //HttpContext.RequestServices 当前请求的根容器
            using (var scope = HttpContext.RequestServices.CreateScope()) 
            {
                var service = scope.ServiceProvider.GetService<IDisposableService>();

                var service2 = scope.ServiceProvider.GetService<IDisposableService>();

                Console.WriteLine("作用域执行结束");

            }
            if (stop)
            {
                hostApplicationLifetime.StopApplication();
            }


            Console.WriteLine("方法执行完成");
            return 2;
        }
    }
}
