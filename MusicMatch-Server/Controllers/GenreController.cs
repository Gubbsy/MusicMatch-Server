using Microsoft.AspNetCore.Mvc;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLServer.Repositories;

namespace MusicMatch_Server.Controllers
{
    [ApiController]
    public class GenreController : APIControllerBase
    {
        private readonly GenreRepository genreRespository;

        public GenreController(GenreRepository genreRespository)
        {
            this.genreRespository = genreRespository;
        }

        [HttpPost(Endpoints.Genres + "getallgenres")]
        public async Task<ObjectResult> GetAllGenres() {

            IEnumerable<GenreDbo> genreDbos = await genreRespository.GetAllGenres();

            return Ok(new Responses.AllGenres
            {
                Genres = genreDbos.Select(g => g.Name).ToArray()
            }) ;
        }

    }
}
