using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUserDbo> userManager;
        private readonly AppDbContext appDbContext;
        private readonly IVenueRepository venueRepository;
        private readonly IGenreRepository genreRepository;

        public UserRepository(UserManager<ApplicationUserDbo> userManager, AppDbContext appDbContext, IVenueRepository venueRepository, IGenreRepository genreRepository)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            this.venueRepository = venueRepository;
            this.genreRepository = genreRepository;
        }

        // Get Account Role

        public async Task<string> GetAcountRole(string userID)
        {
            string role = "no role found";
            try
            {
                ApplicationUser user = await GetUserAccount(userID);
                IList<string> roles = await userManager.GetRolesAsync((ApplicationUserDbo)user).ConfigureAwait(false);
                if (roles.Count > 0)
                {
                    role = roles[0];
                }
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retrive user role", e);
            }

            return role;
        }

        //Get Account Details

        public async Task<ApplicationUser> GetUserAccount(string userId)
        {
            try
            {
                return await appDbContext.Users
                      .Include(u => u.Genres)
                          .ThenInclude(g => g.Genre)
                      .Include(u => u.Venues)
                          .ThenInclude(v => v.Venue)
                      .FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retirve user", e);
            }
        }

        //Create Account
        public async Task Register(string accountRole, string username, string email, string password)
        {
            accountRole = accountRole.ToUpper();
            accountRole = Utils.ValidatorService.CheckRoleExists(accountRole) ?? throw new RepositoryException("Role " + accountRole + " does not exist");
            username = Utils.ValidatorService.CheckIsEmpty(username) ?? throw new RepositoryException("USERNAME cannot be empty or null");
            email = Utils.ValidatorService.CheckIsEmpty(email) ?? throw new RepositoryException("EMAIL cannot be empty or null");
            password = Utils.ValidatorService.CheckIsEmpty(password) ?? throw new RepositoryException("PASSWORD cannot be empty or null");



            if ((await appDbContext.Users.CountAsync(u => u.UserName == username)) != 0)
            {
                throw new RepositoryException(nameof(username) + " value needs to be unique");
            }

            if ((await appDbContext.Users.CountAsync(u => u.Email == email)) != 0)
            {
                throw new RepositoryException(nameof(email) + " is already taken");
            }

            ApplicationUserDbo newUser = new ApplicationUserDbo
            {
                UserName = username,
                Email = email,
                Name = username,
                Picture = "",
                Lat = 0,
                Lon = 0,
                Bio = "",
                LookingFor = "",
                MatchRadius = 100,
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
            }
        }

        //Update Account Details
        public async Task UpdateAccountDetails(string userId, string[] genres, string[] venues, string name, string picture, string bio, string lookingFor, int matchRadius, double lat, double lon)
        {
            ApplicationUserDbo user = (ApplicationUserDbo)await GetUserAccount(userId);

            if (user == null)
            {
                throw new RepositoryException("Unable to find associated user to update");
            }

            user.Name = name;
            user.Picture = picture;
            user.Bio = bio;
            user.LookingFor = lookingFor;
            user.Lat = lat;
            user.Lon = lon;
            user.MatchRadius = matchRadius;

            using (var transaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    await genreRepository.GenreAdditions(genres, user);
                    await venueRepository.VenueAdditions(venues, user);
                    appDbContext.Users.Update(user);
                    appDbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new RepositoryException(e.Message, e);
                }

                transaction.Commit();
            }
        }
    }
}
