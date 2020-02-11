using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;

namespace SQLServer.Repositories
{ 
    public class UserRepository
    {
        private readonly UserManager<ApplicationUserDbo> userManager;
        private readonly AppDbContext appDbContext;

        public UserRepository(UserManager<ApplicationUserDbo> userManager, AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public async Task<ApplicationUserDbo> Register(string username, string email, string password, string name, string bio, double lat, double lon) 
        {
            if ((await appDbContext.Users.CountAsync(u => u.UserName == username)) != 0)
            {
                throw new RepositoryException(nameof(username) + " value needs to be unique" );
            }

            ApplicationUserDbo newUser = new ApplicationUserDbo
            {
                UserName = username,
                Email = email,
                Name = name,
                Bio = bio,
                Lat = lat,
                Lon = lon
            };

            IdentityResult identityResult = await userManager.CreateAsync(newUser, password).ConfigureAwait(false);

            //TODO: Throw exceptions array for IdentityResult
            if (!identityResult.Succeeded) 
            {
                throw new RepositoryException(identityResult.Errors.Select(e => e.Description).ToArray());
            }
            await appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == newUser.UserName);
     

        }
    }
}
