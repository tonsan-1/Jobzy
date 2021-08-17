namespace Jobzy.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Controllers;
    using Jobzy.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class MessagesControllerTests
    {
        [Fact]
        public void ConversationShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<MessagesController>
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
                .Calling(x => x.Conversation("test"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<UserConversationViewModel>()
                        .Passing(
                            model =>
                                model.Messages.Any(x => x.SenderId == "TestSender")));

        [Fact]
        public void ConversationShouldReturnCorrectViewWhenUserIsEmployer()
            => MyController<MessagesController>
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
                .Calling(x => x.Conversation("test"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View(
                    view => view
                        .WithModelOfType<UserConversationViewModel>()
                        .Passing(
                            model =>
                                model.Messages.Any(x => x.SenderId == "TestSender")));

        [Fact]
        public void AllShouldReturnCorrectViewWhenUserIsFreelancer()
            => MyController<MessagesController>
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
                .Calling(x => x.Conversation("test"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void NewMessageShouldReturnCorrectRedirectToActionWhenUserIsEmployer()
            => MyController<MessagesController>
                .Instance(
                    controller => controller
                        .WithDependencies(
                            From.Services<IFreelancePlatform>(),
                            From.Services<UserManager<ApplicationUser>>())
                        .WithData(
                            new Employer()
                            {
                                Id = "123test",
                                UserName = "tonsan2",
                                NormalizedUserName = "TONSAN2",
                                FirstName = "First",
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
                .Calling(
                    x => x.NewMessage(
                        new NewMessageInputModel()
                        {
                            RecipientUsername = "tonsan2",
                            Message = "Blablabla",
                        }))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Conversation", new { id = "123test" });

        [Fact]
        public void MarkAllMessagesAsReadShouldReturnJsonWhenUserIsFreelancer()
            => MyController<MessagesController>
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
                .Calling(x => x.MarkAllMessagesAsRead("testId"))
                .ShouldHave()
                .ActionAttributes(
                    attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Json();
    }
}
