using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FrontEditor.Client.Models;
using Microsoft.Extensions.Configuration;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.BusinessLogic;
using FrontEditor.Client.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FrontEditor.Client.Controllers
{
    public class ProjectsController : BaseController
    {        
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        public ProjectsController(ILogger<HomeController> logger, IConfiguration config, FrontEditorContext context, UserManager<User> userManager, IWebHostEnvironment env) : base(logger, config, context)
        {
            _userManager = userManager;
            _env = env;
        }        
        public async Task<IActionResult> Index(bool ShowAllProject = false, string RedirectTo = "")
        {
            IList<User> users = await _userManager.GetUsersInRoleAsync("Admin");
            int userId = int.Parse(_userManager.GetUserId(User));             
            bool isAdmin = users.Where(x => x.Id == userId).Count() != 0;
            ShowAllProject = ShowAllProject && isAdmin;
            return View(ProjectsBL.ProjectListModel(_context, isAdmin, ShowAllProject, userId, RedirectTo));
        }
        [HttpPost]
        public IActionResult Create()
        {
           return PartialView("CreateEdit", new ProjectViewModel());
        }
        [HttpPost]
        public IActionResult Edit(int projectId)
        {
           ProjectViewModel project = ProjectsBL.GetProjectData(_context, projectId);
           return PartialView("CreateEdit", project);
        }
        [HttpPost]
        public IActionResult Save(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    if(model.ProjectId == 0)
                    { 
                        if(model.OwnerId <= 0) model.OwnerId = int.Parse(_userManager.GetUserId(User)); 
                        model = ProjectsBL.CreateProject(_context, model);                        
                    }
                    else 
                    { 
                        model = ProjectsBL.EditProjectData(_context, model);                        
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
        public IActionResult ReloadContent(bool ShowAllProject)
        {      
            int userId = int.Parse(_userManager.GetUserId(User));             
            bool isAdmin = UsersBL.UserIsAdmin(_context, userId);
            ProjectListViewModel model = ProjectsBL.ProjectListModel(_context, isAdmin, ShowAllProject, userId);
            return PartialView("Content", model.ProjectList);
        }

        [HttpPost]
        public JsonResult DeleteProject(int projectId)
        {
            try 
            {         
                int userId = int.Parse(_userManager.GetUserId(User));       
                ProjectsBL.DeleteProject(_context, projectId, userId);
                string filePath = $"{_env.WebRootPath}/images/projects/{projectId}.jpg";      
                if(System.IO.File.Exists(filePath))    
                    System.IO.File.Delete(filePath);
                filePath = $"{_env.WebRootPath}/images/projects/{projectId}"; 
                if(System.IO.Directory.Exists(filePath))    
                    System.IO.Directory.Delete(filePath);                    
                return Json("SUCCESS");
            }
            catch(Exception ex) 
            {
                return Json(ex.Message);
            }
        }     

        [HttpPost]
        public IActionResult ChangeProjectImage(int projectId)
        {
           ProjectImageChangeViewModel model = new ProjectImageChangeViewModel() { ProjectId = projectId };
           return PartialView("UpladProjectImage", model);
        }      

        public FileResult GetProjectImage(int projectId)
        {
            string filePath = $"{_env.WebRootPath}/images/projects/{projectId}.jpg";

            if(!System.IO.File.Exists(filePath))            
                filePath = $"{_env.WebRootPath}/images/projects/default.jpg";

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/png");             
        }

        [HttpPost]
        public IActionResult UpladProjectImage(ProjectImageChangeViewModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    if (model.Image.Length > 0)
                    {              
                        if(!model.Image.ContentType.StartsWith("image")) throw new Exception("Csak képfájlok tölthetőek fel!");                                 
                        string filePath = $"{_env.WebRootPath}/images/projects/{model.ProjectId}.jpg";
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
           return PartialView("UpladProjectImage", model);
        }  
    }
}
