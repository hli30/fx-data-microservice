using BrokerService.Database.Models;
using BrokerService.Libs.Brokers.Oanda.JsonResponse;
using System;
using System.Collections.Generic;

namespace BrokerService.Libs.Util
{
    class ResponseRemapper
    {
        public List<PriceCandle> RemapResponseToDb(object jsonObj)
        {
            List<PriceCandle> candleList = new List<PriceCandle>();

            if (jsonObj is CandleJson candleJson)
            {
                foreach (var candle in candleJson.Candles)
                {
                    if (candle.Complete == true)
                    {
                        PriceCandle priceCandle = new PriceCandle
                        {
                            Granularity = candleJson.Granularity,
                            Instrument = candleJson.Instrument,
                            PriceTime = DateTime.Parse(candle.Time).ToUniversalTime(),
                            Volume = candle.Volume,

                            BidOpen = decimal.Parse(candle.Bid.Open),
                            BidHigh = decimal.Parse(candle.Bid.High),
                            BidLow = decimal.Parse(candle.Bid.Low),
                            BidClose = decimal.Parse(candle.Bid.Close),

                            AskOpen = decimal.Parse(candle.Ask.Open),
                            AskHigh = decimal.Parse(candle.Ask.High),
                            AskLow = decimal.Parse(candle.Ask.Low),
                            AskClose = decimal.Parse(candle.Ask.Close)
                        };

                        candleList.Add(priceCandle);
                    }
                }
            }

            return candleList;
        }
    }
}
