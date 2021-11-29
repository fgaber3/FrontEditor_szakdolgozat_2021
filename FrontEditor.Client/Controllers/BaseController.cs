using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FrontEditor.Client.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly IConfiguration _config;
        protected readonly FrontEditorContext _context;

        public BaseController(ILogger<BaseController> logger, IConfiguration config, FrontEditorContext context)
        {
            _logger = logger;
            _config = config;
            _context = context;
        }
    }
}
