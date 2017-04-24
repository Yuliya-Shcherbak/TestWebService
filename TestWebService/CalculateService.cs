using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TestWebService
{
    public class CalculateService
    {
        /// <summary>
        /// Method calculate average for each currency first and then average between sources
        /// </summary>
        public static string CalculateMode1(DateTime start, DateTime end)
        {
            var firstSource = DataReader.readXML(start, end);
            var secondSource = DataReader.readCSV(start, end);

            List<CurrencyModel> list = new List<CurrencyModel>();

            foreach (var rate in firstSource)
            {
                var _rate = (rate.Value.Average() + secondSource[rate.Key].Average()) / 2;
                list.Add(new CurrencyModel
                {
                    Currency = rate.Key,
                    Rate = Math.Round(_rate, 2)
                });
            }

            var json = new JavaScriptSerializer().Serialize(list);

            return json;
        }

        /// <summary>
        /// Method calculate average for all rates of the sources
        /// </summary>
        public static string CalculateMode2(DateTime start, DateTime end)
        {
            var firstSource = DataReader.readXML(start, end);
            var secondSource = DataReader.readCSV(start, end);
            List<CurrencyModel> list = new List<CurrencyModel>();
            foreach (var rate in firstSource)
            {
                rate.Value.AddRange(secondSource[rate.Key]);
                list.Add(new CurrencyModel
                {
                    Currency = rate.Key,
                    Rate = Math.Round(rate.Value.Average(), 2)
                });
            }

            var json = new JavaScriptSerializer().Serialize(list);

            return json;
        }
    }
}