namespace Jobzy.Services.Interfaces
{
    using Jobzy.Common;
    using Stripe;

    public interface IStripeAccountManager
    {
        Account CreateAccount(string name, string email);
    }
}
