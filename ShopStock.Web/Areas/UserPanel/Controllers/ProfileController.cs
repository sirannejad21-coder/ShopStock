using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Generator;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.ViewModels.Account;
using System.Security.Claims;

namespace ShopStock.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("Userpanel")]
    public class ProfileController : Controller
    {
        #region constractor

        IAccountService _accountService;

        public ProfileController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion



        #region changepassword

        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                return View(change);
            }
            var user = await _accountService.GetUserByEmailOrUsernameAsync(User.Identity.Name);
            var res = await _accountService.ChangePasswordAsyc(user.Id, change);

            return Redirect("/LogOut");
        }


        #endregion


        #region Edit Profile

        public async Task<IActionResult> Index()
        {

            var model = await _accountService.GetUserProfileAsync
                (int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()));

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Avatar != "nophoto")
            {
                string DeletePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Avatar",
                model.Avatar 
            );
                System.IO.File.Exists(DeletePath);
                System.IO.File.Delete(DeletePath);
                
            



            }
            if (formFile != null)
            {


                if (!formFile.ImageValid())
                {
                    ViewBag.imageisvalid = false;
                    return View(model);
                }

                string AvatarImage =
                    NameGenerator.GenerateuniqName()
                    + Path.GetExtension(formFile.FileName).ToLower();

                string SavePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Avatar",
                    AvatarImage
                );

                using (var stream = System.IO.File.Create(SavePath))
                {
                    await formFile.CopyToAsync(stream);
                }

                model.Avatar = AvatarImage;
            }


            var user = await _accountService
                .GetUserByEmailOrUsernameAsync(User.Identity.Name);

            bool res = await _accountService.FinishProfile(user.Id, model);

            TempData["editprofile"] = res;

            return Redirect("/userpanel");
        }
    }

        #endregion


}
