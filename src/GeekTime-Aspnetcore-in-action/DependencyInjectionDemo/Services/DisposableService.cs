using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{

    public interface IDisposableService { }


    public class DisposableService : IDisposableService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableService disposed :{this.GetHashCode()}");
        }
    }
}
