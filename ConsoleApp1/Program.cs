using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument data = XDocument.Parse(File.ReadAllText(@"aktuality.xml"));
            Console.WriteLine((from d in data.Root.Descendants("item") select d.Element("title").Value).First().ToString());
            Console.ReadKey();
        }
    }
}
