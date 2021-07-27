namespace Jobzy.Web.Controllers
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PaymentController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public PaymentController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Route("/Checkout/")]
        public IActionResult Checkout(string id)
        {
            var contract = this.freelancePlatform.ContractManager.GetContractById(id);
            var recipientId = contract.FreelancerId;
            var paymentAmount = (int)contract.OfferFixedPrice * 100;
            var intent = this.freelancePlatform.StripeManager.CreatePaymentIntent(paymentAmount, recipientId);

            this.ViewData["ClientSecret"] = intent.ClientSecret;
            this.ViewData["CurrentUser"] = recipientId;

            return this.View();
        }
    }
}
