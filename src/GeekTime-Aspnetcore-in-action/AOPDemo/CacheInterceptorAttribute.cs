using Dora.Interception;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class CacheInterceptorAttribute:InterceptorAttribute
    {
        public async Task InvokeAsync(InvocationContext context, IMemoryCache cache, IOptions<MemoryCacheEntryOptions> optionAccessor)
        {
            var key = new CacheKey(context.Method, context.Arguments);
            if(cache.TryGetValue(key,out object value))
            {
                context.ReturnValue = value;
            }
            else
            {
                await context.ProceedAsync();
                cache.Set(key, context.ReturnValue, optionAccessor.Value);
            }
        }

        public override void Use(IInterceptorChainBuilder builder)
        {
            builder.Use(this, Order);
        }

    }
}
