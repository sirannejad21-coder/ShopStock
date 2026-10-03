using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Enums;
using ShopStock.Domain.ViewModels.Account;
using System.Security.Claims;

namespace ShopStock.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor

        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        #endregion


        #region Register

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _service.RegisterAsync(model);

            #region IsValidRegister

            switch (res)
            {
                case RegisterUserResult.Succes:
                    {
                        return View("succesRegister", model);
                    }

                case RegisterUserResult.UserDuplecate:

                    ModelState.AddModelError(
                        "User",
                        "نام وارد شده تکراری است");

                    break;


                case RegisterUserResult.EmailDuplecate:

                    ModelState.AddModelError(
                        "Email",
                        "ایمیل وارد شده تکراری است");

                    break;


                case RegisterUserResult.SendActiveEmail:

                    ModelState.AddModelError(
                        "Email",
                        "ارسال ایمیل فعال سازی با مشکل مواجه شده");

                    break;


                case RegisterUserResult.InvalidInout:

                    ModelState.AddModelError(
                        "",
                        "لطفا فیلدهای فرم را پر کنید");

                    break;


                case RegisterUserResult.Faild:

                    ModelState.AddModelError(
                        "Email",
                        "خطای ناشناس");

                    break;
            }

            #endregion

            return View(model);
        }

        #endregion


        #region Active Email

        [Route("VerifyEmail/{activecode}")]
        public async Task<IActionResult> ActiveAcount(string activecode)
        {
            ViewBag.activecode =
                await _service.ActiveAccountAsync(activecode);

            return View();
        }

        #endregion


        #region Login

        [Route("login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View(new LoginViewModel());
        }


        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "کد امنیتی وارد شده صحیح نمی باشد")]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            // بررسی Validation فرم + کپچا
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            // بررسی Username / Password
            var res = await _service.LoginAsync(model);


            if (res == LoginUserResult.NotActive)
            {
                ModelState.AddModelError(
                    "User",
                    "ایمیل فعال نیست");

                return View(model);
            }


            if (res == LoginUserResult.NotFound)
            {
                ModelState.AddModelError(
                    "Email",
                    "کاربر پیدا نشد");

                return View(model);
            }


            // دریافت اطلاعات کاربر
            var user = await _service
                .GetUserByEmailOrUsernameAsync(
                    model.UserNameOrEmail);


            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "اطلاعات کاربر یافت نشد");

                return View(model);
            }


            // Claims
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName),

                new Claim(
                    "FullName",
                    $"{user.FirstName} {user.LastName}"),

                new Claim(
                    "Mobile",
                    user.Mobile ?? ""),

                new Claim(
                    "Avatar",
                    user.Avatar ?? "")
            };


            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);


            var principal = new ClaimsPrincipal(identity);


            var properties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties);


            // ReturnUrl
            if (!string.IsNullOrEmpty(ReturnUrl) &&
                Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }


            return Redirect("/");
        }

        #endregion


        #region Logout

        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/login");
        }

        #endregion
    }
}