using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> AddIntroduction(string sender, string recipient, bool requested)
        {

            bool matched = false;

            using (var transaction = appDbContext.Database.BeginTransaction())
            {

                try
                {
                    IntroductionsDbo intro = new IntroductionsDbo()
                    {
                        Requested = requested,
                        Sender = sender,
                        Recipient = recipient
                    };

                    appDbContext.Introductions.Add(intro);
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);

                    Introductions introduction = await appDbContext.Introductions.FirstOrDefaultAsync(i => i.Sender == recipient && i.Recipient == sender);

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
                IEnumerable<Introductions> previouseIntroductions = appDbContext.Introductions.Where(i => i.Sender == userId);
                previouseSuggestionIds = previouseIntroductions.Select(x => x.Recipient).ToList();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retieve previouse introductions", e);
            }

            return previouseSuggestionIds;
        }
    }
}
