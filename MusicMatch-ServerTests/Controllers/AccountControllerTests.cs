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

namespace MusicMatch_Server.Controllers.Tests
{
    public class AccountControllerTests
    {
        private Mock<UserRepository> userRepository;
        private Mock<SignInRepository> signInRepository;
        private Mock<HttpContextAccessor> httpContextAccessor;

        private AccountController subject;

        public AccountControllerTests()
        {
            userRepository = new Mock<UserRepository>();
            signInRepository = new Mock<SignInRepository>();
            httpContextAccessor = new Mock<HttpContextAccessor>();

            subject = new AccountController(userRepository.Object, signInRepository.Object, httpContextAccessor.Object);
        }

        [Fact()]
        public async Task CreateAccount_AlwaysReturns_NoContent()
        {
           //  Mock<AppDbContext>
            Mock<Requests.CreateAccount> request = new Mock<Requests.CreateAccount>();
            
            ObjectResult result = await subject.CreateAccount(request.Object);

            Assert.Equal(204, result.StatusCode);
        }

        [Fact()]
        public void CreateTestTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SignInTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SignOutTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void UpdateAccountDetailsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetAccountDetailsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}