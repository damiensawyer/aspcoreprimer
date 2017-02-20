using Microsoft.Extensions.Configuration;

namespace firstcore
{
    public class Greeter : IGreeter
    {
        private readonly string _greeter;

        public Greeter(IConfiguration configuration)
        {
            this._greeter = configuration["Greeting"];
        }

        public string GetGreeting()
        {
            return _greeter;
        }
    }
}