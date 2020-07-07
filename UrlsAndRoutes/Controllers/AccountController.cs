﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using System.Linq;

namespace UrlsAndRoutes.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
      
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {

            //ViewBag.returnUrl = returnUrl;
            details.ReturnUrl = returnUrl;
           // details.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [AllowAnonymous]
        //[HttpPost]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {

            //ViewBag.returnUrl = returnUrl;

            var redirectUrl = Url.Action(nameof(GoogleResponse), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            
            return new ChallengeResult("Google", properties);

        }

        [AllowAnonymous]
        //[HttpGet]
        public async Task<IActionResult> GoogleResponse(string returnUrl = null)
        {
            
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync(userManager.GetUserId(User));

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.ExternalLoginSignInAsync("Google", info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

                if (result.Succeeded)
                {
                    return Redirect(returnUrl);

                    //    [AllowAnonymous]
                    //public IActionResult Login(string returnUrl)
                    //{
                    //    ViewBag.returnUrl = returnUrl;
                    //    return View();
                    //}
                    //[HttpPost]
                    //[AllowAnonymous]
                    //[ValidateAntiForgeryToken]
                    //public async Task<IActionResult> Login(LoginModel details, string returnUrl)
                    //{
                    //    return View(details);
                    //}
                }
                else
                {
                    AppUser user = new AppUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,

                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                    };
                    IdentityResult identResult = await userManager.CreateAsync(user);

                    if (identResult.Succeeded)
                    {
                        identResult = await userManager.AddLoginAsync(user, info);

                        if (identResult.Succeeded)
                        {
                                await signInManager.SignInAsync(user, false);
                            return Redirect(returnUrl);
                        }
                    }
                    return AccessDenied();
                }
            }
    }
}