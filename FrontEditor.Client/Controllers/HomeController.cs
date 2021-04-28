using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontEditor.Client.Models;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using Microsoft.EntityFrameworkCore;
using FrontEditor.Client.BusinessLogic;
using Microsoft.AspNetCore.Authorization;

namespace FrontEditor.Client.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, IConfiguration config, FrontEditorContext context) : base(logger, config, context)
        { }

        public IActionResult Index()
        {
            DashboardViewModel model = DashboardBL.DashboardDatas(_context);
            return View(model);        
        }

        [AllowAnonymous]
        public IActionResult Documentation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
