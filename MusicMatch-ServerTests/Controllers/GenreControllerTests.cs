using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MusicMatch_Server.Controllers.Tests
{
    public class GenreControllerTests
    {
        private Mock<IGenreRepository> genreRepository;
        private GenreController subject;

        public GenreControllerTests()
        {
            genreRepository = new Mock<IGenreRepository>();
            subject = new GenreController(genreRepository.Object);
        }

        [Fact()]
        public async void GetAllGenresTest_GenreRepositroy_GetAllGenres_IsCalledOnce()
        {
            genreRepository.Setup(x => x.GetAllGenres());
            await subject.GetAllGenres();

            genreRepository.Verify(x => x.GetAllGenres(), Times.Once());
        }

        [Fact()]
        public async void GetAllGenresTest_AlwaysReturnsA200()
        {
            List<Genre> genres = new List<Genre>() { };

            genreRepository.Setup(x => x.GetAllGenres()).ReturnsAsync(genres);
            ObjectResult result = await subject.GetAllGenres();

            Assert.Equal(200, result.StatusCode);
        }
    }
}