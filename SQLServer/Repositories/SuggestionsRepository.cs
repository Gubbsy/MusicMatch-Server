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

        public async Task<bool> AddIntroduction(string uId1, string uId2, bool requested) {

            bool matched = false;

            ApplicationUser sender = await userRepository.GetUserAccount(uId1);
            ApplicationUser recipient = await userRepository.GetUserAccount(uId2);

            using (var transaction = appDbContext.Database.BeginTransaction())
            {

                try
                {
                    IntroductionsDbo intro = new IntroductionsDbo()
                    {
                        DidRequest = requested,
                        Sender = sender,
                        Recipient = recipient
                    };

                    appDbContext.Introductions.Add(intro);
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);

                    Introduction introduction = await appDbContext.Introductions.FirstOrDefaultAsync(i => i.Sender == recipient && i.Recipient == sender);

                    if (requested == true && introduction != null && introduction.DidRequest == true)
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
        
        private async Task AddMatch(ApplicationUser user, ApplicationUser matcher) 
        {
            try
            {
                // Add matches both ways rond so only one column needs to be quried to return a list of a users matches. 

                MatchDbo newMatch1 = new MatchDbo()
                {
                    User = (ApplicationUserDbo)user,
                    Matcher = (ApplicationUserDbo)matcher,
                    MatchDate = DateTime.Now
                };

                MatchDbo newMatch2 = new MatchDbo()
                {
                    User = (ApplicationUserDbo)matcher,
                    Matcher = (ApplicationUserDbo)user,
                    MatchDate = DateTime.Now
                };

                appDbContext.Matches.Add(newMatch1);
                appDbContext.Matches.Add(newMatch2);
                await appDbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to add match", e);
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetPreviousSuggestions(string userId)
        {
            List<ApplicationUser> previouseSuggestion = new List<ApplicationUser>();

            ApplicationUser user = await userRepository.GetUserAccount(userId);

            try
            {
                IEnumerable<Introduction> previouseIntroductions = appDbContext.Introductions.Where(i => i.Sender == user);

                foreach (Introduction introduction in previouseIntroductions)
                {
                    previouseSuggestion.Add(introduction.Recipient);
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unabl to retieve previouse matches", e);
            }

            return previouseSuggestion;
        }
    }
}
