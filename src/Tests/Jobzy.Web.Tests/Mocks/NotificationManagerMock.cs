namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Notifications;
    using Moq;

    public class NotificationManagerMock
    {
        public static INotificationManager Instance
        {
            get
            {
                var notificationManagerMock = new Mock<INotificationManager>();

                notificationManagerMock.Setup(
                        x =>
                            x.CreateAsync(It.IsAny<Notification>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                notificationManagerMock.Setup(x =>
                        x.GetNotificationsCount(It.IsAny<string>()))
                    .Returns(44);

                notificationManagerMock.Setup(x =>
                        x.MarkAllNotificationsAsReadAsync(It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                notificationManagerMock.Setup(x =>
                        x.MarkNotificationAsReadAsync(It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                notificationManagerMock.Setup(
                        x =>
                            x.GetAllUserNotificationsAsync<UserNotificationViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<UserNotificationViewModel>()
                        {
                            new UserNotificationViewModel()
                            {
                               Id = "TestId",
                            },
                        });

                return notificationManagerMock.Object;
            }
        }
    }
}
