namespace Jobzy.Services.Interfaces
{
    using Stripe;

    public interface IStripeManager
    {
        Account CreateAccount(string name, string email);

        Account GetAccount(string accountId);

        PaymentIntent CreatePaymentIntent(int amount, string accountId, string contractId);

        long GetFreelancerBalanceAmount(string userId);
    }
}
