using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionDemo
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

            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddTransient<IMyTransientService, MyTransientService>();
            services.AddScoped<IMyScopeService, MyScopeService>();


            services.AddSingleton<IOrderService>(new OrderService());
            //services.AddSingleton<IOrderService>(serviceProvider =>
            //{
            //    return new OrderServiceExt();
            //});

            //services.TryAddSingleton<IOrderService, OrderServiceExt>();

            //注册同一个服务的不同实现的时候 会生效
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceExt>());

            //移除服务
            //services.RemoveAll<IOrderService>();

            //替换注册的服务
            services.Replace(ServiceDescriptor.Transient<IOrderService, OrderServiceExt>());

            //泛型服务注册
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
