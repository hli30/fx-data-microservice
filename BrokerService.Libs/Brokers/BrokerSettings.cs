using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Brokers
{
    abstract class BrokerSettings
    {
        public abstract string ApiKey { get; set; }
        public abstract string Endpoint { get; set; }
    }
}
