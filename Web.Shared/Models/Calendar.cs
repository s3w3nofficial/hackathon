using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Shared.Models
{
    public class Event
    {
        public string name { get; set; }
        public string anotation { get; set; }
        public string description { get; set; }
        public Place place { get; set; }
        public IEnumerable<string> photogalery { get; set; }
    }

    public class Place
    {
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
