using System;
using System.Threading.Tasks;
using System.Linq;
using FrontEditor.Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using FrontEditor.Client.BusinessLogic;

namespace FrontEditor.Client.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            ILogger<AccountController> logger, 
            IConfiguration config, 
            FrontEditorContext context) : base(logger, config, context)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
        }
         
        public IActionResult Index() 
        {
            int userId = 0;
            if(!int.TryParse(_userManager.GetUserId(HttpContext.User), out userId))
            {
                return RedirectToAction("Login","Account");
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
               if(!string.IsNullOrEmpty(model.ErrorText))
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
                
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if(_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index","Home");
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
                if(string.IsNullOrEmpty(user.ErrorText))
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
            if(_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index","Home");
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
                if(!string.IsNullOrEmpty(model.ErrorText))
                {
                    ModelState.AddModelError("", model.ErrorText);
                }                
            }
            return View(model);
        }
    
        [AllowAnonymous]
        public IActionResult PasswordReset()
        {
            if(_userManager.GetUserId(HttpContext.User) != null)
            {
                return RedirectToAction("Index","Home");
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
                if(!string.IsNullOrEmpty(model.ErrorText))
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
