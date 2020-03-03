using Xunit;
using MusicMatch_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SQLServer.Repositories;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Repositories;
using System.Security.Claims;
using MusicMatch_Server.Services;
using Abstraction.Services;
using Abstraction.Models;
using System.Linq;

namespace MusicMatch_Server.Controllers.Tests
{
    public class AccountControllerTests
    {
        private Mock<IUserRepository> userRepository;
        private Mock<ISignInRepository> signInRepository;
        private Mock<ISessionService> sessionService;

        private AccountController subject;

        public AccountControllerTests()
        {
            userRepository = new Mock<IUserRepository>();
            signInRepository = new Mock<ISignInRepository>();
            sessionService = new Mock<ISessionService>();

            subject = new AccountController(userRepository.Object, signInRepository.Object, sessionService.Object);
        }

        [Fact()]
        public async Task CreateAccount_AlwaysReturnsA_400ONullRequest()
        {
            //  Mock<AppDbContext>
            Requests.CreateAccount request = null;

            ObjectResult result = await subject.CreateAccount(request);

            Assert.Equal(400, result.StatusCode);
        }

        [Fact()]
        public async Task CreateAccount_AlwaysReturnsA204()
        {
            Requests.CreateAccount request = new Requests.CreateAccount
            {
                AccountRole = "Artist",
                Email = "Test@Test.com",
                Password = "abcABC123;",
                Username = "TestyMcTest"
            };

            ObjectResult result = await subject.CreateAccount(request);

            Assert.Equal(204, result.StatusCode);
        }

        [Fact()]
        public async Task CreateAccount_UserRepository_RegisterIsCalledOnce()
        {
            Requests.CreateAccount request = new Requests.CreateAccount
            {
                AccountRole = "Artist",
                Username = "TestyMcTest",
                Email = "Test@Test.com",
                Password = "abcABC123;"
               
            };

            userRepository.Setup(x => x.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            ObjectResult result = await subject.CreateAccount(request);

            userRepository.Verify(x => x.Register(request.AccountRole, request.Username, request.Email.ToLower(), request.Password), Times.Once);
        }


        [Fact()]
        public async void SignInTest_AlwaysReturnsA_400ONullRequest()
        {
            Requests.SignIn request = null;

            ObjectResult result = await subject.SignIn(request);
            
            Assert.Equal(400, result.StatusCode);
        }

        [Fact()]
        public async void SignInTest_AlwaysReturnsA204()
        {
            Requests.SignIn request = new Requests.SignIn
            {
                Credential = "Test@Test.com",
                Password = "abcABC123;",
            };

            ObjectResult result = await subject.SignIn(request);

            Assert.Equal(204, result.StatusCode);
        }

        [Fact()]
        public async Task SignInTest_SignInRepository_SignInIsCalledOnce()
        {
            Requests.SignIn request = new Requests.SignIn
            {
                Credential = "Test@Test.com",
                Password = "abcABC123;"
            };

            signInRepository.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>()));

            ObjectResult result = await subject.SignIn(request);

            signInRepository.Verify(x => x.SignIn(request.Credential, request.Password), Times.Once);
        }

        [Fact()]
        public async void SignOut_AlwaysReturnsA204()
        {
            signInRepository.Setup(x => x.SignOut());
            ObjectResult result = await subject.SignOut();

            Assert.Equal(204, result.StatusCode);
        }

        [Fact()]
        public async void SignOut_SignInRepository_SignOutIsCalldOnce()
        {
            signInRepository.Setup(x => x.SignOut());
            ObjectResult result = await subject.SignOut();

            signInRepository.Verify(x => x.SignOut(), Times.Once);
        }


        [Fact()]
        public async void UpdateAccountDetails_AlwaysReturnsA400_OnNullRequest()
        {
            Requests.UpdateAccountDetails request = null;

            ObjectResult result = await subject.UpdateAccountDetails(request);

            Assert.Equal(400, result.StatusCode);
        }

        [Fact()]
        public async void UpdateAccountDetails_AlwaysReturnsA204()
        {
            Requests.UpdateAccountDetails request = new Requests.UpdateAccountDetails
            {
                Genres = new string[] { "Rock", "Roll" },
                Venues = new string[] { "Blue Hut", "Nug" },
                Name = "Test Name",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns("Test ID");

            ObjectResult result = await subject.UpdateAccountDetails(request);

            Assert.Equal(204, result.StatusCode);
        }

        [Fact()]
        public async void UpdateAccountDetails_UserRepository_UpdateAccountDetailsIsCalledOnce()
        {
            string userId = "Test-ID";
            Requests.UpdateAccountDetails request = new Requests.UpdateAccountDetails
            {
                Genres = new string[] { "Rock", "Roll" },
                Venues = new string[] { "Blue Hut", "Nug" },
                Name = "Test Name",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.UpdateAccountDetails(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>()));

            ObjectResult result = await subject.UpdateAccountDetails(request);

            userRepository.Verify(x => x.UpdateAccountDetails(userId, request.Genres, request.Venues, request.Name, request.Bio, request.LookingFor, request.MatchRadius, request.Lat, request.Lon), Times.Once);
        }

        [Fact()]
        public async void GetAccountDetailsTest_AlwaysReturnsA200()
        {
            string userId = "Test-ID";

            Mock<Genre> genre = new Mock<Genre>();
            Mock<Venue> venue = new Mock<Venue>();

            List<UserGenre> genres = new List<UserGenre>();
            List<UserVenue> venues = new List<UserVenue>();


            Mock<UserGenre> userGenre = new Mock<UserGenre>();
            genre.SetupGet(x => x.Name).Returns("Rock");
            genres.Add(userGenre.Object);

            Mock<UserVenue> userVenue = new Mock<UserVenue>();
            venue.SetupGet(x => x.Name).Returns("Blue Hut");
            venues.Add(userVenue.Object);
            

            ApplicationUser returnedUser = new ApplicationUser
            {
                Genres = genres,
                Venues = venues,
                Name = "Test Name",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(returnedUser);

            ObjectResult result = await subject.GetAccountDetails();

            Assert.Equal(200, result.StatusCode);
        }


    }
}