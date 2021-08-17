namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.Tests.Mocks;
    using Jobzy.Web.ViewModels.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ContractsControllerTests
    {
        [Theory]
        [InlineData("testId")]
        public void IndexShouldReturnCorrectViewIfContractIsNotNull(string id)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Freelancer")))
                .Calling(x => x.Index(id))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<SingleContractViewModel>()
                        .Passing(
                            model =>
                                model.Id == "Test" &&
                                model.OfferJobTitle == "TestJobTitle"));

        [Fact]
        public void MyContractsShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.MyContracts())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<IEnumerable<UserContractsListViewModel>>()
                        .Passing(
                            model =>
                                model.Any(x =>
                                    x.Id == "Test" &&
                                    x.OfferJobTitle == "TestJobTitle")));

        [Fact]
        public void MyContractsShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Freelancer")))
                .Calling(x => x.MyContracts())
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<IEnumerable<UserContractsListViewModel>>()
                        .Passing(
                            model =>
                                model.Any(x =>
                                    x.Id == "Test" &&
                                    x.OfferJobTitle == "TestJobTitle")));

        [Theory]
        [InlineData("cancel", "testId")]
        public void ContractActionsShouldRedirectToCorrectActionWhenActionIsCancelAndUserIsEmployer(string cancel, string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.ContractActions(cancel, testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("CancelContract", "Contracts");

        [Theory]
        [InlineData("complete", "testId")]
        public void ContractActionsShouldRedirectToCorrectActionWhenActionIsCompleteAndUserIsEmployer(
            string complete,
            string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.ContractActions(complete, testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Checkout", "Payments", new { id = testId });

        [Theory]
        [InlineData(null, "testId")]
        public void ContractActionsShouldRedirectToCorrectActionWhenActionIsNullAndUserIsEmployer(
            string wrongAction,
            string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.ContractActions(wrongAction, testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .View("Error");

        [Theory]
        [InlineData("testId")]
        public void CompleteContractShouldRedirectToCorrectActionWhenAContractIsValidAndUserIsEmployer(string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.CompleteContract(testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyContracts", "Contracts");

        [Theory]
        [InlineData("testId")]
        public void CancelContractShouldRedirectToCorrectActionWhenAContractIsValidAndUserIsEmployer(string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.CancelContract(testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("MyContracts", "Contracts");

        [Theory]
        [InlineData(null, "testId")]
        public void UploadWorkShouldRedirectToCorrectActionWithFileWhenUserIsFreelancer(IFormFile test, string testId)
            => MyController<ContractsController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithUser(
                            user => user
                                .InRole("Employer")))
                .Calling(x => x.UploadWork(test, testId))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index", "Contracts", new { id = testId });
    }
}
