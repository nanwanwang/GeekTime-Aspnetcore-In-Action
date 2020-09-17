using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class CacheKey
    {
        public MethodBase Method { get; }

        public object[] InputArguments { get; }

        public CacheKey(MethodBase method, object[] arguments)
        {
            this.Method = method;
            this.InputArguments = arguments;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CacheKey another))
            {
                return false;
            }

            if (!Method.Equals(another.Method))
            {
                return false;
            }

            for (int index = 0; index < InputArguments.Length; index++)
            {
                var argument1 = InputArguments[index];
                var argument2 = another.InputArguments[index];
                if(argument1==null&&argument2==null)
                {
                    continue;
                }

                if(argument1==null||argument2==null)
                {
                    return false;
                }

                if(!argument1.Equals(argument2))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = Method.GetHashCode();
            foreach (var argument in InputArguments)
            {
                hashCode ^= argument.GetHashCode();
            }
            return hashCode;
        }
    }
}
