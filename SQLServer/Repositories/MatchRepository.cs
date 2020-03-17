using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SQLServer.Exceptions;

namespace SQLServer.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext appDbContext;

        public MatchRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<string> GetMatches(string userId)
        {
            List<string> matchIds = new List<string>();

            try
            {
                IEnumerable<Matches> matches = appDbContext.Matches.Where(m => m.User == userId).ToList();
                matchIds = matches.Select(x => x.Matchie).ToList();
            }
            catch (Exception e) 
            {
                throw new RepositoryException("Unable to retieve matches", e);
            }

            return matchIds;
        }
    }
}
