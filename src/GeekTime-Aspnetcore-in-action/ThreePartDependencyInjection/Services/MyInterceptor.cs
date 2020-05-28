using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreePartDependencyInjection.Services
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Interceptor before,Method:{invocation.Method.Name}");

            invocation.Proceed();

            Console.WriteLine($"Interceptor after,Method:{invocation.Method.Name}");
        }
    }
}
