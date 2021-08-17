namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Users;
    using Jobzy.Web.ViewModels.Users.Employers;
    using Jobzy.Web.ViewModels.Users.Freelancers;
    using Moq;

    public class UserManagerMock
    {
        public static IUserManager Instance
        {
            get
            {
                var userManagerMock = new Mock<IUserManager>();

                userManagerMock.Setup(
                        x =>
                        x.GetAllFreelancersCount())
                    .Returns(43);

                userManagerMock.Setup(
                        x =>
                            x.UpdateUserInfoAsync(It.IsAny<UserInfoInputModel>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                userManagerMock.Setup(
                        x =>
                            x.UpdateUserOnlineStatusAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                userManagerMock.Setup(
                        x =>
                            x.UpdateUserProfilePictureAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                userManagerMock.Setup(
                        x =>
                            x.GetAllFreelancersAsync<FreelancerViewModel>(
                                It.IsAny<int>(),
                                It.IsAny<string>(),
                                It.IsAny<Sorting>(),
                                It.IsAny<int>()))
                    .ReturnsAsync(
                        new List<FreelancerViewModel>()
                        {
                            new FreelancerViewModel()
                            {
                                Id = "TestFreelancer",
                            },
                        });

                userManagerMock.Setup(
                        x =>
                            x.GetEmployerByIdAsync<EmployerViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new EmployerViewModel()
                        {
                            Id = "TestEmployer",
                        });

                userManagerMock.Setup(
                        x =>
                            x.GetFreelancerByIdAsync<FreelancerViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new FreelancerViewModel()
                        {
                            Id = "TestFreelancer",
                        });

                userManagerMock.Setup(
                        x =>
                            x.GetUserByIdAsync<BaseUserViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new BaseUserViewModel()
                        {
                            Id = "TestUser",
                        });

                return userManagerMock.Object;
            }
        }
    }
}
