using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MusicMatch_Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task SendMessage(Message message)
        {
            await messageRepository.SaveMessage(message);
            await Clients.User(message.Recipient).SendAsync("ReceiveMessage", message);
        }
    }
}
