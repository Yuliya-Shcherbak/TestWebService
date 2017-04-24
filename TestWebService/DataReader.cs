using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Globalization;
using System.IO;

namespace TestWebService
{
    public class DataReader
    {
        public static Dictionary<string, List<double>> readCSV(DateTime StartDate, DateTime EndDate)
        {
            Dictionary<string, List<double>> rates = new Dictionary<string, List<double>>();

            NumberFormatInfo provider = new NumberFormatInfo();
            var lines = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/CSVRates.csv"));
            foreach (var l in lines)
            {
                var arr = l.Split(',');

                if (arr != null)
                {
                    DateTime date = DateTime.ParseExact(arr[1].Remove(0, 1), "yyyyMMdd", null);
                    if (date >= StartDate && date <= EndDate)
                    {
                        if (rates.ContainsKey(arr[0]))
                        {
                            rates[arr[0]].Add(Convert.ToDouble(arr[2].Remove(0, 1), provider));
                        }
                        else
                        {
                            List<double> list = new List<double> { Convert.ToDouble(arr[2].Remove(0, 1), provider) };
                            rates.Add(arr[0], list);
                        }
                    }
                }
            }
            return rates;
        }

        public static Dictionary<string, List<double>> readXML(DateTime StartDate, DateTime EndDate)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/XmlRates.xml"));
            XmlElement xRoot = xDoc.DocumentElement;

            Dictionary<string, List<double>> rates = new Dictionary<string, List<double>>();

            foreach (XmlNode node in xRoot)
            {
                XmlNode attr = node.Attributes.GetNamedItem("code");
                if (attr != null)
                {
                    foreach (XmlNode rate in node.ChildNodes)
                    {
                        DateTime date = DateTime.ParseExact(rate.Attributes.GetNamedItem("date").Value, "yyyyMMdd", null);
                        if (date >= StartDate && date <= EndDate)
                        {
                            if (rates.ContainsKey(attr.Value))
                            {
                                rates[attr.Value].Add(Convert.ToDouble(rate.Attributes.GetNamedItem("rate").Value, provider));
                            }
                            else
                            {
                                List<double> list = new List<double> { Convert.ToDouble(rate.Attributes.GetNamedItem("rate").Value, provider) };
                                rates.Add(attr.Value, list);
                            }
                        }
                    }
                }
            }
            return rates;
        }
    }
}