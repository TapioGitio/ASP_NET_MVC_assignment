using Business.Interfaces;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaWebApp.Controllers;

public class AuthController(IAuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;


    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginForm form)
    {

        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            if (result)
                return RedirectToAction("Index", "Admin");
        }

        ViewBag.ErrorMessage = "Invalid email or password";
        return View(form);
    }

    [HttpPost]
    public IActionResult ExternalLogin(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Invalid provider");
            return View("Login");
        }

        var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/")!;

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("Login");
        }

        var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
            return RedirectToAction("Login");

        var loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (loginResult.Succeeded)
            return RedirectToAction("Index", "Admin");
        else
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                firstName = loginInfo.Principal.FindFirstValue(ClaimTypes.GivenName)!;
                lastName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname)!;
            }
            catch { }

            string email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email)!;
            string userName = $"ext_{loginInfo.LoginProvider.ToLower()}_{email}";

            var user = new MemberEntity { UserName = userName, Email = email, FirstName = firstName, LastName = lastName };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, loginInfo);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Admin");
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Login");
        }
    }

    [Route("Register")]
    public IActionResult Register()
    {

        return View();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserRegistrationForm form)
    {

        if (ModelState.IsValid)
        {
            var result = await _authService.RegisterAsync(form);
            if (result)
                return LocalRedirect("~/");
            else
            {
                ViewBag.ErrorMessage = "User with same email already exists";
                return View(form);
            }
        }

        return View(form);
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Login", "Auth");
    }

    [Route("accessdenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

}
