using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Mvc;
using MusicMatch_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    public class MatchController : APIControllerBase
    {
        private readonly IMatchRepository matchRepository;

        public MatchController(IMatchRepository matchRepository, SessionService sessionService)
        {
            this.matchRepository = matchRepository;
            SessionService = sessionService;
        }

        public SessionService SessionService { get; }

        [HttpPost(Endpoints.Matches + "getmatches")]
        public async Task<ObjectResult> GetMatches() 
        {
            string userID = SessionService.GetCurrentUserId();
            IEnumerable<ApplicationUser> matches = await matchRepository.GetMatches(userID);

            return Ok(matches);
        }
    }
}
