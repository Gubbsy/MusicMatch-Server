using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;
using SQLServer.Models;

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
                throw new RepositoryException("Unable to retrive user suggestions", e);
            }
        }

        public async Task<bool> AddIntroduction(string uId1, string uId2, bool requested) {

            bool matched = false;

            using (var transaction = appDbContext.Database.BeginTransaction())
            {

                try
                {
                    IntroductionsDbo intro = new IntroductionsDbo()
                    {
                        Requested = requested,
                        UId1 = uId1,
                        UId2 = uId2
                    };

                    appDbContext.Introductions.Add(intro);
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);

                    if (requested == true && appDbContext.Introductions.Any(i => i.UId2 == i.UId1) && appDbContext.Introductions.Any(i => i.Requested == true))
                    {
                        await AddMatch(uId1, uId2);
                        matched = true;
                    }

                }
                catch (Exception e)
                {
                    throw new RepositoryException("Unable to process suggestion choice", e);
                }

                transaction.Commit();
            }

            return matched;
        }
        
        public async Task AddMatch(string uId1, string uId2) 
        {
            try
            {
                MatchesDbo match = new MatchesDbo()
                {
                    UId1 = uId1,
                    UId2 = uId2,
                    MatchDate = DateTime.Now
                };

                appDbContext.Matches.Add(match);
                await appDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to process suggestion choice", e);
            }
        }
    }
}
