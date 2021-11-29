using System;
using System.Linq;
using FrontEditor.Client.Models;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.BusinessLogic
{
    public static class ProjectsBL
    {
        public static ProjectViewModel CreateProject(FrontEditorContext _context, ProjectViewModel proj)
        {
            User owner = _context.Users.Where(x => x.Id == proj.OwnerId).FirstOrDefault();
            Project project = new Project()
            {
                Title = proj.Title,
                Description = proj.Description,
                Category = proj.Category,
                CreateTime = DateTime.Now,
                Owner = owner,
                LastEdit = DateTime.Now,
                Status = ProjectStatus.New,
                ExportCount = 0
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            return new ProjectViewModel(project) { DatasChanged = true, NewProject = true };
        }

        public static ProjectViewModel EditProjectData(FrontEditorContext _context, ProjectViewModel proj)
        {
            Project project = _context.Projects.Where(x => x.Id == proj.ProjectId).FirstOrDefault();

            project.Title = proj.Title;
            project.Description = proj.Description;
            project.Category = proj.Category;
            project.LastEdit = DateTime.Now;
            if (proj.OwnerId < 0)
            {
                User owner = _context.Users.Where(x => x.Id == proj.OwnerId).FirstOrDefault();
                project.Owner = owner;
            }
            project.Status = proj.Status;

            _context.Projects.Update(project);
            _context.SaveChanges();
            return new ProjectViewModel(project) { DatasChanged = true };
        }

        public static void DeleteProject(FrontEditorContext _context, int projectId, int userId)
        {
            Project project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if (!UsersBL.UserIsAdmin(_context, userId) && project.Owner.Id != userId)
                throw new Exception("Nincs jogosultsága a kért művelet elvégzéséhez!");
            ProjectJSON jsondata = _context.ProjectJSONDatas.Where(x => x.ProjectId == projectId).FirstOrDefault();
            if (jsondata != null)
                _context.ProjectJSONDatas.Remove(jsondata);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public static void SetProjectStatus(FrontEditorContext _context, int projectId, ProjectStatus status, bool addExport = false)
        {
            Project project = _context.Projects.Where(x => x.Id == projectId).FirstOrDefault();
            if (project != null)
            {
                project.Status = status;
                if (addExport) project.ExportCount++;
                _context.Update(project);
                _context.SaveChanges();
            }
        }

        public static ProjectListViewModel ProjectListModel(FrontEditorContext _context, bool isAdmin, bool adminMode, int userId, string redirectTo = "")
        {
            ProjectListViewModel project = new ProjectListViewModel();
            project.AdminMode = isAdmin;
            project.ShowAllProject = isAdmin && adminMode;
            project.RedirectTo = redirectTo;
            if (project.ShowAllProject)
            {
                project.ProjectList = _context.Projects.Select(x => new ProjectViewModel(x)).ToList();
            }
            else
            {
                project.ProjectList = _context.Projects.Where(x => x.Owner.Id == userId).Select(x => new ProjectViewModel(x)).ToList();
            }
            return project;
        }
        public static ProjectViewModel GetProjectData(FrontEditorContext _context, int id)
        {
            Project project = _context.Projects.Where(x => x.Id == id).FirstOrDefault();
            return new ProjectViewModel(project);
        }
    }
}