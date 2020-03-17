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
        private readonly IUserRepository userRepository;

        public SuggestionsRepository(AppDbContext appDbContext, IUserRepository userRepository)
        {
            this.appDbContext = appDbContext;
            this.userRepository = userRepository;
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

        public async Task<bool> AddIntroduction(string sender, string recipient, bool requested) {

            bool matched = false;

            using (var transaction = appDbContext.Database.BeginTransaction())
            {

                try
                {
                    IntroductionsDbo intro = new IntroductionsDbo()
                    {
                        Requested = requested,
                        UId1 = sender,
                        UId2 = recipient
                    };

                    appDbContext.Introductions.Add(intro);
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);

                    Introductions introduction = await appDbContext.Introductions.FirstOrDefaultAsync(i => i.UId1 == recipient && i.UId2 == sender);

                    if (requested == true && introduction != null && introduction.Requested == true)
                    {
                        await AddMatch(sender, recipient);
                        matched = true;
                    }

                }
                catch (Exception e)
                {
                    throw new RepositoryException("Unable to process response to sugestions", e);
                }

                transaction.Commit();
            }

            return matched;
        }
        
        public async Task AddMatch(string user, string matchie) 
        {
            try
            {
                MatchesDbo userMatch = new MatchesDbo()
                {
                    User = user,
                    Matchie = matchie,
                    MatchDate = DateTime.Now
                };

                MatchesDbo matchieMatch = new MatchesDbo()
                {
                    User = matchie,
                    Matchie = user,
                    MatchDate = DateTime.Now
                };

                appDbContext.Matches.Add(userMatch);
                appDbContext.Matches.Add(matchieMatch);
                await appDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to add match", e);
            }
        }

        public IEnumerable<string> GetPreviousSuggestions(string userId)
        {
            List<string> previouseSuggestionIds = new List<string>();

            try
            {
                IEnumerable<Introductions> previouseIntroductions = appDbContext.Introductions.Where(i => i.UId1 == userId);
                previouseSuggestionIds =  previouseIntroductions.Select(x => x.UId2).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unabl to retieve previouse matches", e);
            }

            return previouseSuggestionIds;
        }
    }
}
