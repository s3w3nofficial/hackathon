using Web.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Web.Server.Hubs;
using Web.Server.Services;
using Web.Shared.Models;
using XMLParser;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public IEnumerable<Article> ArticleList = new List<Article>();
        public IEnumerable<Event> EventList = new List<Event>();

        private readonly AppState _state;
        private readonly IHubContext<NotificationHub> _hubContext;
        public SampleDataController(AppState state, IHubContext<NotificationHub> hubContext)
        {
            _state = state;
            _hubContext = hubContext;
            _state.OnArticlesUpdated += OnArticlesUpdated;
            _state.OnEventUpdated += OnEventsUpdated;
            ArticleList = _state.Articles;
            EventList = _state.Events;
        }

        [HttpGet("[action]")]
        public IEnumerable<Article> Articles()
        {
            return ArticleList;
        }

        [HttpGet("[action]")]
        public IEnumerable<Event> Events()
        {
            return _state.Events;
        }

        [HttpGet("[action]")]
        public void UpdateAllData()
        {
            _state.UpdateArticles();
            _state.UpdateEvents();
        }

        public async void OnArticlesUpdated()
        {
            ArticleList = _state.Articles;
            await _hubContext.Clients.All.SendAsync("DataUpdated", "Articles");
        }

        public async void OnEventsUpdated()
        {
            EventList = _state.Events;
            await _hubContext.Clients.All.SendAsync("DataUpdated", "Events");
        }
    }
}
