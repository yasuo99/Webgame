using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DichVuGame.Areas.Identity.Pages.Account
{
    [Area("Identity")]
    public class AccountConfirmController : Controller
    {
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountConfirmController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signinManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            IdentityUser user = _userManager.FindByIdAsync(userId).Result;
            IdentityResult result = _userManager.ConfirmEmailAsync(user, token).Result;
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                return RedirectToAction("Fail", "Home", new { area = "Customer" });
            }
        }
    }
}
