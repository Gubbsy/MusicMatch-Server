using Microsoft.AspNetCore.Mvc;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLServer.Repositories;
using Abstraction.Models;
using Abstraction.Repositories;

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
        public async Task<ObjectResult> GetAllGenres() {

            IEnumerable<Genre> genres = await genreRespository.GetAllGenres();

            return Ok(new Responses.AllGenres
            {
                Genres = genres.Select(g => g.Name).ToArray()
            }) ;
        }

    }
}
