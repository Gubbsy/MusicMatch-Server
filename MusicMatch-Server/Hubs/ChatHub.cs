using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Abstraction.Models;

namespace MusicMatch_Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
