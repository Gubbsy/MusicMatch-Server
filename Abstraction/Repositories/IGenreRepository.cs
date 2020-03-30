using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IGenreRepository
    {
        public Task<IEnumerable<Genre>> GetAllGenres();
        public Task GenreAdditions(string[] genres, ApplicationUser user);
    }
}
