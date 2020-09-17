using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddMemoryCache()
                .AddInterception()
                .AddSingleton<ISystemClock, DefaultSystemClock>()
                .AddRouting()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {

            app
                 .UseRouting()
                 .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
