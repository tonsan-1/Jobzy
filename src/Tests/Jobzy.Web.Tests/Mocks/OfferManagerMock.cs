namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Offers;
    using Moq;

    public class OfferManagerMock
    {
        public static IOfferManager Instance
        {
            get
            {
                var offerManagerMock = new Mock<IOfferManager>();

                offerManagerMock.Setup(
                        x =>
                        x.AcceptOfferAsync(It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                offerManagerMock.Setup(
                        x =>
                        x.CreateAsync(It.IsAny<OfferInputModel>()))
                    .Returns(Task.CompletedTask);

                offerManagerMock.Setup(
                        x =>
                        x.DeleteOfferAsync(It.IsAny<string>()))
                    .Returns(Task.CompletedTask);

                offerManagerMock.Setup(
                        x =>
                        x.GetActiveOffersCount(It.IsAny<string>()))
                    .Returns(55);

                offerManagerMock.Setup(
                        x =>
                        x.GetAllOffersCount())
                    .Returns(66);

                offerManagerMock.Setup(
                        x =>
                        x.GetSentOffersCount(It.IsAny<string>()))
                    .Returns(67);

                offerManagerMock.Setup(
                        x =>
                            x.GetJobOffersAsync<JobOfferViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<JobOfferViewModel>()
                        {
                            new JobOfferViewModel()
                            {
                                Id = "Testing",
                            },
                        });

                offerManagerMock.Setup(
                        x =>
                            x.GetUserJobOffersAsync<UserOffersViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<UserOffersViewModel>()
                        {
                            new UserOffersViewModel()
                            {
                                Id = "TestTest",
                            },
                        });

                return offerManagerMock.Object;
            }
        }
    }
}
