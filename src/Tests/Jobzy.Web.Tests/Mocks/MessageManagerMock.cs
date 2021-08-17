namespace Jobzy.Web.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages;
    using Jobzy.Web.ViewModels.Messages.AllConversations;
    using Moq;

    public class MessageManagerMock
    {
        public static IMessageManager Instance
        {
            get
            {
                var messageManagerMock = new Mock<IMessageManager>();

                messageManagerMock.Setup(
                        x =>
                            x.CreateAsync(
                                It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                messageManagerMock.Setup(
                        x =>
                            x.GetConversationLastMessageAsync(
                                It.IsAny<string>(),
                                It.IsAny<string>()))
                    .ReturnsAsync("TestLastMessage");

                messageManagerMock.Setup(
                        x =>
                            x.GetConversationLastMessageSentDateAsync(
                                It.IsAny<string>(),
                                It.IsAny<string>()))
                    .ReturnsAsync(new DateTime(2021, 08, 17));

                messageManagerMock.Setup(x =>
                        x.GetUnreadMessagesCount(It.IsAny<string>()))
                    .Returns(12);

                messageManagerMock.Setup(x =>
                        x.MarkAllMessagesAsReadAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                messageManagerMock.Setup(
                        x =>
                            x.GetAllUserConversationsAsync<AllUserConversationsViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<AllUserConversationsViewModel>()
                        {
                            new AllUserConversationsViewModel()
                            {
                                Id = "Test",
                                LastMessage = "Last",
                            },
                        });

                messageManagerMock.Setup(
                        x =>
                            x.GetMessagesAsync<UserMessageViewModel>(
                                It.IsAny<string>(),
                                It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<UserMessageViewModel>()
                        {
                            new UserMessageViewModel()
                            {
                                SenderId = "TestSender",
                            },
                        });

                return messageManagerMock.Object;
            }
        }
    }
}
