using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class DefaultSystemClock : ISystemClock
    {
        private readonly IMemoryCache _cache;
        private readonly IOptions<MemoryCacheEntryOptions> _options;
        public DefaultSystemClock(IMemoryCache cache, IOptions<MemoryCacheEntryOptions> options)
        {
            _cache = cache;
            _options = options;
        }
        [CacheInterceptor]
        public DateTime GetCurrentTime(DateTimeKind dateTimeKind)
        {
            return dateTimeKind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;
        }
    }
}
