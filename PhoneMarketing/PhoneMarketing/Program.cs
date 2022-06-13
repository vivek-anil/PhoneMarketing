using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PhoneMarketing.Engine;
using PhoneMarketing.FileManager;
using PhoneMarketing.Services;

namespace PhoneMarketing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.json");
                         
            IConfiguration configuration = builder.Build();
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IFileService, CsvFileService>()
                .AddSingleton<IInputService, FileReader>()
                .AddSingleton<IProcessingEngine, ProcessingEngine>()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            

            var phoneNumbers = serviceProvider.GetService<IInputService>()?.GetData();
            if (phoneNumbers != null)
            {
                var result = serviceProvider.GetService<IProcessingEngine>()?.Process(phoneNumbers);

                if (result != null)
                {
                    foreach (var res in result)
                    {
                        Console.WriteLine($"{res.Key} === {res.Value}");
                    }
                }
            }
        }
    }
}
