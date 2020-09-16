using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
            new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"F:\GeekTime-Aspnetcore-In-Action\src\GeekTime-Aspnetcore-in-action\FileSystem\bin\Debug\netcoreapp3.1\test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetRequiredService<IFileManager>()
                .ShowStructure(Print);
        }
    }
}
