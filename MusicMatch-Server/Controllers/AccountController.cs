using Microsoft.AspNetCore.Mvc;
using SQLServer.Exceptions;
using SQLServer.Models;
using SQLServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class AccountController : APIControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly SignInRepository signInRepository;

        public AccountController(UserRepository userRepository, SignInRepository signInRepository)
        {
            this.userRepository = userRepository;
            this.signInRepository = signInRepository;
        }

        [HttpPost(Endpoints.Account + "createaccount")]
        public async Task<ObjectResult> CreateTest(Requests.CreateAccount createAccount)
        {
            ApplicationUserDbo newUserdbo = await userRepository.Register(createAccount.AccountRole, createAccount.Username, createAccount.Email.ToLower(), createAccount.Password);
            return Ok(new Responses.NewUser
            {
                Id = newUserdbo.Id,
                AccountRole = createAccount.AccountRole,
                Username = newUserdbo.UserName,
                Email = newUserdbo.Email,
            });
        }

        [HttpPost(Endpoints.Account + "signin")]
        public async Task<ObjectResult> SignIn(Requests.SignIn request)
        {
            if (request == null)
            {
                return NoRequest();
            }

            IEnumerable<string>? result = await signInRepository.SignIn(request.Credential, request.Password).ConfigureAwait(false);

            if (result == null)
            {
                return Unauthorized("Unable to log in user.");
            }

            return Ok(new Responses.SignedInUser {
                role = result
            });
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

           await userRepository.UpdateAccountDetails(request.username, request.Genres, request.Venues, request.Name, request.Bio, request.LookingFor, request.MatchRadius, request.Lat, request.Lon).ConfigureAwait(false);

            return NoContent();
        }
    }
}
