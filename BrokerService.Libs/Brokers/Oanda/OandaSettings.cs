using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Brokers.Oanda
{
    class OandaSettings : BrokerSettings
    {
        public override string ApiKey { get; set; }
        public override string Endpoint { get; set; }

        public OandaSettings(string mode, IConfiguration configuration)
        {
            ApiKey = configuration.GetSection("Oanda")["ApiKey"];

            if (mode == "Practice")
            {
                Endpoint = configuration.GetSection("Oanda")["Endpoint:Practice"];
            }
            else
            {
                Endpoint = configuration.GetSection("Oanda")["Endpoint:Live"];
            }
        }



    }
}
