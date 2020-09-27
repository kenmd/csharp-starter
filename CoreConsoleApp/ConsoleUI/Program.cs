using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            try
            {
                var provider = Config.Setup();
                Log.Information("Application Starting");

                var greetingService = provider.GetRequiredService<IGreetingService>();
                greetingService.Greet();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                throw;
            }
        }
    }
}
