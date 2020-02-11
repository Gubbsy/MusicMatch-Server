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
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("createuser")]
        public async Task<Responses.NewUser> CreateTest(Requests.CreateUser createUser)
        {
            ApplicationUserDbo newUserdbo = await userRepository.Register(createUser.Username, createUser.Email, createUser.Password, createUser.Name, createUser.Bio, createUser.Lat, createUser.Lon);
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
