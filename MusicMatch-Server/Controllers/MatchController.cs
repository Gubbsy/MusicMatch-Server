using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Geolocation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    public class MatchController : APIControllerBase
    {
        private readonly IMatchRepository matchRepository;
        private readonly ISessionService sessionService;
        private readonly IUserRepository userRepository;

        public MatchController(IMatchRepository matchRepository, ISessionService sessionService, IUserRepository userRepository)
        {
            this.matchRepository = matchRepository;
            this.sessionService = sessionService;
            this.userRepository = userRepository;
        }


        [HttpPost(Endpoints.Matches + "getmatches")]
        public async Task<ObjectResult> GetMatches()
        {
            string userId = sessionService.GetCurrentUserId();
            ApplicationUser currentUser = await userRepository.GetUserAccount(userId);

            IEnumerable<string> matchIds = matchRepository.GetMatches(userId);
            List<ReturnedUser> matches = new List<ReturnedUser>();

            foreach (string id in matchIds)
            {
                ApplicationUser match = await userRepository.GetUserAccount(id);

                ReturnedUser matchResponse = new ReturnedUser
                {
                    Id = match.Id,
                    Username = match.UserName,
                    Name = match.Name,
                    Bio = match.Bio,
                    LookingFor = match.LookingFor,
                    Genres = match.Genres.Select(ug => ug.Genre.Name).ToArray(),
                    Venues = match.Venues.Select(uv => uv.Venue.Name).ToArray(),
                    Distance = GeoCalculator.GetDistance(currentUser.Lat, currentUser.Lon, match.Lat, match.Lon),
                    Role = await userRepository.GetAcountRole(match.Id)
                };

                matches.Add(matchResponse);
            }

            return Ok(matches);
        }
    }
}
