using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Services;
using Microsoft.JSInterop;
using Web.Shared.Models;

namespace Web.Client.Services
{
    public class AppState
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Event> Events { get; set; }

        private readonly AppClient _client;
        private static AppState _state;
        public AppState(AppClient client)
        {
            _client = client;
            _state = this;
            Articles = new List<Article>();
            Events = new List<Event>();
            UpdateArticles();
            UpdateEvents();
        }

        public event Action OnArticlesUpdated;
        public event Action OnEventsUpdated;

        public async void UpdateArticles()
        {
            Articles = await _client.GetArticlesAsync();
            OnArticlesUpdated();
        }

        public async void UpdateEvents()
        {
            Events = await _client.GetEventsAsync();
            OnEventsUpdated();
        }

        [JSInvokableAttribute]
        public static void UpdateArticlesStatic()
        {
            _state.UpdateArticles();
        }

        [JSInvokableAttribute]
        public static void UpdateEventsStatic()
        {
            _state.UpdateEvents();
        }
    }
}
