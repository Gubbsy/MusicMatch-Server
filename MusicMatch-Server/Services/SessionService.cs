using Abstraction.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicMatch_Server.Services
{
    public class SessionService : ISessionService
    {
        private readonly HttpContextAccessor httpContextAccessor;

        public SessionService(HttpContextAccessor httpContextAccessor) 
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId() 
        { 
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
