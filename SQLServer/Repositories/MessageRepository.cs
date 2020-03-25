using Abstraction.Models;
using Abstraction.Repositories;
using SQLServer.Exceptions;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SQLServer.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext appDbContext;

        public MessageRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Message>> RetrieveMessage(string userId, string recipientId)
        {
            try
            {
                return await appDbContext.Messages
                    .Where(m => (m.Sender == userId && m.Recipient == recipientId) || (m.Recipient == userId && m.Sender == recipientId))
                    .OrderBy(m => m.Date)
                    .ToListAsync(); 
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retirve previouse messages", e);
            }
        }

        public async Task SaveMessage(Message message)
        {
            try
            {
                MessageDbo newMessage = new MessageDbo { 
                    Sender = message.Sender,
                    Recipient = message.Recipient,
                    Msg = message.Msg,
                    Date = message.Date
                };
                appDbContext.Messages.Add(newMessage);
                await appDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException(e.Message, e);
            }
        }
    }
}
