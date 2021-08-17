namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Contracts;
    using Moq;

    public class ContractManagerMock
    {
        public static IContractManager Instance
        {
            get
            {
                var contractManagerMock = new Mock<IContractManager>();

                contractManagerMock.Setup(
                        x =>
                        x.CreateAsync(It.IsAny<string>()))
                    .ReturnsAsync("test");

                contractManagerMock.Setup(
                        x =>
                        x.GetFinishedContractsCount(It.IsAny<string>()))
                    .Returns(12);

                contractManagerMock.Setup(
                        x =>
                            x.GetOngoingContractsCount(It.IsAny<string>()))
                    .Returns(22);

                contractManagerMock.Setup(
                        x =>
                            x.SetContractStatusAsync(ContractStatus.Ongoing, It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                contractManagerMock.Setup(
                        x =>
                            x.GetAllUserContractsAsync<UserContractsListViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<UserContractsListViewModel>()
                        {
                            new UserContractsListViewModel()
                            {
                                Id = "Test",
                                OfferJobTitle = "TestJobTitle",
                            },
                        });

                contractManagerMock.Setup(
                        x =>
                            x.GetContractByIdAsync<SingleContractViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new SingleContractViewModel()
                        {
                            Id = "Test",
                            OfferJobTitle = "TestJobTitle",
                            FreelancerId = "TestId",
                            EmployerId = "testing",
                        });

                return contractManagerMock.Object;
            }
        }
    }
}
