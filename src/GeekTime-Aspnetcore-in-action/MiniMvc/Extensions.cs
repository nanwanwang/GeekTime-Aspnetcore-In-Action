using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMvc
{
    public static partial class Extensions
    {

        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
            => builder.UseServer(new HttpListenerServer(urls));

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
