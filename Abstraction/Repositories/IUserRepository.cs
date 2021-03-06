﻿using Abstraction.Models;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IUserRepository
    {
        public Task<ApplicationUser> GetUserAccount(string userId);
        public Task Register(string accountRole, string username, string email, string password);
        public Task UpdateAccountDetails(string userId, string[] genres, string[] venues, string name, string picture, string bio, string lookingFor, int matchRadius, double lat, double lon);
        public Task<string> GetAcountRole(string userID);
    }
}
