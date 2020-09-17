using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo
{
    public interface ISystemClock
    {
        DateTime GetCurrentTime(DateTimeKind dateTimeKind);
    }
}
