using Xunit;
using MusicMatch_Server.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Abstraction.Repositories;
using Moq;
using Abstraction.Models;
using Microsoft.AspNetCore.Mvc;

namespace MusicMatch_Server.Controllers.Tests
{
    public class VenueControlllerTests
    {
        private Mock<IVenueRepository> venueRepository;
        private VenueControlller subject;

        public VenueControlllerTests()
        {
            venueRepository = new Mock<IVenueRepository>();
            subject = new VenueControlller(venueRepository.Object);
        }

        [Fact()]
        public async void GetAllVenuesTest_VenueRepositroy_GetAllVenues_IsCalledOnce()
        {
            venueRepository.Setup(x => x.GetAllVenues());
            await subject.GetAllVenues();

            venueRepository.Verify(x => x.GetAllVenues(), Times.Once());
        }

        [Fact()]
        public async void GetAllVenuesTest_AlwaysReturns200()
        {
            List<Venue> venues = new List<Venue>() { };

            venueRepository.Setup(x => x.GetAllVenues()).ReturnsAsync(venues);
            ObjectResult result = await subject.GetAllVenues();

            Assert.Equal(200, result.StatusCode);
        }
    }
}