namespace Jobzy.Services
{
    using Jobzy.Common;
    using Jobzy.Services.Interfaces;

    using Stripe;

    public class StripeAccountManager : IStripeAccountManager
    {
        public Account CreateAccount(string name, string email, Country location)
        {
            StripeConfiguration.ApiKey = GlobalConstants.StripeConfigurationKey;

            var options = new AccountCreateOptions
            {
                Type = "standard",
                Email = email,
                Country = location.ToString(),
                BusinessType = "individual",
                Individual = new AccountIndividualOptions
                {
                    FirstName = name,
                    Email = email,
                }
            };

            var service = new AccountService();
            var account = service.Create(options);

            return account;
        }
    }
}
