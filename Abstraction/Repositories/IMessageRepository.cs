using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IMessageRepository
    {
        public Task SaveMessage(Message message);
        public Task<IEnumerable<Message>> RetrieveMessage(string userId, string recipientId);
    }
}
