namespace Jobzy.Web.Controllers
{
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class PaymentController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        public PaymentController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [Route("/Checkout/")]
        public IActionResult Checkout(string id)
        {
            var contract = this.freelancePlatform.ContractManager.GetContractById(id);

            if (contract is null)
            {
                return this.View("Error");
            }

            var recipientId = contract.FreelancerId;
            var paymentAmount = (int)contract.OfferFixedPrice * 100;
            var intent = this.freelancePlatform.StripeManager.CreatePaymentIntent(paymentAmount, recipientId, contract.Id);

            this.ViewData["ClientSecret"] = intent.ClientSecret;
            this.ViewData["CurrentUser"] = recipientId;

            return this.View(contract);
        }
    }
}
