using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Abstraction.Models;

namespace MusicMatch_Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.User(message.UserId).SendAsync("ReceiveMessage", message);
        }
    }
}
