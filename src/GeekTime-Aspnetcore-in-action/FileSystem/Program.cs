using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //ShowStructure();

            ReadEmbeddedFile();

            ReadEmbeddedFileByEmbeddedProvider();
        }

        static void ShowStructure()
        {
            static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
            new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"F:\GeekTime-Aspnetcore-In-Action\src\GeekTime-Aspnetcore-in-action\FileSystem\bin\Debug\netcoreapp3.1\test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetRequiredService<IFileManager>()
                .ShowStructure(Print);
        }

        static void  ReadEmbeddedFile()
        {
            var assembly = typeof(Program).Assembly;
            var embeddedResources=assembly.GetManifestResourceNames();
            Debug.Assert(embeddedResources.Contains("FileSystem.test.dir1.foobar.bar.txt"));
            Debug.Assert(embeddedResources.Contains("FileSystem.test.dir1.foobar.foo.txt"));
            Debug.Assert(embeddedResources.Contains("FileSystem.test.dir1.baz.txt"));
            Debug.Assert(embeddedResources.Contains("FileSystem.test.dir2.qux.txt"));

        }
        static void ReadEmbeddedFileByEmbeddedProvider()
        {
           var directories=  new ServiceCollection().AddSingleton<IFileProvider>(new EmbeddedFileProvider(typeof(Program).Assembly))
               .BuildServiceProvider()
               .GetService<IFileProvider>().GetDirectoryContents("/");

            using (var stream = directories.First().CreateReadStream())
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Console.WriteLine(Encoding.Default.GetString(buffer));
            }
          

        }

    }
}
