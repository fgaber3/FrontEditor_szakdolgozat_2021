using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using FrontEditor.Client.Models;
using FrontEditor.Client.Models.EditorModels;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.BusinessLogic
{
    public static class EditorBL 
    {
        private static JsonParser<EditorModelData> parser = new JsonParser<EditorModelData>();

        public static EditorViewModel EditorData(FrontEditorContext _context, int projectId, string rootFolder)
        {
            EditorViewModel editor = new EditorViewModel();
            Project project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if(project == null)
            {
                return editor;
            }
            editor.ProjectData = new ProjectViewModel(project);
            editor.EditorData = GetProjectEditorData(_context, projectId);
            editor.ImageList = GetProjectImageList(projectId, rootFolder);

            return editor;
        }

        public static List<string> GetProjectImageList(int projectId, string rootPath)
        {
            string projectPath = rootPath + "/images/projects/" + projectId +"/";
            if(!Directory.Exists(projectPath)) return new List<string>();
            List<string> list = new List<string>();
            foreach(string path in Directory.GetFiles(projectPath)) {
                list.Add(Path.GetFileName(path));
            }
            return list;
        }

        public static EditorModelData GetProjectEditorData(FrontEditorContext _context, int projectId)
        {
            ProjectJSON data = _context.ProjectJSONDatas.Where(x => x.ProjectId == projectId).FirstOrDefault();
            if(data == null)
            {
                data = new ProjectJSON() {
                   ProjectId = projectId,
                   ProjectData = parser.SerializeData(new EditorModelData())
                };
                _context.Add(data);
                _context.SaveChanges();
            }
            return parser.DeserializeData(data.ProjectData);
        }

        public static void SaveProjectEditorData(FrontEditorContext _context, int projectId, EditorModelData saveData)
        {
            ProjectJSON data = _context.ProjectJSONDatas.Where(x => x.ProjectId == projectId).FirstOrDefault();
            if(data == null)
            {
                data = new ProjectJSON() {
                   ProjectId = projectId,
                   ProjectData = parser.SerializeData(saveData)
                };
                _context.Add(data);
            }
            else 
            {
                data.ProjectData = parser.SerializeData(saveData);                
                _context.Update(data);
            }            
            _context.SaveChanges();
        }

        internal static void SaveProjectHeaderData(FrontEditorContext _context, ProjectHeaderDataViewModel model)
        {
            EditorModelData editorData = GetProjectEditorData(_context, model.ProjectIdForBaseDatas);
            editorData.Title = model.TitleForBaseDatas;
            editorData.Description = model.Description;         
            SaveProjectEditorData(_context, model.ProjectIdForBaseDatas, editorData);
        }

        public static void SaveCustomCss(FrontEditorContext _context, int projectId, string customCss)
        {
            EditorModelData editorData = GetProjectEditorData(_context, projectId);
            editorData.CustomCss = customCss;         
            SaveProjectEditorData(_context, projectId, editorData);
        }

        public static void SaveBlocksData(FrontEditorContext _context, int projectId, List<EditorBaseViewModel> blocks)
        {
            EditorModelData editorData = GetProjectEditorData(_context, projectId);
            editorData.Blocks = blocks;         
            SaveProjectEditorData(_context, projectId, editorData);
        }

        private static EditorModelData NewMethod(FrontEditorContext _context, int projectId)
        {
            return GetProjectEditorData(_context, projectId);
        }

        public static void ImportProjectFromZip(FrontEditorContext _context, int projectId, string rootPath, string zipPath)
        {
            try 
            {                
                string tempPath = rootPath + "/import/" + projectId + "-import-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "/";
                string imagesPath = rootPath + "/images/projects/" + projectId + "/";
                ZipFile.ExtractToDirectory(zipPath, tempPath);
                //Parse json and Save it
                string importedData = System.IO.File.ReadAllText(tempPath + "project.json");
                EditorModelData data = parser.DeserializeData(importedData);
                ProjectJSON jsonData = _context.ProjectJSONDatas.Where(x => x.ProjectId == projectId).FirstOrDefault();
                if(jsonData == null)
                {
                    jsonData = new ProjectJSON() {
                    ProjectId = projectId,
                    ProjectData = parser.SerializeData(data)
                    };
                    _context.Add(jsonData);
                }
                else {
                    jsonData.ProjectData = parser.SerializeData(data);
                    _context.Update(jsonData);
                }
                _context.SaveChanges();
                //Empty files folder and copy new files to files folder
                foreach (string file in Directory.GetFiles(imagesPath, "*.*",SearchOption.AllDirectories))
                {
                    File.Delete(file);
                }     
                foreach (string file in Directory.GetFiles(tempPath + "images/", "*.*",SearchOption.AllDirectories))
                {
                    File.Copy(file, file.Replace(imagesPath, tempPath), true);
                }   
                //Delete import files from temp folder            
                Directory.Delete(tempPath, true);   
            }
            catch (Exception ex)
            {
                throw new Exception("Hiba történta projekt impotálása közben!");
            }
        }

        public static string ExportProjectJson(FrontEditorContext _context, int projectId, string rootPath) 
        {
            ProjectJSON data = _context.ProjectJSONDatas.Where(x => x.ProjectId == projectId).FirstOrDefault();
            if(data == null) throw new Exception("Projekt nem található!");

            string fileName = projectId + "-export-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") +".zip";
            string exportPath = rootPath + "/export/" + fileName;
            string tempPath = rootPath + "/export/" + projectId + "-export-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "/";
            string templatePath = rootPath + "/export-template/";
            string imagesPath = rootPath + "/images/projects/" + projectId + "/";

            //Create temp folder and copy template and images
            System.IO.Directory.CreateDirectory(tempPath);
            foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(templatePath, tempPath));
            }
            foreach (string newPath in Directory.GetFiles(templatePath, "*.*",SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(templatePath, tempPath), true);
            }             
            if(Directory.Exists(imagesPath))
                foreach (string newPath in Directory.GetFiles(imagesPath, "*.*",SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(imagesPath, tempPath + "images/"), true);
                }                 
            //Create template json file    
            using (FileStream fs = File.Create(tempPath + "project.json"))     
            {                      
                Byte[] html = new UTF8Encoding(true).GetBytes(data.ProjectData);    
                fs.Write(html, 0, html.Length);    
            }  
            //Create .zip file 
            ZipFile.CreateFromDirectory(tempPath, exportPath);
            //Remove temp folder
            Directory.Delete(tempPath, true);            
            return "/export/" + fileName;
        }

        public static string ExportProjectHtml(int projectId, string rootPath, string exportHtml, string exportCss) 
        {
            string fileName = projectId + "-export-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") +".zip";
            string exportPath = rootPath + "/export/" + fileName;
            string tempPath = rootPath + "/export/" + projectId + "-export-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "/";
            string templatePath = rootPath + "/export-template/";
            string imagesPath = rootPath + "/images/projects/" + projectId + "/";

            //Create temp folder and copy template and images
            System.IO.Directory.CreateDirectory(tempPath);
            foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(templatePath, tempPath));
            }
            foreach (string newPath in Directory.GetFiles(templatePath, "*.*",SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(templatePath, tempPath), true);
            }             
            if(Directory.Exists(imagesPath))
                foreach (string newPath in Directory.GetFiles(imagesPath, "*.*",SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(imagesPath, tempPath + "images/"), true);
                } 
            //Create temaplate files    
            using (FileStream fs = File.Create(tempPath + "index.html"))     
            {                      
                Byte[] html = new UTF8Encoding(true).GetBytes(exportHtml);    
                fs.Write(html, 0, html.Length);    
            }    
            using (FileStream fs = File.Create(tempPath + "/css/site.css"))     
            {                      
                Byte[] css = new UTF8Encoding(true).GetBytes(exportCss);    
                fs.Write(css, 0, css.Length);    
            } 
            //Create .zip file 
            ZipFile.CreateFromDirectory(tempPath, exportPath);
            //Remove temp folder
            Directory.Delete(tempPath, true);     

            return "/export/" + fileName;
        }    
    }
}