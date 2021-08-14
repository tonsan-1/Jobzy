using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Jobzy.Web.Areas.Identity.IdentityHostingStartup))]

namespace Jobzy.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
