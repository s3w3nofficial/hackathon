using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Shared.Models;
using XMLParser;

namespace Web.Server.Services
{
    public class AppState
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Article> UpVotedArticles { get; set; }
        public IEnumerable<Event> Events { get; set; }

        private readonly Data _client;
        public AppState(Data client)
        {
            _client = client;
        }

        public event Action OnArticlesUpdated;
        public event Action OnEventUpdated;

        public void UpdateArticles()
        {
            Articles = _client.GetArticles();
            if (UpVotedArticles != null)
            {
                foreach (var art in Articles)
                {
                    var upArt = UpVotedArticles.First(a => a.title == art.title);
                    if (upArt != null)
                    {
                        art.UpVoteCount = upArt.UpVoteCount;
                    }
                }

                UpVotedArticles = Articles;
            }
            else
            {
                UpVotedArticles = Articles;
            }

            OnArticlesUpdated();
        }

        public void UpdateEvents()
        {
            Events = _client.GetEvents();
            OnEventUpdated();
        }
    }
}
