using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDI
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class InjectionAttribute:Attribute
    {
    }
}
