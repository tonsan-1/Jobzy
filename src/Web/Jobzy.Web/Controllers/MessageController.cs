namespace Jobzy.Web.Controllers
{
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class MessageController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        public MessageController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [Route("/Messages")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult Messages()
        {
            IEnumerable<UsersListViewModel> users = new List<UsersListViewModel>();

            return this.View(users);
        }

        [HttpPost]
        [Route("/Messages")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        [IgnoreAntiforgeryToken]
        public IActionResult Messages([FromBody]string query)
        {
            IEnumerable<UsersListViewModel> users = new List<UsersListViewModel>();

            if (query == string.Empty)
            {
                return this.Json(users);
            }

            users = this.freelancePlatform.MessageManager.GetMatchingUsers(query);

            return this.Json(users);
        }
    }
}
