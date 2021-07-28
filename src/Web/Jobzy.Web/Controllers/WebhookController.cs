namespace Jobzy.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;

    public class WebhookController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        public WebhookController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [HttpPost("webhook")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ProcessWebhookEvent()
        {
            var json = await new StreamReader(this.HttpContext.Request.Body).ReadToEndAsync();

            const string endpointSecret = "whsec_WH8v4S0IVlge4Px1fUCPuMYv4LGbVHCP";
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, this.Request.Headers["Stripe-Signature"], endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    var connectedAccountId = stripeEvent.Account;
                    var contractId = paymentIntent.Metadata["contractId"];
                    var contract = this.freelancePlatform.ContractManager.GetContractById(contractId);

                    await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Finished, contractId);
                    await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.Closed, contract.OfferJobId);

                    // TODO:notify both parties that the contract is succesfully completed
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest();
            }
        }
    }
}