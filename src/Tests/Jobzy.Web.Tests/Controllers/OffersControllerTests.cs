namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class OffersControllerTests
    {
        [Fact]
        public void AllShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<OffersController>
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
                .Calling(x => x.All("testId"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<IEnumerable<JobOfferViewModel>>()
                        .Passing(
                            model =>
                            model.Any(x => x.Id == "Testing")));

        [Fact]
        public void MyOffersShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<OffersController>
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
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Freelancer")))
                .Calling(x => x.MyOffers())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<IEnumerable<UserOffersViewModel>>()
                        .Passing(
                            model =>
                            model.Any(x => x.Id == "TestTest")));

        [Fact]
        public void AddOfferShouldReturnCorrectRedirectWhenEverythingIsValid()
            => MyController<OffersController>
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
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Freelancer")))
                .Calling(
                    x => x.AddOffer(
                        new OfferInputModel()
                        {
                            JobId = "TestId",
                            UserId = "TestId",
                            DeliveryDays = 5,
                            FixedPrice = 2500,
                        }))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyOffers", "Offers");

        [Fact]
        public void DeleteOfferShouldReturnCorrectRedirectWhenUserIsFreelancer()
            => MyController<OffersController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .WithIdentifier("test123")
                                .WithUsername("tonsan1")
                                .InRole("Freelancer")))
                .Calling(x => x.DeleteOffer("testOffer"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyOffers", "Offers");

        [Fact]
        public void DeleteOfferShouldReturnCorrectRedirectWhenUserIsEmployer()
            => MyController<OffersController>
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
                .Calling(x => x.DeleteOffer("testOffer"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyJobs", "Jobs");
    }
}
