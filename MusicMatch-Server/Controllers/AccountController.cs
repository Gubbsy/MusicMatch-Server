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

        [HttpPost(Endpoints.Account + "createaccount")]
        public async Task<ObjectResult> CreateTest(Requests.CreateAccount createAccount)
        {
            ApplicationUserDbo newUserdbo = await userRepository.Register(createAccount.AccountRole, createAccount.Username, createAccount.Email, createAccount.Password);
            return Ok(new Responses.NewUser
            {
                Id = newUserdbo.Id,
                AccountRole = createAccount.AccountRole,
                Username = newUserdbo.UserName,
                Email = newUserdbo.Email,
            });
        }
    }
}
