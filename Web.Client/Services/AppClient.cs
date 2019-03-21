using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Web.Shared.Models;

namespace Web.Client.Services
{
    public class AppClient
    {
        private readonly HttpClient _client;
        public AppClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _client.GetJsonAsync<IEnumerable<Article>>("/api/sampledata/articles");
        }
        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _client.GetJsonAsync<IEnumerable<Event>>("/api/sampledata/events");
        }
    }
}
