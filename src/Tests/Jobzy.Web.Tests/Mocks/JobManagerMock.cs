namespace Jobzy.Web.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Jobs;
    using Moq;

    public class JobManagerMock
    {
        public static IJobManager Instance
        {
            get
            {
                var jobManagerMock = new Mock<IJobManager>();

                jobManagerMock.Setup(
                        x =>
                        x.GetAllPostedJobsCount())
                    .Returns(5);

                jobManagerMock.Setup(
                        x =>
                        x.GetPostedJobsCount(It.IsAny<string>()))
                    .Returns(4);

                var inputModel = new JobInputModel()
                {
                    Title = "Test",
                    Budget = 2000,
                    CategoryId = "testCategory123",
                    Description = "yadada",
                };

                jobManagerMock.Setup(
                        x =>
                            x.CreateAsync(inputModel, "testUser"))
                    .Returns(Task.CompletedTask);

                jobManagerMock.Setup(
                        x =>
                        x.SetJobStatusAsync(JobStatus.InContract, "testJob"))
                    .Returns(Task.CompletedTask);

                jobManagerMock.Setup(
                        x => x.GetAllJobPostsAsync<AllJobsListViewModel>(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<Sorting>(),
                            It.IsAny<int>()))
                    .ReturnsAsync(
                        new List<AllJobsListViewModel>()
                    {
                        new AllJobsListViewModel()
                        {
                            Id = "123",
                            Title = "Test",
                            EmployerFirstName = "Vanko",
                            EmployerLastName = "Edno",
                        },
                    });

                jobManagerMock.Setup(
                        x =>
                            x.GetAllUserJobPostsAsync<UserJobsListViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(new List<UserJobsListViewModel>()
                    {
                        new UserJobsListViewModel()
                        {
                            Id = "123",
                            Title = "Test",
                            Budget = 2000,
                        },
                    });

                jobManagerMock.Setup(
                        x =>
                            x.GetJobByIdAsync<SingleJobViewModel>(It.IsAny<string>()))
                    .ReturnsAsync(
                        new SingleJobViewModel()
                        {
                            Id = "123",
                            Title = "test",
                        });

                return jobManagerMock.Object;
            }
        }
    }
}
