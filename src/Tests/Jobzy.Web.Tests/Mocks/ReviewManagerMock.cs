namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Reviews;
    using Moq;

    public class ReviewManagerMock
    {
        public static IReviewManager Instance
        {
            get
            {
                var reviewManagerMock = new Mock<IReviewManager>();

                reviewManagerMock.Setup(
                        x =>
                            x.CreateAsync(It.IsAny<ReviewInputModel>()))
                    .Returns(Task.CompletedTask);

                reviewManagerMock.Setup(
                        x =>
                        x.GetReviewsCount(It.IsAny<string>()))
                    .Returns(76);

                reviewManagerMock.Setup(
                        x =>
                            x.GetAllUserReviewsAsync<ReviewsListViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new List<ReviewsListViewModel>()
                        {
                            new ReviewsListViewModel()
                            {
                                Rating = 3,
                                Text = "Testing reviews",
                            },
                        });

                return reviewManagerMock.Object;
            }
        }
    }
}
