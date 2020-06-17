using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DichVuGame.Models;
using DichVuGame.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;

namespace DichVuGame.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required, Display(Name = "Tài khoản")]
            public string Username { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} tối thiểu {2} và tối đa {1} kí tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không chính xác.")]
            public string ConfirmPassword { get; set; }
            [Required, Display(Name = "Họ và tên")]
            public string Fullname { get; set; }
            [Display(Name = "SĐT")]
            [DataType(DataType.PhoneNumber)]
            public string Phone { get; set; }
            [Display(Name = "Giới tính")]
            public string Sex { get; set; }
            [Display(Name = "Địa chỉ")]
            public string Address { get; set; }
            [Display(Name = "Tài khoản quản lý")]
            public bool IsManager { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Fullname = Input.Fullname, PhoneNumber = Input.Phone, Address = Input.Address, User = Input.Username, Sex=Input.Sex };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Helper.ADMIN_ROLE))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Helper.ADMIN_ROLE));
                    }
                    if (!await _roleManager.RoleExistsAsync(Helper.MANAGER_ROLE))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Helper.MANAGER_ROLE));
                    }
                    if (!await _roleManager.RoleExistsAsync(Helper.CUSTOMER_ROLE))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Helper.CUSTOMER_ROLE));
                    }
                    if(Input.IsManager)
                    {
                        await _userManager.AddToRoleAsync(user, Helper.MANAGER_ROLE);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Helper.CUSTOMER_ROLE);
                    }
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation("User created a new account with password.");

                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
