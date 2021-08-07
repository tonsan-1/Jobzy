namespace Jobzy.Web
{
    using System.Reflection;
    using CloudinaryDotNet;
    using Jobzy.Data;
    using Jobzy.Data.Common;
    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Data.Repositories;
    using Jobzy.Data.Seeding;
    using Jobzy.Services;
    using Jobzy.Services.Data;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Services.Messaging;
    using Jobzy.Web.Hubs;
    using Jobzy.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IJobManager, JobManager>();
            services.AddTransient<IFreelancePlatform, FreelancePlatform>();
            services.AddTransient<IOfferManager, OfferManager>();
            services.AddTransient<IContractManager, ContractManager>();
            services.AddTransient<IProfileManager, ProfileManager>();
            services.AddTransient<IStripeManager, StripeManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IReviewManager, ReviewManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Stripe setup
            StripeConfiguration.ApiKey = this.configuration.GetSection("Stripe")["ApiKey"];

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapHub<MessageHub>("/MessageHub");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
