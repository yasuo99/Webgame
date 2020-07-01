using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DichVuGame.Extensions;
using DichVuGame.Utility;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

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
        public async Task<IActionResult> ResendOTP(string returnUrl = null)
        {
            var otp = HttpContext.Session.Get<OTPSession>("OTP");
            var randomOtp = new Random().Next(10000, 99999);
            OTPSession otpSession = new OTPSession(randomOtp, DateTime.Now.AddMinutes(5), otp.Email, otp.Password);
            HttpContext.Session.Set("OTP", otpSession);
            using (SmtpClient client = new SmtpClient())
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("GameProvider", "yasuo12091999@gmail.com"));
                message.To.Add(new MailboxAddress("Không trả lời", otp.Email));
                message.Subject = "Xác thực OTP";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                { Text = "Mã OTP: " + randomOtp };
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("yasuo120999@gmail.com", "Thanhpro1999@");
                client.Send(message);
                client.Disconnect(true);
                return RedirectToPage("OTPConfirm");
            }
        }
    }
}
