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
    class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext appDbContext;

        public MessageRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Message>> RetrieveMessage(string userId)
        {
            try
            {
                return await appDbContext.Messages.Where(m => m.Sender == userId || m.Recipient == userId).ToListAsync(); 
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
                appDbContext.Messages.Add((MessageDbo)message);
                await appDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException(e.Message, e);
            }
        }
    }
}
