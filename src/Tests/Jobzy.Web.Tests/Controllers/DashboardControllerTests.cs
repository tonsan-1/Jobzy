namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.ViewModels.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class DashboardControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<DashboardController>
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
                        .WithUser(user => user
                            .WithIdentifier("test123")
                            .WithUsername("tonsan1")
                            .InRole("Employer")))
                .Calling(x => x.Index())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                    .WithModelOfType<EmployerDashboardViewModel>()
                    .Passing(
                        model =>
                        model.FinishedContractsCount == 12 &&
                        model.JobPostedCount == 4 &&
                        model.Name == "Test" &&
                        model.OngoingContractsCount == 22 &&
                        model.ReviewsCount == 76));

        [Fact]
        public void IndexShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<DashboardController>
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
                                        RoleId = "e41192c4-affc-4596-a988-8426e36d4b28",
                                    },
                                },
                            })
                        .WithUser(user => user
                            .WithIdentifier("test123")
                            .WithUsername("tonsan1")
                            .InRole("Freelancer")))
                .Calling(x => x.Index())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                    .WithModelOfType<FreelancerDashboardViewModel>()
                    .Passing(
                        model =>
                        model.FinishedContractsCount == 12 &&
                        model.Name == "Test" &&
                        model.OngoingContractsCount == 22 &&
                        model.ReviewsCount == 76));
    }
}
