using System;
using System.Threading.Tasks;
using FrontEditor.Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using FrontEditor.Client.BusinessLogic;
using Microsoft.AspNetCore.Hosting;

namespace FrontEditor.Client.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
            IConfiguration config,
            FrontEditorContext context,
            IWebHostEnvironment env) : base(logger, config, context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }

        public IActionResult Index()
        {
            int userId = 0;
            if (!int.TryParse(_userManager.GetUserId(HttpContext.User), out userId))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(UsersBL.GetUser(_context, userId));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView("ChangePassword", new PasswordChangeViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await UsersBL.ChangePassword(_context, _userManager, User, model);
                if (!string.IsNullOrEmpty(model.ErrorText))
                {
                    ModelState.AddModelError("", model.ErrorText);
                }
            }
            return PartialView("ChangePassword", model);
        }

        [HttpGet]
        public IActionResult ChangeProfile()
        {
            int userId = int.Parse(_userManager.GetUserId(User));
            UserViewModel model = UsersBL.GetUser(_context, userId);
            return PartialView("ChangeProfile", model);
        }
        [HttpPost]
        public IActionResult ChangeProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.UserId = int.Parse(_userManager.GetUserId(User));
                    model = UsersBL.SaveUserProfile(_context, model);
                    model.DatasChanged = true;
                    return PartialView("ChangeProfile", model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("ChangeProfile", model);
        }

        public FileResult GetProfileImage(int profileId)
        {
            int userId = int.Parse(_userManager.GetUserId(User));
            string filePath = $"{_env.WebRootPath}/images/profile/{userId}.jpg";

            if (!System.IO.File.Exists(filePath))
                filePath = $"{_env.WebRootPath}/images/base/profileimage.jpg";

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/png");
        }

        [HttpGet]
        public IActionResult ChangeProfileImage()
        {
            return PartialView("UpladProfileImage", new ProfileImageChangeViewModel());
        }

        [HttpPost]
        public IActionResult UpladProfileImage(ProfileImageChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = int.Parse(_userManager.GetUserId(User));
                    if (model.Image.Length > 0)
                    {
                        if (!model.Image.ContentType.StartsWith("image")) throw new Exception("Csak képfájlok tölthetőek fel!");
                        string filePath = $"{_env.WebRootPath}/images/profile/{userId}.jpg";
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            model.Image.CopyTo(stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("UpladProfileImage", model);
        }

        [HttpPost]
        public IActionResult RemoveProfileImage()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = int.Parse(_userManager.GetUserId(User));
                    string filePath = $"{_env.WebRootPath}/images/profile/{userId}.jpg";
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("UpladProfileImage", new ProfileImageChangeViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                user = await UsersBL.Login(_context, _signInManager, user);
                if (string.IsNullOrEmpty(user.ErrorText))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", user.ErrorText);
                    return View(user);
                }
            }
            return View(user);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await UsersBL.Register(_context, _userManager, _signInManager, model);
                if (!string.IsNullOrEmpty(model.ErrorText))
                {
                    ModelState.AddModelError("", model.ErrorText);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult PasswordReset()
        {
            if (_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordReset(PasswordResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await UsersBL.PasswordReset(_context, _userManager, model);
                if (!string.IsNullOrEmpty(model.ErrorText))
                {
                    ModelState.AddModelError("", model.ErrorText);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
