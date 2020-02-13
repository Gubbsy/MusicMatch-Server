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

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("createuser")]
        public async Task<ObjectResult> CreateTest(Requests.CreateAccount createAccount)
        {
            ApplicationUserDbo newUserdbo = await userRepository.Register(createAccount.AccountRole, createAccount.Username, createAccount.Email, createAccount.Password, createAccount.Name, createAccount.Lat, createAccount.Lon, createAccount.Bio, createAccount.LookingFor, createAccount.Genres, createAccount.MatchRadius);
            return Ok(new Responses.NewUser
            {
                Id = newUserdbo.Id,
                Username = newUserdbo.UserName,
                Email = newUserdbo.Email,
                Name = newUserdbo.Name,
                Lat = newUserdbo.Lat,
                Lon = newUserdbo.Lon,
                Bio = newUserdbo.Bio,
                LookingFor = newUserdbo.LookingFor,
                Genres = newUserdbo.Genres.Select(ug => ug.Genre.Name).ToArray(),
            });
        }
    }
}
