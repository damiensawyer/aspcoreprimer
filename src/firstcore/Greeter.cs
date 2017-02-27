using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace firstcore
{
    public class Greeter : IGreeter
    {
        private readonly string _greeter;
        private ILogger<Greeter> _logger;

        public Greeter(IConfiguration configuration, ILogger<Greeter> logger)
        {
            this._logger = logger;
            this._greeter = configuration["Greeting"];
        }

        public string GetGreeting()
        {
            this._logger.LogTrace($"getting message {_greeter}");
            return $"hello from the {_greeter}";
        }
    }
}