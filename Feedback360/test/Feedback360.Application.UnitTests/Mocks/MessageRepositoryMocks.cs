using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class MessageRepositoryMocks
    {
        public static Mock<IMessageRepository> GetMessageRepository()
        {
            var messages = new List<Message>
            {
                new Message
                {
                    MessageId = Guid.NewGuid(),
                    Code = "1",
                    MessageContent = "{PropertyName} is required.",
                    Language = "en",
                    Type = Message.MessageType.Error
                },
                new Message
                {
                    MessageId = Guid.NewGuid(),
                    Code = "2",
                    MessageContent = "{PropertyName} must not exceed {MaxLength} characters.",
                    Language = "en",
                    Type = Message.MessageType.Error
                },
                new Message
                {
                    MessageId = Guid.NewGuid(),
                    Code = "3",
                    MessageContent = "An event with the same name and date already exists.",
                    Language = "en",
                    Type = Message.MessageType.Error
                }
            };

            var mockMessageRepository = new Mock<IMessageRepository>();

            mockMessageRepository.Setup(repo => repo.GetMessage(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(
                (string Code, string Lang) =>
                {
                    return messages.FirstOrDefault(x => x.Code == Code && x.Language == Lang);
                });

            return mockMessageRepository;
        }
    }
}
