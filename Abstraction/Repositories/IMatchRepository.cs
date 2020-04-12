using System.Collections.Generic;

namespace Abstraction.Repositories
{
    public interface IMatchRepository
    {
        public IEnumerable<string> GetMatches(string userID);
    }
}
