﻿using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;
using System.Collections.Generic;

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

        //Get Account Account

        public async Task<ApplicationUserDbo> GetUserAccount(string username) 
        {
            try
            {
                return await appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username).ConfigureAwait(false);
            }
            catch (Exception e) 
            { 
                throw new RepositoryException("Unable to retirve user");
            }
        }
        
        //Create Account
        public async Task<ApplicationUserDbo> Register(string accountRole, string username, string email, string password) 
        {
            accountRole = accountRole.ToUpper();
            accountRole = Utils.ValidatorService.CheckRoleExists(accountRole) ?? throw new RepositoryException("Role " + accountRole + " does not exist");
            username = Utils.ValidatorService.CheckIsEmpty(username) ?? throw new RepositoryException("USERNAME cannot be empty or null");
            email = Utils.ValidatorService.CheckIsEmpty(email) ?? throw new RepositoryException("EMAIL cannot be empty or null");
            password = Utils.ValidatorService.CheckIsEmpty(password) ?? throw new RepositoryException("PASSWORD cannot be empty or null");
   


            if ((await appDbContext.Users.CountAsync(u => u.UserName == username)) != 0)
            {
                throw new RepositoryException(nameof(username) + " value needs to be unique" );
            }

            if ((await appDbContext.Users.CountAsync(u => u.Email == email)) != 0)
            {
                throw new RepositoryException(nameof(email) + " is already taken");
            }

            ApplicationUserDbo newUser = new ApplicationUserDbo
            {
                UserName = username,
                Email = email,
                Name = "",
                Lat = 0,
                Lon = 0,
                Bio = "",
                LookingFor = "",
                MatchRadius = 100
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

             return await GetUserAccount(newUser.UserName);
        }

        //Update Account Details
        public async Task UpdateAccountDetails(string username, string[] genres, string[] venues, string name, string bio, string lookingFor, int matchRadius, double lat, double lon)
        {
            ApplicationUserDbo user = await GetUserAccount(username);

            if (user == null) 
            {
                throw new RepositoryException("Unable to find associated user to update");
            }

            user.Name = name;
            user.Bio = bio;
            user.LookingFor = lookingFor;
            user.Lat = lat;
            user.Lon = lon;
            user.MatchRadius = matchRadius;

            using (var transaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    await GenreAdditions(genres, user);
                    await VenueAdditions(venues, user);
                    appDbContext.Users.Update(user);
                    appDbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new RepositoryException(e.Message, e);
                }
            } 
        }



        //Add Genres
        public async Task<GenreDbo> GenreAdditions(string[] genres, ApplicationUserDbo user) 
        {
            GenreDbo genreDbo = null;

            IEnumerable<UserGenreDbo> g = await appDbContext.UserGenre
                .Include(ug => ug.AssociatedUser)
                .Where(ug => ug.AssociatedUser.Id == user.Id).ToListAsync();
            appDbContext.UserGenre.RemoveRange(g);

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
                    catch (Exception e)
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
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new RepositoryException(e.Message, e);
                }
            }

            return genreDbo;
        }

        //Add Venues
        public async Task<VenueDbo> VenueAdditions(string[] venues, ApplicationUserDbo user)
        {
            VenueDbo venueDbo = null;

            IEnumerable<UserVenueDbo> v = await appDbContext.UserVenue
                .Include(uv => uv.AssociatedUser)
                .Where(uv => uv.AssociatedUser.Id == user.Id).ToListAsync();
            appDbContext.UserVenue.RemoveRange(v);

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
                    await appDbContext.SaveChangesAsync().ConfigureAwait(false);
                }
                catch
                {
                    throw new RepositoryException("Unable to add VENUE(S)");
                }
            }

            return venueDbo;
        }
    }
}
