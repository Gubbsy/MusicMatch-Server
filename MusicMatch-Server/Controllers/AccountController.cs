using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMatch_Server.Services;
using SQLServer.Exceptions;
using SQLServer.Models;
using SQLServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class AccountController : APIControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISignInRepository signInRepository;
        private readonly ISessionService sesionService;

        public AccountController(IUserRepository userRepository, ISignInRepository signInRepository, ISessionService sesionService)
        {
            this.userRepository = userRepository;
            this.signInRepository = signInRepository;
            this.sesionService = sesionService;
        }

        [HttpPost(Endpoints.Account + "createaccount")]
        public async Task<ObjectResult> CreateAccount(Requests.CreateAccount request)
        {
            if (request == null)
            {
                return NoRequest();
            }

            await userRepository.Register(request.AccountRole, request.Username, request.Email.ToLower(), request.Password);
            return NoContent();
        }

        [HttpPost(Endpoints.Account + "signin")]
        public async Task<ObjectResult> SignIn(Requests.SignIn request)
        {
            if (request == null)
            {
                return NoRequest();
            }

            IEnumerable<string>? role = await signInRepository.SignIn(request.Credential, request.Password).ConfigureAwait(false);

            if (role == null)
            {
                return Unauthorized("Incorrect Username or Password");
            }

            return NoContent();
        }

        [HttpPost(Endpoints.Account + "signout")]
        public async Task<ObjectResult> SignOut()
        {
            await signInRepository.SignOut().ConfigureAwait(false);
            return NoContent();
        }

        [HttpPost(Endpoints.Account + "updateaccountdetails")]
        public async Task<ObjectResult> UpdateAccountDetails(Requests.UpdateAccountDetails request) 
        {
            if (request == null)
            {
                return NoRequest();
            }

            string userId = sesionService.GetCurrentUserId();

            await userRepository.UpdateAccountDetails(userId, request.Genres, request.Venues, request.Name, request.Bio, request.LookingFor, request.MatchRadius, request.Lat, request.Lon).ConfigureAwait(false);

            return NoContent();
        }

        [HttpPost(Endpoints.Account + "getaccountdetails")]
        public async Task<ObjectResult> GetAccountDetails() 
        {
            string userId = sesionService.GetCurrentUserId();
            ApplicationUser user = await userRepository.GetUserAccount(userId);

            return Ok(new Responses.AccountDetails
            {
                Name = user.Name,
                Bio = user.Bio,
                LookingFor = user.LookingFor,
                Lat = user.Lat,
                Lon = user.Lon,
                MatchRadius = user.MatchRadius,
                Genres = user.Genres.Select(ug => ug.Genre.Name).ToArray(),
                Venues = user.Venues.Select(uv => uv.Venue.Name).ToArray()
            }) ;
        }
    }
}
