using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IMatchRepository
    {
        public IEnumerable<string> GetMatches(string userID);
    }
}
