namespace BrokerService.Libs.Brokers
{
    abstract class BrokerFactory
    {
        public abstract BrokerConnection GetBrokerConnection();
    }
}
