using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThreePartDependencyInjection.Services;

namespace ThreePartDependencyInjection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }
        public ILifetimeScope AutofacContainer { get; private set; }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ÆÕÍ¨×¢²á
            //builder.RegisterType<MyService>().As<IMyService>().SingleInstance();
            //ÃüÃû×¢²á
            //builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");

            //ÊôÐÔ×¢Èë
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();

            //AOP
            //builder.RegisterType<MyInterceptor>();
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired().InterceptedBy(typeof(MyInterceptor)).EnableInterfaceInterceptors();


            // ×ÓÈÝÆ÷
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            this.AutofacContainer= app.ApplicationServices.GetAutofacRoot();


            //var service1 = AutofacContainer.Resolve<IMyService>();
            //var service2 = AutofacContainer.ResolveNamed<IMyService>("service2");
            //service1.ShowCode();
            //service2.ShowCode();

            using var scope = AutofacContainer.BeginLifetimeScope("myscope");
            var service0 = scope.Resolve<MyNameService>();
            using var subScope = scope.BeginLifetimeScope();
            var service1 = subScope.Resolve<MyNameService>();
            var service2 = subScope.Resolve<MyNameService>();
            Console.WriteLine($"service1=service2:{service1 == service2}");
            Console.WriteLine($"service1=service0:{service1==service0}");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
