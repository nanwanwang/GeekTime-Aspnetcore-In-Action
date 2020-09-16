using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SimpleDI2
{
    class Program
    {
        static void Main(string[] args)
        {
            var (engineType, engineScopeType) = ResolveTypes();
            var root = new ServiceCollection().BuildServiceProvider();
            var child1 = root.CreateScope().ServiceProvider;
            var child2 = root.CreateScope().ServiceProvider;
            var engine = GetEngine(root);
            var rootScope = GetRootScope(engine, engineType);

            //ServiceProviderEngine的唯一性
            Debug.Assert(ReferenceEquals(GetEngine(rootScope, engineScopeType), engine));
            Debug.Assert(ReferenceEquals(GetEngine(child1, engineScopeType), engine));
            Debug.Assert(ReferenceEquals(GetEngine(child2, engineScopeType), engine));

            //ServiceProviderEngine和IServiceScopeFactory的同一性
            Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceScopeFactory>(), engine));
            Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceScopeFactory>(), engine));
            Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceScopeFactory>(), engine));

            //ServiceProviderEngineScope提供的IServiceProvider是它自己
            //ServiceProvider提供的IServiceProvider是RootScope
            Debug.Assert(ReferenceEquals(root.GetRequiredService<IServiceProvider>(), rootScope));
            Debug.Assert(ReferenceEquals(child1.GetRequiredService<IServiceProvider>(), child1));
            Debug.Assert(ReferenceEquals(child2.GetRequiredService<IServiceProvider>(), child2));

            //ServiceProviderEngineScope和IServiceProvider的同一性
            Debug.Assert(ReferenceEquals((rootScope).ServiceProvider, rootScope));
            Debug.Assert(ReferenceEquals(((IServiceScope)child1).ServiceProvider, child1));
            Debug.Assert(ReferenceEquals(((IServiceScope)child2).ServiceProvider, child2));
        }


        static (Type Engine, Type EngineScope) ResolveTypes()
        {
            var assembly = typeof(ServiceProvider).Assembly;
            var engine = assembly.GetTypes().Single(it => it.Name == "IServiceProviderEngine");
            var engineScope = assembly.GetTypes().Single(it => it.Name == "ServiceProviderEngineScope");
            return (engine, engineScope);
        }

        static object GetEngine(ServiceProvider serviceProvider)
        {
            var field = typeof(ServiceProvider).GetField("_engine", BindingFlags.Instance | BindingFlags.NonPublic);
            return field.GetValue(serviceProvider);
        }

        static object GetEngine(object enginScope,Type engineScopeType)
        {
            var property = engineScopeType.GetProperty("Engine", BindingFlags.Instance | BindingFlags.Public);
            return property.GetValue(enginScope);
        }
        static IServiceScope GetRootScope(object engine, Type engineType)
        {
            var property = engineType.GetProperty("RootScope", BindingFlags.Instance | BindingFlags.Public);
            return (IServiceScope)property.GetValue(engine);
        }
    }

    
}
