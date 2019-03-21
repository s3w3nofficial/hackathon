using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Web.Shared.Models
{
    public class Article
    {
        //public string guid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public DateTime pubDate { get; set; }
        public int UpVoteCount { get; set; }
        public bool IsVottingEnabled { get; set; } = true;
    }
}
