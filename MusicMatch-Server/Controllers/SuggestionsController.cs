using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;
using MusicMatch_Server.Responses;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class SuggestionsController : APIControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionService sessionService;
        private readonly ISuggestionsRepository suggestionsRepository;

        public SuggestionsController(IUserRepository userRepository, ISessionService sessionService, ISuggestionsRepository suggestionsRepository)
        {
            this.userRepository = userRepository;
            this.sessionService = sessionService;
            this.suggestionsRepository = suggestionsRepository;
        }

        [HttpPost(Endpoints.Suggestions + "getsuggestions")]
        public async Task<ObjectResult> GetSuggestions()
        {
            string userId = sessionService.GetCurrentUserId();
            ApplicationUser user = await userRepository.GetUserAccount(userId);

            CoordinateBoundaries boundaries = new CoordinateBoundaries(user.Lat, user.Lon, user.MatchRadius, DistanceUnit.Kilometers);

            IEnumerable<ApplicationUser> matchesInRadius = await suggestionsRepository.GetUsersInMatchRadius(boundaries.MinLatitude, boundaries.MaxLatitude, boundaries.MinLongitude, boundaries.MaxLongitude);

            IEnumerable<string> previouslyRespondedSuggestionsIds = suggestionsRepository.GetPreviousSuggestions(userId);

            IEnumerable<SuggestedUser> suggestedUsers = matchesInRadius.Select(x => new SuggestedUser
            {
                Id  = x.Id,
                Name = x.Name,
                Bio = x.Bio,
                LookingFor = x.LookingFor,
                Genres = x.Genres.Select(ug => ug.Genre.Name).ToArray(),
                Venues = x.Venues.Select(uv => uv.Venue.Name).ToArray(),
                Distance = GeoCalculator.GetDistance(user.Lat, user.Lon, x.Lat, x.Lon)
            })
                .Where(x => x.Id != user.Id)
                .Where(x =>!previouslyRespondedSuggestionsIds.Contains(x.Id))
                .Where(x => x.Distance <= user.MatchRadius)
                .OrderBy(x => x.Distance);

            return Ok(new SuggestedUsers() {
                suggestedUsers = suggestedUsers
            });
        }

        [HttpPost(Endpoints.Suggestions + "respondtosuggestion")]
        public async Task<ObjectResult> RespondToSuggestion(Requests.ResponseToSuggestion request)
        {
            string userId = sessionService.GetCurrentUserId();

            bool didMatch = await this.suggestionsRepository.AddIntroduction(userId, request.SuggestedUserId, request.requestMatch);

            return Ok(new Matched()
            {
                DidMatch = didMatch
            }) ;
        }

    }
}
