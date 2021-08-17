namespace Jobzy.Web.Tests.Mocks
{
    using Jobzy.Services.Interfaces;
    using Moq;
    using Stripe;

    public class StripeManagerMock
    {
        public static IStripeManager Instance
        {
            get
            {
                var stripeManagerMock = new Mock<IStripeManager>();

                stripeManagerMock.Setup(
                        x =>
                        x.CreateAccount(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new Account());

                stripeManagerMock.Setup(
                        x =>
                            x.CreatePaymentIntent(
                                It.IsAny<int>(),
                                It.IsAny<string>(), 
                                It.IsAny<string>()))
                    .Returns(new PaymentIntent());

                stripeManagerMock.Setup(
                        x =>
                            x.GetAccount(It.IsAny<string>()))
                    .Returns(new Account());

                stripeManagerMock.Setup(
                        x =>
                            x.GetFreelancerBalanceAmount(It.IsAny<string>()))
                    .Returns("12345");

                return stripeManagerMock.Object;
            }
        }
    }
}
