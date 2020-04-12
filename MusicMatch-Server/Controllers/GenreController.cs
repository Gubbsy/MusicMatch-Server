using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class GenreController : APIControllerBase
    {
        private readonly IGenreRepository genreRespository;

        public GenreController(IGenreRepository genreRespository)
        {
            this.genreRespository = genreRespository;
        }

        [HttpPost(Endpoints.Genres + "getallgenres")]
        public async Task<ObjectResult> GetAllGenres()
        {

            IEnumerable<Genre> genres = await genreRespository.GetAllGenres();

            return Ok(new Responses.AllGenres
            {
                Genres = genres.Select(g => g.Name).ToArray()
            });
        }

    }
}
