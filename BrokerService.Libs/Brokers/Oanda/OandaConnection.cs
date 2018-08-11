using BrokerService.Database.Models;
using BrokerService.Libs.Brokers.Oanda.JsonResponse;
using BrokerService.Libs.Util;
using JsonPrettyPrinterPlus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BrokerService.Libs.Brokers.Oanda
{
    class OandaConnection : BrokerConnection
    {
        private readonly string _apiKey;
        private readonly string _endpoint;

        public OandaConnection(string mode, IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("Oanda")["ApiKey"];

            if (mode == "Practice")
            {
                _endpoint = configuration.GetSection("Oanda")["Endpoint:Practice"];
            }
            else
            {
                _endpoint = configuration.GetSection("Oanda")["Endpoint:Live"];
            }
        }

        private RestRequest SetupGetRequest(string fxPair)
        {
            var request = new RestRequest("/instruments/{fxPair}/candles", Method.GET);

            request.AddUrlSegment("fxPair", fxPair);

            request.AddHeader("Accept-Datetime-Format", "RFC3339");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            request.AddParameter("price", "BA");

            return request;
        }

        public override List<PriceCandle> FetchCandles(string granularity, DateTime from)
        {
            var client = new RestClient(_endpoint);
            var request = SetupGetRequest("EUR_USD"); //need to refactor into using all fxpairs

            DateTime now = DateTime.UtcNow;

            //Converting DateTime to RFC3339 format as required by Oanda API
            string utcTo = XmlConvert.ToString(now, XmlDateTimeSerializationMode.Utc);
            string utcFrom = XmlConvert.ToString(from, XmlDateTimeSerializationMode.Utc);

            request.AddParameter("granularity", granularity);
            request.AddParameter("from", utcFrom);
            request.AddParameter("to", utcTo);

            IRestResponse response = client.Execute(request);

            string jsonRes = response.Content;
            Console.WriteLine($"RECEIVED DATA:{jsonRes.PrettyPrintJson()}");

            CandleJson jsonObj = JsonConvert.DeserializeObject<CandleJson>(jsonRes);

            var remapper = new ResponseRemapper();

            List<PriceCandle> priceCandle = remapper.RemapResponseToDb(jsonObj);

            return priceCandle;
        }
    }
}
