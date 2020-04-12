using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using SQLServer.Exceptions;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly AppDbContext appDbContext;

        public VenueRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Venue>> GetAllVenues()
        {
            try
            {
                return await appDbContext.Venues.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retirve Venues", e);
            }

        }

        //Add Venues
        public async Task VenueAdditions(string[] venues, ApplicationUser user)
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
                    AssociatedUser = (ApplicationUserDbo)user,
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
        }
    }
}

