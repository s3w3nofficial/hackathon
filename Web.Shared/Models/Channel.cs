using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Shared.Models
{
    public class Channel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string managingEditor { get; set; }
        public DateTime pubDate { get; set; }
    }
}
