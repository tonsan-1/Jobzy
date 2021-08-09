namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class PaymentController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        public PaymentController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [Route("/Checkout/")]
        public async Task<IActionResult> Checkout(string id)
        {
            var contract = await this.freelancePlatform.ContractManager.GetContractByIdAsync<SingleContractViewModel>(id);

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
