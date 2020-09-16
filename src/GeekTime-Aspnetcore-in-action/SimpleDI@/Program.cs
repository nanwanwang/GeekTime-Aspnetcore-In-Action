using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace SimpleDI_
{
    class Program
    {
        static void Main(string[] args)
        {

            //ServiceLifetime();



            //DisposeService();


            CheckServiceScope();


        }



        static void ServiceLifetime()
        {
            var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(provider => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
            var provider1 = root.CreateScope().ServiceProvider;
            var provider2 = root.CreateScope().ServiceProvider;

            void GetServices<TService>(IServiceProvider provider)
            {
                provider.GetService<TService>();
                provider.GetService<TService>();
            }


            GetServices<IFoo>(provider1);
            GetServices<IBar>(provider1);
            GetServices<IBaz>(provider1);
            Console.WriteLine();

            GetServices<IFoo>(provider2);
            GetServices<IBar>(provider2);
            GetServices<IBaz>(provider2);
        }

        static void DisposeService()
        {
            using (var root = new ServiceCollection()
               .AddTransient<IFoo, Foo>()
               .AddScoped<IBar, Bar>()
               .AddSingleton<IBaz, Baz>()
               .BuildServiceProvider())
            {
                using (var scope = root.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    provider.GetService<IFoo>();
                    provider.GetService<IBar>();
                    provider.GetService<IBaz>();
                    Console.WriteLine("child container is disposed.");
                }

                Console.WriteLine("root contaoner is disposed.");
            }
        }

        static void CheckServiceScope()
        {
            var root = new ServiceCollection()
                .AddSingleton<IFoo2, Foo2>()
                .AddScoped<IBar2, Bar2>()
                .BuildServiceProvider(true);

            var child = root.CreateScope().ServiceProvider;

            void ResolveService<T>(IServiceProvider provider)
            {
                var isRootContainer = root == provider ? "yes" : "no";
                try
                {
                    provider.GetService<T>();
                    Console.WriteLine($"Status: Success; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; Service Type: {typeof(T).Name}; Root: {isRootContainer}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            ResolveService<IFoo2>(root);
            ResolveService<IBar2>(root);

            ResolveService<IFoo2>(child);
            ResolveService<IBar2>(child);
        }
    }

    public interface IFoo2 { }
    public interface IBar2 { }
    public class Foo2 : IFoo2
    {
        public IBar2 Bar2 { get; }
        public Foo2(IBar2 bar) => Bar2 = bar;
    }
    public class Bar2 : IBar2 { }
}
