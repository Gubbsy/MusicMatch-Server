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

        public async Task<ApplicationUserDbo> Register(string username, string email, string password, string name, string bio, double lat, double lon, string accountRole) 
        {
            username = Utils.ValidatorService.CheckIsEmpty(username) ?? throw new RepositoryException("USERNAME cannot be empty or null");
            email = Utils.ValidatorService.CheckIsEmpty(email) ?? throw new RepositoryException("EMAIL cannot be empty or null");
            password = Utils.ValidatorService.CheckIsEmpty(password) ?? throw new RepositoryException("PASSWORD cannot be empty or null");
            name = Utils.ValidatorService.CheckIsEmpty(name) ?? throw new RepositoryException("NAME cannot be empty or null");
            bio = Utils.ValidatorService.CheckIsEmpty(bio) ?? throw new RepositoryException("BIO cannot be empty or null");
            accountRole = Utils.ValidatorService.CheckRoleExists(accountRole) ?? throw new RepositoryException("Provided role does not exist");

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

            using (var transaction = appDbContext.Database.BeginTransaction()) 
            {
                IdentityResult identityResult = await userManager.CreateAsync(newUser, password).ConfigureAwait(false);

                if (!identityResult.Succeeded)
                {
                    throw new RepositoryException(identityResult.Errors.Select(e => e.Description).ToArray());
                }

                // Add transactions, fail to cerate  role should role back account creation.
                try
                {
                    IdentityResult addRoleIdentityResult = await userManager.AddToRoleAsync(newUser, accountRole).ConfigureAwait(false);
                }
                catch (InvalidOperationException e)
                {
                    throw new RepositoryException(e.Message, e);
                }

                await appDbContext.SaveChangesAsync().ConfigureAwait(false);

                transaction.Commit();
                return await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == newUser.UserName);
            }

        }
    }
}
