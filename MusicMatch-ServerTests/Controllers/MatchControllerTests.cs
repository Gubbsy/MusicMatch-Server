using Xunit;
using MusicMatch_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Abstraction.Services;
using Abstraction.Repositories;
using Abstraction.Models;
using Microsoft.AspNetCore.Mvc;

namespace MusicMatch_Server.Controllers.Tests
{
    public class MatchControllerTests
    {

        private Mock<ISessionService> sessionService;
        private Mock<IUserRepository> userRepository;
        private Mock<IMatchRepository> matchRepository;
        private MatchController subject;

        public MatchControllerTests()
        {
            sessionService = new Mock<ISessionService>();
            userRepository = new Mock<IUserRepository>();
            matchRepository = new Mock<IMatchRepository>();

            subject = new MatchController(matchRepository.Object, sessionService.Object, userRepository.Object);
        }

        [Fact()]
        public async void GetMatches_AlwaysReturnsA200()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser user = new ApplicationUser()
            {
                Id = "Test-ID",
                Email = "Test@email.com",
                Name = "Test-Name",
                UserName = "Test-UserName",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50.3755,
                Lon = 4.1427,
                MatchRadius = 50,
                Genres = genres,
                Venues = venues,
            };

            List<string> matchIds = new List<string> 
            { "Test-Match-Id-1", 
              "Test-Match-Id-2",
              "Test-Match-Id-3", 
              "Test-Match-Id-4" 
            };

            string role = "artist";


            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            matchRepository.Setup(x => x.GetMatches(It.IsAny<string>())).Returns(matchIds);
            userRepository.Setup(x => x.GetAcountRole(It.IsAny<string>())).ReturnsAsync(role);

            ObjectResult result = await subject.GetMatches();

            Assert.Equal(200, result.StatusCode);
        }

        [Fact()]
        public async void GetMatches_SessionService_GetCurrentUserID_IsCalledOnce()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser user = new ApplicationUser()
            {
                Id = "Test-ID",
                Email = "Test@email.com",
                Name = "Test-Name",
                UserName = "Test-UserName",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50.3755,
                Lon = 4.1427,
                MatchRadius = 50,
                Genres = genres,
                Venues = venues,
            };

            List<string> matchIds = new List<string>
            { "Test-Match-Id-1",
              "Test-Match-Id-2",
              "Test-Match-Id-3",
              "Test-Match-Id-4"
            };

            string role = "artist";


            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            matchRepository.Setup(x => x.GetMatches(It.IsAny<string>())).Returns(matchIds);
            userRepository.Setup(x => x.GetAcountRole(It.IsAny<string>())).ReturnsAsync(role);

            ObjectResult result = await subject.GetMatches();

            sessionService.Verify(x => x.GetCurrentUserId(), Times.Once);
        }

        [Fact()]
        public async void GetMatches_UserRepository_GetUserAccount_IsCalledOnce_WithCurrentUserID()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser user = new ApplicationUser()
            {
                Id = "Test-ID",
                Email = "Test@email.com",
                Name = "Test-Name",
                UserName = "Test-UserName",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50.3755,
                Lon = 4.1427,
                MatchRadius = 50,
                Genres = genres,
                Venues = venues,
            };

            List<string> matchIds = new List<string>
            { "Test-Match-Id-1",
              "Test-Match-Id-2",
              "Test-Match-Id-3",
              "Test-Match-Id-4"
            };

            string role = "artist";


            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            matchRepository.Setup(x => x.GetMatches(It.IsAny<string>())).Returns(matchIds);
            userRepository.Setup(x => x.GetAcountRole(It.IsAny<string>())).ReturnsAsync(role);

            ObjectResult result = await subject.GetMatches();

            userRepository.Verify(x => x.GetUserAccount(userId), Times.Once);
        }

        [Fact()]
        public async void GetMatches_UserRepository_GetUserAccount_IsCalledOnce_WithReturnedUserID()
        {
            string userId = "Test-ID";

            List<UserGenre> genres = new List<UserGenre>() { };
            List<UserVenue> venues = new List<UserVenue>() { };

            ApplicationUser user = new ApplicationUser()
            {
                Id = "Test-ID",
                Email = "Test@email.com",
                Name = "Test-Name",
                UserName = "Test-UserName",
                Bio = "Test Bio",
                LookingFor = "Test Looking For",
                Lat = 50.3755,
                Lon = 4.1427,
                MatchRadius = 50,
                Genres = genres,
                Venues = venues,
            };

            List<string> matchIds = new List<string>
            { "Test-Match-Id-1",
              "Test-Match-Id-2",
              "Test-Match-Id-3",
              "Test-Match-Id-4"
            };

            string role = "artist";


            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            matchRepository.Setup(x => x.GetMatches(It.IsAny<string>())).Returns(matchIds);
            userRepository.Setup(x => x.GetAcountRole(It.IsAny<string>())).ReturnsAsync(role);

            ObjectResult result = await subject.GetMatches();

            userRepository.Verify(x => x.GetUserAccount(user.Id), Times.Once);
        }
    }
}