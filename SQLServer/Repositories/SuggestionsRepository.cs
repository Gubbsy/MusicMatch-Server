using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;

namespace SQLServer.Repositories
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly AppDbContext appDbContext;

        public SuggestionsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInMatchRadius(double minLat, double maxLat, double minLon, double maxLon)
        {
            try
            {
                return await appDbContext.Users
                    .Include(u => u.Genres)
                        .ThenInclude(g => g.Genre)
                    .Include(u => u.Venues)
                        .ThenInclude(v => v.Venue)
                    .Where(x => x.Lat >= minLat && x.Lat <= maxLat)
                    .Where(x => x.Lon >= minLon && x.Lon <= maxLon).ToListAsync();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retrive user suggestions");
            }
        }
    }
}
