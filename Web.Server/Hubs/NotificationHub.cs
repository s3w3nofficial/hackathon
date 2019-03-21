using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Web.Server.Services;

namespace Web.Server.Hubs
{
    public class NotificationHub : Hub
    {
        private IHubContext<NotificationHub> _hubContext;
        private readonly AppState _state;

        public NotificationHub(IHubContext<NotificationHub> hubContext, AppState state)
        {
            _hubContext = hubContext;
            _state = state;
        }
        public async Task OnArticleLiked(string title)
        {
            _state.UpVotedArticles.First(a => a.title == title).UpVoteCount += 1;
            await _hubContext.Clients.All.SendAsync("DataUpdated", "Articles");
        }
    }
}
