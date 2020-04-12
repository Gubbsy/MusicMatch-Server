using Abstraction.Models;
using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
        public async void SignInTest_AlwaysReturnsA200()
        {
            Requests.SignIn request = new Requests.SignIn
            {
                Credential = "Test@Test.com",
                Password = "abcABC123;",
            };

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser returnedUser = new ApplicationUser
            {
                Genres = genres,
                Venues = venues,
                Name = "Test Name",
                Picture = "Test picture",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            signInRepository.Setup(x => x.SignIn(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(returnedUser);

            ObjectResult result = await subject.SignIn(request);

            Assert.Equal(200, result.StatusCode);
        }

        [Fact()]
        public async Task SignInTest_SignInRepository_SignIn_IsCalledOnce()
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
        public async void SignOut_SignInRepository_SignOut_IsCalldOnce()
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
                Picture = "Test Picture",
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
        public async void UpdateAccountDetails_UserRepository_UpdateAccountDetails_IsCalledOnce()
        {
            string userId = "Test-ID";
            Requests.UpdateAccountDetails request = new Requests.UpdateAccountDetails
            {
                Genres = new string[] { "Rock", "Roll" },
                Venues = new string[] { "Blue Hut", "Nug" },
                Name = "Test Name",
                Picture = "Test picture",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.UpdateAccountDetails(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>()));

            ObjectResult result = await subject.UpdateAccountDetails(request);

            userRepository.Verify(x => x.UpdateAccountDetails(userId, request.Genres, request.Venues, request.Name, request.Picture, request.Bio, request.LookingFor, request.MatchRadius, request.Lat, request.Lon), Times.Once);
        }

        [Fact()]
        public async void GetAccountDetailsTest_AlwaysReturnsA200()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser returnedUser = new ApplicationUser
            {
                Genres = genres,
                Venues = venues,
                Name = "Test Name",
                Picture = "Test picture",
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

        [Fact()]
        public async void GetAccountDetailsTest_SessionService_GetCurrenUserID_IsCalledOnce()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser returnedUser = new ApplicationUser
            {
                Genres = genres,
                Venues = venues,
                Name = "Test Name",
                Picture = "Test picture",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(returnedUser);

            ObjectResult result = await subject.GetAccountDetails();

            sessionService.Verify(x => x.GetCurrentUserId(), Times.Once);
        }

        [Fact()]
        public async void GetAccountDetailsTest_UserRepository_GetUserAccount_IsCalledOnce()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser returnedUser = new ApplicationUser
            {
                Genres = genres,
                Venues = venues,
                Name = "Test Name",
                Picture = "Test picture",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50,
                Lon = 23,
                MatchRadius = 40,
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(returnedUser);

            ObjectResult result = await subject.GetAccountDetails();

            userRepository.Verify(x => x.GetUserAccount(userId), Times.Once);
        }

    }
}