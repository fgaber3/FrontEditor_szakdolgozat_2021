using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontEditor.Client.Models;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using FrontEditor.Client.BusinessLogic;
using Microsoft.AspNetCore.Identity;

namespace FrontEditor.Client.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {

        private readonly UserManager<User> _userManager;

        public UsersController(ILogger<HomeController> logger, IConfiguration config, FrontEditorContext context, UserManager<User> userManager) : base(logger, config, context)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<UserViewModel> users = UsersBL.GetUserList(_context);
            return View(users);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return PartialView("CreateEdit", new UserViewModel());
        }
        [HttpPost]
        public IActionResult Edit(int userId)
        {
            UserViewModel user = UsersBL.GetUser(_context, userId);
            return PartialView("CreateEdit", user);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.UserId == 0)
                    {
                        model = await UsersBL.CreateUser(_context, _userManager, model);
                        if (!string.IsNullOrEmpty(model.ErrorText))
                        {
                            throw new Exception(model.ErrorText);
                        }
                    }
                    else
                    {
                        model = UsersBL.EditUserProfile(_context, model);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> ResetPassword(int userId)
        {
            UserViewModel model = await UsersBL.ResetPassword(_context, _userManager, userId);
            return Json(model);
        }

        [HttpPost]
        public IActionResult UserData(int userId)
        {
            UserViewModel user = UsersBL.GetUser(_context, userId);
            return PartialView("UserData", user);
        }

        [HttpPost]
        public IActionResult ReloadContent()
        {
            List<UserViewModel> users = UsersBL.GetUserList(_context);
            return PartialView("Content", users);
        }

        [HttpPost]
        public JsonResult RemoveUser(int userId)
        {
            try
            {
                UsersBL.RemoveUser(_context, userId);
                return Json("SUCCESS");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
