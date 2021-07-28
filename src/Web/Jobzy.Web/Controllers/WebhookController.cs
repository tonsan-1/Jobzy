namespace Jobzy.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Stripe;

    [ApiController]
    public class WebhookController : BaseController
    {
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

                    // handleSuccessfulPaymentIntent(connectedAccountId, paymentIntent);
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