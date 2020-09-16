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
            #region 注册服务

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
            #endregion


            //services.AddTransient<IDisposableService, DisposableService>();
            //services.AddScoped<IDisposableService, DisposableService>();
            //services.AddSingleton<IDisposableService, DisposableService>();

            //services.AddScoped<IDisposableService>(serviceProvider=>new DisposableService());

            //注册我们直接创建的(new)对象 容器不释放对象
            //services.AddSingleton<IDisposableService>(new DisposableService()); 

            services.AddTransient<IDisposableService>(serviceProvider => new DisposableService());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //从根容器获取瞬时服务
            var services= app.ApplicationServices.GetService<IDisposableService>();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
