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
    public class AccountController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("createuser")]
        public async Task<Responses.NewUser> CreateTest(Requests.CreateAccount createAccount)
        {
            ApplicationUserDbo newUserdbo = await userRepository.Register(createAccount.Username, createAccount.Email, createAccount.Password, createAccount.Name, createAccount.Bio, createAccount.Lat, createAccount.Lon, createAccount.AccountRole);
            return new Responses.NewUser
            {
                Id = newUserdbo.Id,
                Username = newUserdbo.Name,
                Email = newUserdbo.Email,
                Name = newUserdbo.Name,
                Bio = newUserdbo.Bio,
                Lat = newUserdbo.Lat,
                Lon = newUserdbo.Lon
            };
            
        }
    }
}
