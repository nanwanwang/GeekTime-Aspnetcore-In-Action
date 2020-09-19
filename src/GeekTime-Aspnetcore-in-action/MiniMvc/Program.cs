using System;
using System.Threading.Tasks;

namespace MiniMvc
{
    class Program
    {
        static async Task Main(string[] args)
        {
           await new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app
                 .Use(FooMiddleware)
                 .Use(BarMiddleware)
                 .Use(BazMiddleware))
                 .Build()
                 .StartAsync();
            Console.ReadKey();
        }

        public static RequestDelegate FooMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next)
            => async context =>
            {
                await context.Response.WriteAsync("Bar=>");
                await next(context);
            };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
            => context => context.Response.WriteAsync("Baz");
    }
}
