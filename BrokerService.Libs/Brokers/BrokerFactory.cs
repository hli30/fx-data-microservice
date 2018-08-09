using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Brokers
{
    abstract class BrokerFactory
    {
        public abstract BrokerConnection GetBrokerConnection();
    }
}
