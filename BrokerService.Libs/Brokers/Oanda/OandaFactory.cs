using Microsoft.Extensions.Configuration;

namespace BrokerService.Libs.Brokers.Oanda
{
    class OandaFactory : BrokerFactory
    {
        private string _mode;
        private IConfiguration _configuration;

        public OandaFactory(string mode, IConfiguration configuration)
        {
            _configuration = configuration;
            _mode = mode;
        }

        public override BrokerConnection GetBrokerConnection()
        {
            return new OandaConnection(_mode, _configuration);
        }
    }
}
