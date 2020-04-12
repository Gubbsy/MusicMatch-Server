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
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext appDbContext;

        public GenreRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            try
            {
                return await appDbContext.Genres.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unable to retirve Genres", e);
            }
        }

        //Add Genres
        public async Task GenreAdditions(string[] genres, ApplicationUser user)
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
                    AssociatedUser = (ApplicationUserDbo)user,
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
        }
    }
}
