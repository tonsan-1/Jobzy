namespace Jobzy.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Jobzy";
        public const int PlatformFeeAmount = 500;

        public const string AdministratorRoleName = "Administrator";
        public const string EmployerRoleName = "Employer";
        public const string FreelancerRoleName = "Freelancer";

        public const string LocalHostHomePage = "https://localhost:44319/";
        public const string LocalHostRegisterPage = "https://localhost:44319/Register";

        public const string StripeConfigurationKey = "sk_test_51JECFYFoZWpnNT9SoQmIQCtE0rIU5ReNiY832f8UtiHsTtX1gMyiSCHTDqI6h8pn04MJ9hVBbouyUyan990c1DiS00VD11M1hR";
        public const string StripeTestOAuthLink = "https://connect.stripe.com/oauth/authorize?response_type=code&client_id=ca_Js11uG0YalpB7BhLIqd3BykfvFtCREI2&scope=read_write";

        // notification icons
        public const string OfferIcon = "icon-material-outline-gavel";
        public const string ContractIcon = "icon-material-outline-library-books";
        public const string JobIcon = "icon-material-outline-business-center";
        public const string PaymentIcon = "icon-feather-dollar-sign";
    }
}
