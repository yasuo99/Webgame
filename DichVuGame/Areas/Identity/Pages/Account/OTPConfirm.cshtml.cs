using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Data;
using DichVuGame.Extensions;
using DichVuGame.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DichVuGame.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class OTPConfirmModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly ApplicationDbContext _db;
        public OTPConfirmModel(SignInManager<IdentityUser> signInManager,ApplicationDbContext db)
        {
            _signinManager = signInManager;
            _db = db;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            [Required,Display(Name = "OTP")]
            public int OTP { get; set; }
        }
        [TempData]
        public string ErrorMessage { get; set; }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }


            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signinManager.GetExternalAuthenticationSchemesAsync()).ToList();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var otpSession = HttpContext.Session.Get<OTPSession>("OTP");
            if(Input.OTP.Equals(otpSession.OTP))
            {
                var user = _db.ApplicationUsers.Where(u => u.Email == otpSession.Email).FirstOrDefault();
                await _signinManager.PasswordSignInAsync(otpSession.Email, otpSession.Password,false, lockoutOnFailure: true);
            }    
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}
