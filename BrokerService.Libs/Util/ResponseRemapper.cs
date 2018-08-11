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
                        PriceCandle priceCandle = new PriceCandle();

                        priceCandle.Granularity = candleJson.Granularity;
                        priceCandle.Instrument = candleJson.Instrument;
                        priceCandle.PriceTime = DateTime.Parse(candle.Time).ToUniversalTime();
                        priceCandle.Volume = candle.Volume;

                        priceCandle.BidOpen = decimal.Parse(candle.Bid.Open);
                        priceCandle.BidHigh = decimal.Parse(candle.Bid.High);
                        priceCandle.BidLow = decimal.Parse(candle.Bid.Low);
                        priceCandle.BidClose = decimal.Parse(candle.Bid.Close);

                        priceCandle.AskOpen = decimal.Parse(candle.Ask.Open);
                        priceCandle.AskHigh = decimal.Parse(candle.Ask.High);
                        priceCandle.AskLow = decimal.Parse(candle.Ask.Low);
                        priceCandle.AskClose = decimal.Parse(candle.Ask.Close);

                        candleList.Add(priceCandle);
                    }
                }
            }

            return candleList;
        }
    }
}
