using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace ConsoleUI
{
    public static class Config
    {
        public static ServiceProvider Setup()
        {
            // setup json config
            var appConfig = CreateConfigBuilder().Build();

            // setup logger
            Serilog.Log.Logger = CreateLoggerConfig(appConfig).CreateLogger();

            // setup DI
            var provider = CreateServices(appConfig).BuildServiceProvider();

            return provider;
        }

        static IConfigurationBuilder CreateConfigBuilder()
        {
            var env = Environment.GetEnvironmentVariable("ENV");

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        static LoggerConfiguration CreateLoggerConfig(IConfigurationRoot appConfig)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(appConfig)
                .Enrich.FromLogContext()
                .WriteTo.Console();
        }

        static IServiceCollection CreateServices(IConfigurationRoot appConfig)
        {
            return new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog(dispose: true))
                .AddSingleton<IConfiguration>(serviceProvider => appConfig)
                .AddSingleton<IGreetingService, GreetingService>();
        }
    }
}
