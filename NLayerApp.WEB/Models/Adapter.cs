using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NLayerApp.WEB.Models
{

    public class Report
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Worker { get; set; }
        public int O3 { get; set; }
        public int NO2 { get; set; }
        public int SO2 { get; set; }
    }

    public static class ManufacturerDataProvider
    {
        static public List<Report> GetData() =>
           new List<Report>
           {
            new Report { Id = 1, Date = "01.02.2019", City = "Kyiv", Worker = "JohnUolker@gmail.com",
                O3 = 220, NO2 = 160 , SO2 = 16 },
            new Report { Id = 2, Date = "20.02.1029", City = "Kherson", Worker = "OlegButin@gmail.com",
                O3 = 200, NO2 = 100 , SO2 = 89 },
            new Report { Id = 3, Date = "16.04.2019", City = "Poltava", Worker = "AnnaSavina@gmail.com",
                O3 = 250, NO2 = 89 , SO2 = 65 },
            new Report { Id = 4, Date = "18.03.2019", City = "Lviv", Worker = "OrnestLutyi@gmail.com",
                O3 = 180, NO2 = 96 , SO2 = 91 },
            new Report { Id = 5, Date = "19.01.2019", City = "Odessa", Worker = "TomHalper@gmail.com",
                O3 = 163, NO2 = 120 , SO2 = 101 }
           };
    }

    public class XmlConverter
    {
        public XDocument GetXML()
        {
            var xDocument = new XDocument();
            var xElement = new XElement("Reports");
            var xAttributes = ManufacturerDataProvider.GetData()
                .Select(m => new XElement("Report",
                                    new XAttribute("Id", m.Id),
                                    new XAttribute("Date", m.Date),
                                    new XAttribute("City", m.City),
                                    new XAttribute("Worker", m.Worker),
                                    new XAttribute("O3", m.O3),
                                    new XAttribute("NO2", m.NO2),
                                    new XAttribute("SO2", m.SO2)));

            xElement.Add(xAttributes);
            xDocument.Add(xElement);

            Console.WriteLine(xDocument);

            return xDocument;
        }
    }

    public class JsonConverter
    {
        private IEnumerable<Report> _manufacturers;

        public JsonConverter(IEnumerable<Report> manufacturers)
        {
            _manufacturers = manufacturers;
        }

        public void ConvertToJson()
        {
            var jsonManufacturers = JsonConvert.SerializeObject(_manufacturers, Formatting.Indented);

            Console.WriteLine("\nPrinting JSON list\n");
            Console.WriteLine(jsonManufacturers);
        }
    }


    public interface IXmlToJson
    {
        void ConvertXmlToJson();
    }


    public class XmlToJsonAdapter : IXmlToJson
    {
        private readonly XmlConverter _xmlConverter;

        public XmlToJsonAdapter(XmlConverter xmlConverter)
        {
            _xmlConverter = xmlConverter;
        }

        public void ConvertXmlToJson()
        {
            var manufacturers = _xmlConverter.GetXML()
                    .Element("Reports")
                    .Elements("Report")
                    .Select(m => new Report
                    {
                        Id = Convert.ToInt32(m.Attribute("Id").Value),
                        Date = m.Attribute("Date").Value,
                        City = m.Attribute("City").Value,
                        Worker = m.Attribute("Worker").Value,
                        O3 = Convert.ToInt32(m.Attribute("O3").Value),
                        NO2 = Convert.ToInt32(m.Attribute("NO2").Value),
                        SO2 = Convert.ToInt32(m.Attribute("SO2").Value)
                    });

            new JsonConverter(manufacturers)
                .ConvertToJson();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var xmlConverter = new XmlConverter();
            var adapter = new XmlToJsonAdapter(xmlConverter);
            adapter.ConvertXmlToJson();
            Console.ReadLine();
        }
    }
}