using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class MessageController : APIControllerBase
    {
        private readonly IMessageRepository messageRepository;
        private readonly ISessionService sessionService;

        public MessageController(IMessageRepository messageRepository, ISessionService sessionService)
        {
            this.messageRepository = messageRepository;
            this.sessionService = sessionService;
        }

        [HttpPost(Endpoints.Messages + "getpreviousmessages")]
        public async Task<ObjectResult> GetPreviousMessages(Requests.GetPreviouseMessages request) 
        {
            if (request == null)
            {
                return NoRequest();
            }

            string userId = sessionService.GetCurrentUserId();

            IEnumerable<Message> messages = await messageRepository.RetrieveMessage(userId, request.RecipientId);

            return Ok(messages);
        }
    }
}
