using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Web.Shared.Models;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Data();
            var events = data.GetEvents();
            foreach (var eve in events)
            {
                Console.WriteLine(eve.name);   
            }

            Console.ReadKey();
        }
    }

    public class Data
    {
        private XDocument data;
        private XDocument dataCalendar;
        public List<Article> GetArticles()
        {
            var m_strFilePath = "http://www.kr-kralovehradecky.cz/rss/aktuality.xml";
            string xmlStr;
            using (var wc = new WebClient())
            {
                xmlStr = wc.DownloadString(m_strFilePath);
            }

            data = XDocument.Parse(xmlStr);

            List<Article> articles = new List<Article>();
            foreach (var art in data.Root.Descendants("item"))
            {
                articles.Add(new Article
                {
                    title = art.Element("title").Value,
                    author = art.Element("author").Value,
                    category = art.Element("category").Value,
                    description = art.Element("description").Value,
                    link = art.Element("link").Value,
                    pubDate = DateTime.Parse(art.Element("pubDate").Value)
                });
            }

            return articles;
        }

        public List<Event> GetEvents()
        {
            var url = "http://www.hkregion.cz/bs/data_repository/export/events.php?nomenclatures=show";
            string xmlStr;
            using (var wc = new WebClient())
            {
                xmlStr = wc.DownloadString(url);
            }

            dataCalendar = XDocument.Parse(xmlStr);
            List<Event> events = new List<Event>();
            foreach (var eve in dataCalendar.Root.Descendants("event").Take(10))
            {
                DateTime start;
                DateTime end;
                if (DateTime.TryParse(eve.Descendants("date_time").First().Element("start").Value,out start))
                {
                    start = DateTime.Parse(eve.Descendants("date_time").First().Element("start").Value);
                }

                if (DateTime.TryParse(eve.Descendants("date_time").First().Element("end").Value, out end))
                {
                    end = DateTime.Parse(eve.Descendants("date_time").First().Element("end").Value);
                }

                try
                {
                    events.Add(new Event
                    {
                        name = eve.Element("name").Value,
                        description = eve.Element("description").Value,
                        anotation = eve.Element("anotation").Value,
                        place = new Place
                        {
                            start = start,
                            end = end,
                            name = eve.Descendants("place").First().Element("name_place").Value
                        },
                        photogalery = eve.Descendants("photogalery").Select(i => i.Element("image_thumb").Value)

                    });
                } catch { }
            }

            return events;
        }
    }
}
