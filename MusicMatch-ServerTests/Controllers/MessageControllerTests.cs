using Abstraction.Repositories;
using Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MusicMatch_Server.Controllers.Tests
{
    public class MessageControllerTests
    {
        private Mock<ISessionService> sessionService;
        private Mock<IMessageRepository> messageRepository;

        private MessageController subject;

        public MessageControllerTests()
        {
            sessionService = new Mock<ISessionService>();
            messageRepository = new Mock<IMessageRepository>();

            subject = new MessageController(messageRepository.Object, sessionService.Object);
        }

        [Fact()]
        public async void GetPreviousMessagesTest_ReturnsA400_OnNullRequest()
        {
            Requests.GetPreviouseMessages request = null;

            ObjectResult result = await subject.GetPreviousMessages(request);

            Assert.Equal(400, result.StatusCode);
        }

        [Fact()]
        public async void GetPreviousMessagesTest_AlwaysReturnsA204()
        {
            Requests.GetPreviouseMessages request = new Requests.GetPreviouseMessages
            {
                RecipientId = "TestID"
            };

            ObjectResult result = await subject.GetPreviousMessages(request);

            Assert.Equal(200, result.StatusCode);
        }

        [Fact()]
        public async void GetPreviousMessagesTest_GetCurrentUserId_IsCalledOnce()
        {
            Requests.GetPreviouseMessages request = new Requests.GetPreviouseMessages
            {
                RecipientId = "TestID"
            };

            sessionService.Setup(x => x.GetCurrentUserId());

            ObjectResult result = await subject.GetPreviousMessages(request);

            sessionService.Verify(x => x.GetCurrentUserId(), Times.Once);
        }

        [Fact()]
        public async void GetPreviousMessagesTest_RetrieveMessage_IsCalledOnce()
        {
            string currentUserId = "TestCurrentUserId";
            Requests.GetPreviouseMessages request = new Requests.GetPreviouseMessages
            {
                RecipientId = "TestID"
            };

            sessionService.Setup(x => x.GetCurrentUserId()).Returns(currentUserId);

            messageRepository.Setup(x => x.RetrieveMessage(It.IsAny<string>(), It.IsAny<string>()));

            ObjectResult result = await subject.GetPreviousMessages(request);

            messageRepository.Verify(x => x.RetrieveMessage(currentUserId, request.RecipientId), Times.Once);
        }
    }
}