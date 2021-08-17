namespace Jobzy.Web.Tests
{
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.Tests.Mocks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceTransient<IJobManager>(_ => JobManagerMock.Instance);
            services.ReplaceTransient<IContractManager>(_ => ContractManagerMock.Instance);
            services.ReplaceTransient<ICategoryManager>(_ => CategoryManagerMock.Instance);
            services.ReplaceTransient<IFileManager>(_ => FileManagerMock.Instance);
            services.ReplaceTransient<IMessageManager>(_ => MessageManagerMock.Instance);
            services.ReplaceTransient<INotificationManager>(_ => NotificationManagerMock.Instance);
            services.ReplaceTransient<IOfferManager>(_ => OfferManagerMock.Instance);
            services.ReplaceTransient<IReviewManager>(_ => ReviewManagerMock.Instance);
            services.ReplaceTransient<IUserManager>(_ => UserManagerMock.Instance);
            services.ReplaceTransient<IStripeManager>(_ => StripeManagerMock.Instance);
        }
    }
}
