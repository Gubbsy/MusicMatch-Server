using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IMatchRepository
    {
        public Task<IEnumerable<ApplicationUser>> GetMatches(string userID);
    }
}
