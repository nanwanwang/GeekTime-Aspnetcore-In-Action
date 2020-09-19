using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMvc
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);

        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);

        IWebHost Build();
    }
}
