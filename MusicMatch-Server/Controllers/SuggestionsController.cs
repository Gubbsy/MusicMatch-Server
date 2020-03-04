using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class SuggestionsController : APIControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionService sessionService;

        public SuggestionsController(IUserRepository userRepository, ISessionService sessionService)
        {
            this.userRepository = userRepository;
            this.sessionService = sessionService;
        }

        [HttpPost(Endpoints.Suggestions + "getsuggestions")]
        public async Task<ObjectResult> GetSuggestions() 
        {
            string userId = sessionService.GetCurrentUserId();
            ApplicationUser user = await userRepository.GetUserAccount(userId);

            CoordinateBoundaries boundaries = new CoordinateBoundaries(user.Lat, user.Lon, user.MatchRadius, DistanceUnit.Kilometers);

            return NoContent();
        }
    }
}
