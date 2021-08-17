using System.Linq;
using Jobzy.Web.ViewModels.Users;

namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.ViewModels.Users.Employers;
    using Jobzy.Web.ViewModels.Users.Freelancers;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class UsersControllerTests
    {
        [Fact]
        public void EmployerShouldReturnCorrectViewWhenUserIdIsValidEmployer()
            => MyController<UsersController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Employer()
                            {
                                Id = "TestEmployer",
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
                .Calling(x => x.Employer("TestEmployer"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                    .WithModelOfType<EmployerViewModel>()
                    .Passing(model => model.Id == "TestEmployer"));

        [Fact]
        public void FreelancerShouldReturnCorrectViewWhenUserIdIsValidFreelancer()
            => MyController<UsersController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Freelancer()
                            {
                                Id = "TestFreelancer",
                                Roles = new List<IdentityUserRole<string>>()
                                {
                                    new IdentityUserRole<string>()
                                    {
                                        RoleId = "e41192c4-affc-4596-a988-8426e36d4b28",
                                    },
                                },
                            })
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.Freelancer("TestFreelancer"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<FreelancerViewModel>()
                        .Passing(model => model.Id == "TestFreelancer"));

        [Fact]
        public void AllFreelancersShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<UsersController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.AllFreelancers(new AllFreelancersQueryModel()))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<AllFreelancersQueryModel>()
                        .Passing(
                            model =>
                                model.Freelancers.Any(
                                    x =>
                                        x.Id == "TestFreelancer")));

        [Fact]
        public void SettingsShouldReturnCorrectView()
            => MyController<UsersController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Employer")))
                .Calling(x => x.Settings())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<UserSettingsViewModel>());
    }
}
