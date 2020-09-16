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
            #region ע�����

            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddTransient<IMyTransientService, MyTransientService>();
            services.AddScoped<IMyScopeService, MyScopeService>();


            services.AddSingleton<IOrderService>(new OrderService());
            //services.AddSingleton<IOrderService>(serviceProvider =>
            //{
            //    return new OrderServiceExt();
            //});

            //services.TryAddSingleton<IOrderService, OrderServiceExt>();

            //ע��ͬһ������Ĳ�ͬʵ�ֵ�ʱ�� ����Ч
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceExt>());

            //�Ƴ�����
            //services.RemoveAll<IOrderService>();

            //�滻ע��ķ���
            services.Replace(ServiceDescriptor.Transient<IOrderService, OrderServiceExt>());

            //���ͷ���ע��
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            #endregion


            //services.AddTransient<IDisposableService, DisposableService>();
            //services.AddScoped<IDisposableService, DisposableService>();
            //services.AddSingleton<IDisposableService, DisposableService>();

            //services.AddScoped<IDisposableService>(serviceProvider=>new DisposableService());

            //ע������ֱ�Ӵ�����(new)���� �������ͷŶ���
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

            //�Ӹ�������ȡ˲ʱ����
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
