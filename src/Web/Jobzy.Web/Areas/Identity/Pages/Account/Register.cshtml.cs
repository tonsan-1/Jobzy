namespace Jobzy.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Range(0, 1, ErrorMessage = "An invalid role has been chosen.")]
            public UserType UserType { get; set; }

            [Required]
            [StringLength(25, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(25, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(25, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Range(1, 40, ErrorMessage = "Choose a location.")]
            [Display(Name = "Location")]
            public Country Location { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

#pragma warning disable SA1201 // Elements should appear in the correct order
        public async Task OnGetAsync(string code)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                ApplicationUser user;

                switch (this.Input.UserType)
                {
                    case UserType.Freelancer:
                        user = new Freelancer();
                        break;
                    case UserType.Employer:
                        user = new Employer();
                        break;
                    default:
                        this.ModelState.AddModelError(null, "Choose your role.");
                        return this.Page();
                }

                var account =
                    this.freelancePlatform.StripeManager.CreateAccount(this.Input.FirstName, this.Input.Email);

                user.Id = account.Id;
                user.FirstName = this.Input.FirstName;
                user.LastName = this.Input.LastName;
                user.UserName = this.Input.Username;
                user.Email = this.Input.Email;
                user.Location = this.Input.Location;
                user.ProfileImageUrl = "https://res.cloudinary.com/jobzy/image/upload/v1627979027/user-avatar-placeholder_kkhpst.png";

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                await this.userManager.AddToRoleAsync(user, this.Input.UserType.ToString());

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var codeToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    codeToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(codeToken));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = codeToken, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
#pragma warning restore SA1117 // Parameters should be on same line or separate lines

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect("/");
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
