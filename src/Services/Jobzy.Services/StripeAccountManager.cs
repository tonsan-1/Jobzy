namespace Jobzy.Services
{
    using System;

    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Stripe;

    public class StripeAccountManager : IStripeAccountManager
    {
        public Account CreateAccount(string name, string email)
        {
            StripeConfiguration.ApiKey = GlobalConstants.StripeConfigurationKey;

            var options = new AccountCreateOptions
            {
                Type = "custom",
                Email = email,
                Country = "US",
                BusinessType = "individual",
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Mcc = "5045",
                    Url = "https://bestcookieco.com",
                },
                Individual = new AccountIndividualOptions
                {
                    IdNumber = "000000000",
                    SsnLast4 = "0000",
                    FirstName = name,
                    LastName = "test",
                    Email = email,
                    Phone = "8888675309",
                    Address = new AddressOptions
                    {
                        Line1 = "address_full_match",
                        City = "Concord",
                        State = "NH",
                        Country = "US",
                        PostalCode = "03301",
                    },
                    Dob = new DobOptions
                    {
                        Year = 1901,
                        Day = 01,
                        Month = 01,
                    },
                },
                Capabilities = new AccountCapabilitiesOptions
                {
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions
                    {
                        Requested = true,
                    },
                    Transfers = new AccountCapabilitiesTransfersOptions
                    {
                        Requested = true,
                    },
                },
                ExternalAccount = "tok_mastercard_debit",
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    Date = DateTime.UtcNow,
                    Ip = "172.18.80.19",
                },
            };

            var service = new AccountService();
            var account = service.Create(options);

            return account;
        }
    }
}
