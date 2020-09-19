using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMvc
{
    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;
        public WebHost(IServer server,RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }
        public async Task StartAsync()
        {
           await  _server.StartAsync(_handler);
        }
    }
}
