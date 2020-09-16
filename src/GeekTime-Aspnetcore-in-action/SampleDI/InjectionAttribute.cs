using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDI
{
    [AttributeUsage(AttributeTargets.Constructor |
                      AttributeTargets.Property |
                      AttributeTargets.Method,
                      AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}
