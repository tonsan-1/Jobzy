namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class JobsControllerTests
    {
        [Theory]
        [InlineData("testId")]
        public void IndexShouldReturnCorrectViewWhenJobIsNotNullAndUserIsEmployer(string testId)
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.Index(testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<SingleJobViewModel>()
                        .Passing(
                            model =>
                                model.Id == "123" &&
                                model.Title == "test"));

        [Theory]
        [InlineData("testId")]
        public void IndexShouldReturnCorrectViewWhenJobIsNotNullAndUserIsFreelancer(string testId)
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Freelancer")))
                .Calling(x => x.Index(testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<SingleJobViewModel>()
                        .Passing(
                            model =>
                                model.Id == "123" &&
                                model.Title == "test"));

        [Fact]
        public void AllShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Freelancer")))
                .Calling(x => x.All(
                    new AllJobsQueryModel()
                {
                    Sorting = Sorting.Newest,
                    CurrentPage = 1,
                }))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<AllJobsQueryModel>()
                        .Passing(
                            model =>
                                model.Jobs.Any(
                                    x =>
                                        x.Id == "123" &&
                                        x.Title == "Test" &&
                                        x.EmployerFirstName == "Vanko" &&
                                        x.EmployerLastName == "Edno")));

        [Fact]
        public void MyJobsShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Employer()
                            {
                                Id = "test123",
                                UserName = "tonsan1",
                                FirstName = "Test",
                                Roles = new List<IdentityUserRole<string>>()
                                {
                                    new IdentityUserRole<string>()
                                    {
                                        RoleId = "4dc5dea8-00cc-44e4-b626-451ce0b6c0ae",
                                    },
                                },
                            })
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.MyJobs())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<IEnumerable<UserJobsListViewModel>>()
                        .Passing(
                            model =>
                                model.Any(
                                    x =>
                                        x.Id == "123" &&
                                        x.Title == "Test" &&
                                        x.Budget == 2000)));

        [Fact]
        public void AddJobShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Employer()
                            {
                                Id = "test123",
                                UserName = "tonsan1",
                                FirstName = "Test",
                                Roles = new List<IdentityUserRole<string>>()
                                {
                                    new IdentityUserRole<string>()
                                    {
                                        RoleId = "4dc5dea8-00cc-44e4-b626-451ce0b6c0ae",
                                    },
                                },
                            })
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.AddJob())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<JobInputModel>());

        [Fact]
        public void AddJobPostShouldReturnRedirectWithValidStateWhenUserIsEmployer()
            => MyController<JobsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Employer()
                            {
                                Id = "test123",
                                UserName = "tonsan1",
                                FirstName = "Test",
                                Roles = new List<IdentityUserRole<string>>()
                                {
                                    new IdentityUserRole<string>()
                                    {
                                        RoleId = "4dc5dea8-00cc-44e4-b626-451ce0b6c0ae",
                                    },
                                },
                            })
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.AddJob(new JobInputModel()
                {
                    Budget = 2000,
                    CategoryId = "test",
                    Description = "testasdasda",
                    Title = "testasdasdasd",
                    Categories = new List<CategoriesListViewModel>(),
                }))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyJobs", "Jobs");
    }
}
