using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SQLServer.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Abstraction.Repositories;
using System.Security.Claims;
using Abstraction.Services;
using Abstraction.Models;
using System.Linq;
using SQLServer.Models;
using Microsoft.EntityFrameworkCore;

namespace SQLServer.Repositories.Tests
{
    public class GenreRepositoryTests
    {
        private Mock<AppDbContext> appDbContext;
        private Mock<DbContextOptions> options;
        private Mock<DbSet<GenreDbo>> genreDbo;
        private GenreRepository subject;

        public GenreRepositoryTests()
        {
            options = new Mock<DbContextOptions>();
            appDbContext = new Mock<AppDbContext>(options.Object);
            genreDbo = new Mock<DbSet<GenreDbo>>();

            subject = new GenreRepository(appDbContext.Object);
        }

        [Fact()]
        public async void GetAllGenresTest()
        {
            appDbContext.Setup(x => x.Genres).Returns(genreDbo.Object);
            await subject.GetAllGenres();
            appDbContext.Verify(x => x.Genres, Times.Once);
        }
    }
}