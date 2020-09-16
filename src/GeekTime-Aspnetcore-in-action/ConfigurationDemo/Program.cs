using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
            { "key1","value1"  },
            { "key2","value2"  },
            { "section1:key4","value3"  },
            {"section1:key6","value6" },
            {"section2:section3:key5","value5" }
            });

            IConfigurationRoot configurationRoot = builder.Build();

            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);

            IConfigurationSection section1 = configurationRoot.GetSection("section1");

            Console.WriteLine(section1["key4"]);
            Console.WriteLine(section1["key6"]);

            var section3 = configurationRoot.GetSection("section2").GetSection("section3");
            Console.WriteLine(section3["key5"]);


            Console.ReadKey();
        }
    }
}
