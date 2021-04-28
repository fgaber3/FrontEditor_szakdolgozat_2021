using System;
using System.IO;
using System.Threading.Tasks;
using FrontEditor.Client.BusinessLogic;
using FrontEditor.Client.Models;
using FrontEditor.Client.Models.EditorModels;
using FrontEditor.Client.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEditor.Client.Controllers
{
    public class EditorController : BaseController
    {
         private ICompositeViewEngine _viewEngine;
        private readonly IWebHostEnvironment _env;
        public EditorController(ILogger<HomeController> logger, IConfiguration config, FrontEditorContext context, ICompositeViewEngine viewEngine, IWebHostEnvironment env) : base(logger, config, context)
        { 
            _viewEngine = viewEngine;
            _env = env;
        }

        public IActionResult Index(int projectId)
        {
            EditorViewModel editor = EditorBL.EditorData(_context, projectId, $"{_env.WebRootPath}");
            if(editor.ProjectData == null)
                return RedirectToAction("Index","Projects");
            ProjectsBL.SetProjectStatus(_context, projectId, Models.Enums.ProjectStatus.Editable);
            return View(editor);
        }

        public IActionResult Preview(int projectId)
        {
            ViewBag.IsClientPreview = true;
            EditorViewModel editor = EditorBL.EditorData(_context, projectId, $"{_env.WebRootPath}");
            editor.Mode = EditorMode.Preview; 
            if(editor.ProjectData == null)
                return RedirectToAction("Index","Projects");

            return View(editor);
        }

        public async Task<IActionResult> Export(int projectId, bool jsonExport = true)
        {                        
            EditorViewModel model = EditorBL.EditorData(_context, projectId, $"{_env.WebRootPath}");
            model.Mode = EditorMode.Export;

            if(model.ProjectData == null) 
                return RedirectToAction("Index","Projects");

            string redirect = "";
            if(jsonExport)
            {
                redirect =  EditorBL.ExportProjectJson(_context, projectId, $"{_env.WebRootPath}");
            }
            else
            {                
                string htmlContent = await RenderPartialViewToString("Preview", model);
                redirect = EditorBL.ExportProjectHtml(projectId, $"{_env.WebRootPath}", htmlContent, model.EditorData.CustomCss);
            }
            
            ProjectsBL.SetProjectStatus(_context, projectId, Models.Enums.ProjectStatus.Exported, true);

            return RedirectToAction("Index", "Projects", new { RedirectTo = redirect });
        }

        [HttpPost]
        public IActionResult ImportProject(int projectId)
        {
           ImportProjectDataViewModel model = new ImportProjectDataViewModel() { ProjectId = projectId };
           return PartialView("ProjectImporter", model);
        }      

        [HttpPost]
        public IActionResult Import(ImportProjectDataViewModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    if (model.FromFile.Length > 0)
                    {                     
                        if(!model.FromFile.FileName.EndsWith(".zip")) throw new Exception("Csak .zip fájlok tölthetőek fel!");
                        System.IO.Directory.CreateDirectory($"{_env.WebRootPath}/images/projects/{model.ProjectId}");
                        string filePath = $"{_env.WebRootPath}/images/projects/{model.ProjectId}/export.zip";
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            model.FromFile.CopyTo(stream);
                        }       
                        EditorBL.ImportProjectFromZip(_context, model.ProjectId, $"{_env.WebRootPath}", filePath);     
                        model.ImportSuccess = true;
                    }
                }      
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }        
            }
            return PartialView("ProjectImporter", model);
        }  

        [HttpPost]
        public IActionResult UploadImageToProject(int projectId)
        {
           UploadProjectImageViewModel model = new UploadProjectImageViewModel() { ProjectId = projectId };
           return PartialView("ProjectImageUploader", model);
        }      
        [HttpPost]
        public IActionResult UpladProjectImage(UploadProjectImageViewModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    if (model.Image.Length > 0)
                    {              
                        if(!model.Image.ContentType.StartsWith("image")) throw new Exception("Csak képfájlok tölthetőek fel!");                                 
                        string filePath = $"{_env.WebRootPath}/images/projects/{model.ProjectId}/{model.Image.FileName}";
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            model.Image.CopyTo(stream);
                        }      
                        model.UploadedFileName = model.Image.FileName;
                    }
                }      
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }        
            }
           return PartialView("ProjectImageUploader", model);
        }  

        [HttpPost]
        public IActionResult DeleteProjectImage(int projectId, string image) 
        {
            try 
            {         
                string filePath = $"{_env.WebRootPath}/images/projects/{projectId}/{image}.jpg";      
                if(System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);         
                return Json("SUCCESS");
            }
            catch(Exception ex) 
            {
                return Json(ex.Message);
            } 
        }

        private async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult = 
                    _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext, 
                    viewResult.View, 
                    ViewData, 
                    TempData, 
                    writer, 
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public JsonResult SaveCustomCss(int projectId, string customCss)
        {
            try 
            {         
                EditorBL.SaveCustomCss(_context, projectId, customCss);
                return Json("SUCCESS");
            }
            catch(Exception ex) 
            {
                return Json(ex.Message);
            }           
        }   
       
        [HttpPost]
        public IActionResult SaveHeaderData(ProjectHeaderDataViewModel model)
        {
            if (ModelState.IsValid)
            {   
                try
                {
                    EditorBL.SaveProjectHeaderData(_context, model);                                          
                    model.DatasChanged = true;
                }      
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }        
            }
            return PartialView("_baseDatasForm", model);
        } 
    }
}
