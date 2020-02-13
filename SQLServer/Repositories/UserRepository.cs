using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System;
using System.Linq;
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

        public async Task<ApplicationUserDbo> Register(string accountRole, string username, string email, string password, string name, double lat, double lon, string bio, string lookingFor, string[] genres, string[] venues ,int matchRadius) 
        {
            accountRole = Utils.ValidatorService.CheckRoleExists(accountRole) ?? throw new RepositoryException("Role " + accountRole.ToUpper() + " does not exist");
            username = Utils.ValidatorService.CheckIsEmpty(username) ?? throw new RepositoryException("USERNAME cannot be empty or null");
            email = Utils.ValidatorService.CheckIsEmpty(email) ?? throw new RepositoryException("EMAIL cannot be empty or null");
            password = Utils.ValidatorService.CheckIsEmpty(password) ?? throw new RepositoryException("PASSWORD cannot be empty or null");
            name = Utils.ValidatorService.CheckIsEmpty(name) ?? throw new RepositoryException("NAME cannot be empty or null");
            bio = Utils.ValidatorService.CheckIsEmpty(bio) ?? throw new RepositoryException("BIO cannot be empty or null");
            lookingFor = Utils.ValidatorService.CheckIsEmpty(lookingFor) ?? throw new RepositoryException("LOOKINGFOR cannot be empty or null");


            if ((await appDbContext.Users.CountAsync(u => u.UserName == username)) != 0)
            {
                throw new RepositoryException(nameof(username) + " value needs to be unique" );
            }


            ApplicationUserDbo newUser = new ApplicationUserDbo
            {
                UserName = username,
                Email = email,
                Name = name,
                Lat = lat,
                Lon = lon,
                Bio = bio,
                LookingFor = lookingFor,
                MatchRadius = matchRadius
                
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

            await GenreAdditions(genres, newUser).ConfigureAwait(false);
            await VenueAdditions(venues, newUser).ConfigureAwait(false);


            return await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == newUser.UserName);
        }

        public async Task<GenreDbo> GenreAdditions(string[] genres, ApplicationUserDbo user) 
        {
            GenreDbo genreDbo = null;

            foreach (string genre in genres)
            {
                if ((await appDbContext.Genres.CountAsync(g => g.Name == genre)) == 0)
                {
                    genreDbo = new GenreDbo
                    {
                        Name = genre
                    };

                    try
                    {
                        appDbContext.Genres.Add(genreDbo);
                    }
                    catch (InvalidOperationException e)
                    {
                        throw new RepositoryException(e.Message);
                    }
                }
                else 
                {
                    genreDbo = await appDbContext.Genres.FirstOrDefaultAsync(g => g.Name == genre);
                }

                UserGenreDbo userGenreDbo = new UserGenreDbo
                {
                    UserId = user.Id,
                    AssociatedUser = user,
                    GenreId = genreDbo.Id,
                    Genre = genreDbo
                };

                try
                {
                    appDbContext.UserGenre.Add(userGenreDbo);
                }
                catch
                {
                    throw new RepositoryException("Unable to add GENRE(S)");
                }
            }

            await appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return genreDbo;
        }

        public async Task<VenueDbo> VenueAdditions(string[] venues, ApplicationUserDbo user)
        {
            VenueDbo venueDbo = null;

            foreach (string venue in venues)
            {
                if ((await appDbContext.Venues.CountAsync(v => v.Name == venue)) == 0)
                {
                    venueDbo = new VenueDbo
                    {
                        Name = venue
                    };

                    try
                    {
                        appDbContext.Venues.Add(venueDbo);
                    }
                    catch (InvalidOperationException e)
                    {
                        throw new RepositoryException(e.Message);
                    }
                }
                else
                {
                    venueDbo = await appDbContext.Venues.FirstOrDefaultAsync(v => v.Name == venue);
                }

                UserVenueDbo userVenueDbo = new UserVenueDbo
                {
                    UserId = user.Id,
                    AssociatedUser = user,
                    VenueId = venueDbo.Id,
                    Venue = venueDbo
                };

                try
                {
                    appDbContext.UserVenue.Add(userVenueDbo);
                }
                catch
                {
                    throw new RepositoryException("Unable to add VENUE(S)");
                }
            }

            await appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return venueDbo;
        }
    }
}
