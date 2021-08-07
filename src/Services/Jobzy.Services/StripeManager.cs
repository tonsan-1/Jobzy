namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jobzy.Common;
    using Jobzy.Services.Interfaces;
    using Stripe;

    public class StripeManager : IStripeManager
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
            return service.Create(options);
        }

        public PaymentIntent CreatePaymentIntent(int amount, string accountId, string contractId)
        {
            StripeConfiguration.ApiKey = GlobalConstants.StripeConfigurationKey;

            var service = new PaymentIntentService();
            var createOptions = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Amount = amount,
                Currency = "usd",
                ApplicationFeeAmount = GlobalConstants.PlatformFeeAmount,
                Metadata = new Dictionary<string, string>()
                {
                    ["contractId"] = contractId,
                },
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;
            return service.Create(createOptions, requestOptions);
        }

        public Account GetAccount(string accountId)
        {
            StripeConfiguration.ApiKey = GlobalConstants.StripeConfigurationKey;

            var service = new AccountService();
            var account = service.Get(accountId);

            return account;
        }

        public long GetFreelancerBalanceAmount(string userId)
        {
            StripeConfiguration.ApiKey = GlobalConstants.StripeConfigurationKey;

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = userId;
            var service = new BalanceService();
            Balance balance = service.Get(requestOptions);

            return balance.Available
                .Where(x => x.Currency == "usd")
                .Select(x => x.Amount)
                .FirstOrDefault();
        }
    }
}
