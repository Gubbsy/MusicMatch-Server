using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SQLServer.Repositories
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly AppDbContext appDbContext;

        public SuggestionsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInMatchRadius(double maxLat, double maxLon)
        {
            try
            {
                return await appDbContext.Users.
            }
            catch (Exception e)
            { 

            }
        }
    }
}
