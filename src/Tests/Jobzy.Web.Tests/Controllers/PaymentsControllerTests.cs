using Jobzy.Web.ViewModels.Contracts;

namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PaymentsControllerTests
    {
        [Fact]
        public void CheckoutShouldReturnCorrectViewWithValidContract()
            => MyController<PaymentsController>
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
                .Calling(x => x.Checkout("testId"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<SingleContractViewModel>()
                        .Passing(model => model.Id == "Test"));
    }
}
