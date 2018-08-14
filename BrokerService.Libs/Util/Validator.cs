using BrokerService.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerService.Libs.Util
{
    static class Validator
    {
        static public List<PriceCandle> ContinuousDataCheck(List<PriceCandle> data)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i++].PriceTime == data[i].PriceTime)
                {
                    data.Remove(data[i]);
                }
            }

            return data;
        }
    }
}
