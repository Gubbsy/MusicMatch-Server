using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstraction.Models;

namespace MusicMatch_Server.Hubs
{
    public class ChatHub: Hub
    {
        async Task SendMessge(Message message) 
        {
            await Clients.All.SendAsync("Recieved Message", message);
        }   
    }
}
