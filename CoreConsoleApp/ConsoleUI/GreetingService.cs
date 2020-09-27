using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> logger;
        private readonly IConfiguration config;

        public GreetingService(ILogger<GreetingService> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        public void Greet()
        {
            for (int i = 0; i < config.GetValue<int>("LoopTimes"); i++)
            {
                logger.LogInformation("Greet number {runNumber}", i);
            }
        }
    }
}
