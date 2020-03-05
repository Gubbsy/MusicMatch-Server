﻿using Xunit;
using MusicMatch_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Abstraction.Repositories;
using Abstraction.Services;
using Moq;
using Abstraction.Models;
using Microsoft.AspNetCore.Mvc;

namespace MusicMatch_Server.Controllers.Tests
{
    public class SuggestionsControllerTests
    {
        private Mock<IUserRepository> userRepository;
        private Mock<ISuggestionsRepository> suggesionsRepository;
        private Mock<ISessionService> sessionService;
        private SuggestionsController subject;

        public SuggestionsControllerTests()
        {
            userRepository = new Mock<IUserRepository>();
            suggesionsRepository = new Mock<ISuggestionsRepository>();
            sessionService = new Mock<ISessionService>();

            subject = new SuggestionsController(userRepository.Object, sessionService.Object, suggesionsRepository.Object);
        }

        [Fact()]
        public async void GetSuggestions_AlwaysReturnsA200()
        {
            string userID = "Test-ID";

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

            List<ApplicationUser> matchesInRadius = new List<ApplicationUser>() { new ApplicationUser()
                {
                    Id = "Returned-ID",
                    Email = "Returned@email.com",
                    Name = "Returned-Name",
                    UserName = "Returned-UserName",
                    Bio = "Returned Bio",
                    LookingFor = "Returned Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                }, 
                new ApplicationUser()
                {
                    Id = "Returned2-ID",
                    Email = "Returned2@email.com",
                    Name = "Returned2-Name",
                    UserName = "Returned2-UserName",
                    Bio = "Returned2 Bio",
                    LookingFor = "Returned2 Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                }
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userID);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            suggesionsRepository.Setup(x => x.GetUsersInMatchRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(matchesInRadius);

            ObjectResult result =  await subject.GetSuggestions();

            Assert.Equal(200, result.StatusCode);
        }

        [Fact()]
        public async void GetSuggestions_SessionService_GetCurrentUserID_IsCalledOnce()
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

            List<ApplicationUser> matchesInRadius = new List<ApplicationUser>() { new ApplicationUser()
                {
                    Id = "Returned-ID",
                    Email = "Returned@email.com",
                    Name = "Returned-Name",
                    UserName = "Returned-UserName",
                    Bio = "Returned Bio",
                    LookingFor = "Returned Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                },
                new ApplicationUser()
                {
                    Id = "Returned2-ID",
                    Email = "Returned2@email.com",
                    Name = "Returned2-Name",
                    UserName = "Returned2-UserName",
                    Bio = "Returned2 Bio",
                    LookingFor = "Returned2 Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                }
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            suggesionsRepository.Setup(x => x.GetUsersInMatchRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(matchesInRadius);

            ObjectResult result = await subject.GetSuggestions();

            sessionService.Verify(x => x.GetCurrentUserId(), Times.Once);
        }

        [Fact()]
        public async void GetSuggestions_UserRepository_GetUserAccount_IsCalledOnce()
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

            List<ApplicationUser> matchesInRadius = new List<ApplicationUser>() { new ApplicationUser()
                {
                    Id = "Returned-ID",
                    Email = "Returned@email.com",
                    Name = "Returned-Name",
                    UserName = "Returned-UserName",
                    Bio = "Returned Bio",
                    LookingFor = "Returned Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                },
                new ApplicationUser()
                {
                    Id = "Returned2-ID",
                    Email = "Returned2@email.com",
                    Name = "Returned2-Name",
                    UserName = "Returned2-UserName",
                    Bio = "Returned2 Bio",
                    LookingFor = "Returned2 Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                }
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            suggesionsRepository.Setup(x => x.GetUsersInMatchRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(matchesInRadius);

            ObjectResult result = await subject.GetSuggestions();

            userRepository.Verify(x => x.GetUserAccount(userId), Times.Once);
        }


        [Fact()]
        public async void GetSuggestions_SuggestionRepository_GetUsersInMatchRadius_IsCalledOnce()
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

            List<ApplicationUser> matchesInRadius = new List<ApplicationUser>() { new ApplicationUser()
                {
                    Id = "Returned-ID",
                    Email = "Returned@email.com",
                    Name = "Returned-Name",
                    UserName = "Returned-UserName",
                    Bio = "Returned Bio",
                    LookingFor = "Returned Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                },
                new ApplicationUser()
                {
                    Id = "Returned2-ID",
                    Email = "Returned2@email.com",
                    Name = "Returned2-Name",
                    UserName = "Returned2-UserName",
                    Bio = "Returned2 Bio",
                    LookingFor = "Returned2 Looking For",
                    Lat = 50.3755,
                    Lon = 4.1427,
                    MatchRadius = 50,
                    Genres = genres,
                    Venues = venues,
                }
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(userId);
            userRepository.Setup(x => x.GetUserAccount(It.IsAny<string>())).ReturnsAsync(user);
            suggesionsRepository.Setup(x => x.GetUsersInMatchRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(matchesInRadius);

            ObjectResult result = await subject.GetSuggestions();

            suggesionsRepository.Verify(x => x.GetUsersInMatchRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }
    }
}