using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniMvc
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
