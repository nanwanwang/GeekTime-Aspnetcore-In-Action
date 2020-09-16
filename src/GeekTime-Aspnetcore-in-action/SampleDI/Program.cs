using System;

namespace SampleDI
{
    //https://www.cnblogs.com/artech/p/asp-net-core-di-register.html
    class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();
            cat.Register(typeof(IFoo),typeof(Foo));
            cat.Register(typeof(IBar), typeof(Bar));
            cat.Register(typeof(IBaz), typeof(Baz));
            cat.Register(typeof(IQux), typeof(Qux));

            IFoo service = (IFoo)cat.GetService(typeof(IFoo));
            Foo foo = (Foo)service;
            Baz baz = (Baz)foo.Baz;

            Console.WriteLine("cat.GetService<IFoo>(): {0}", service);
            Console.WriteLine("cat.GetService<IFoo>().Bar: {0}", foo.Bar);
            Console.WriteLine("cat.GetService<IFoo>().Baz: {0}", foo.Baz);
            Console.WriteLine("cat.GetService<IFoo>().Baz.Qux: {0}", baz.Qux);
        }

        public interface IFoo { }
        public interface IBar { }
        public interface IBaz { }
        public interface IQux { }

        public class Foo : IFoo
        {
            public IBar Bar { get; private set; }

            [Injection]
            public IBaz Baz { get; set; }

            public Foo() { }

            [Injection]
            public Foo(IBar bar)
            {
                this.Bar = bar;
            }
        }

        public class Bar : IBar { }

        public class Baz : IBaz
        {
            public IQux Qux { get; private set; }

            [Injection]
            public void Initialize(IQux qux)
            {
                this.Qux = qux;
            }
        }

        public class Qux : IQux { }
    }
}
