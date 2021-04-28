using System;
using System.Collections.Generic;
using System.Linq;
using FrontEditor.Client.Models;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.BusinessLogic
{
    public static class DashboardBL 
    {
        public static DashboardViewModel DashboardDatas(FrontEditorContext _context) 
        {
            DashboardViewModel data = new DashboardViewModel();
            using(_context)
            {
                data = new DashboardViewModel() {
                    UsersCount = _context.Users.Count(),
                    ProjectsCount = _context.Projects.Count(),
                    CategoriesCount = _context.Projects.Select(x => x.Category).Distinct().Count(),
                    ExportsCount = _context.Projects.Select(x => x.ExportCount).Sum(),
                    NewProjectsCount = _context.Projects.Where(x => x.Status == ProjectStatus.New).Count(),
                    InProgressProjectsCount = _context.Projects.Where(x => x.Status == ProjectStatus.Editable).Count(),
                    ExportedProjectsCount = _context.Projects.Where(x => x.Status == ProjectStatus.Exported).Count()
                };
                data.StatusList = new List<StatusItemViewModel> {
                    new StatusItemViewModel("Új projekt", data.NewProjectsCount > 0 ? "Van" : "Nincs", data.NewProjectsCount > 0),
                    new StatusItemViewModel("Projekt folyamatban", data.InProgressProjectsCount > 0 ? "Van" : "Nincs", data.InProgressProjectsCount > 0),
                    new StatusItemViewModel("Exportált projekt", data.ExportedProjectsCount > 0 ? "Van" : "Nincs", data.ExportedProjectsCount > 0),
                    new StatusItemViewModel("Felhasználók", data.UsersCount > 100 ? "100 felett" : "100 alatt", data.UsersCount > 10),
                    new StatusItemViewModel("Exportálás", data.ExportsCount > 0 ? "Történt" : "Nem történt", data.ExportsCount > 0)
                };
                data.TechnologyList = new List<TechnologyItemViewModel> {
                    new TechnologyItemViewModel(".NET Core 3.1", "https://dotnet.microsoft.com/download/dotnet-core/3.1", "3.1.0", "MIT"),
                    new TechnologyItemViewModel("MySQL", "https://www.mysql.com/", "8.0.23", "MIT"),
                    new TechnologyItemViewModel("Entity Framework Core", "https://docs.microsoft.com/en-us/ef/core/", "3.1.5", "Apache-2.0"),
                    new TechnologyItemViewModel("Popper.js", "https://popper.js.org/", "1.16.1", "MIT"),
                    new TechnologyItemViewModel("jQuery", "https://code.jquery.com/jquery/", "3.5.1", "MIT"),
                    new TechnologyItemViewModel("Bootstrap", "https://getbootstrap.com/", "4.5.3", "MIT"),
                    new TechnologyItemViewModel("FontAwesome", "https://fontawesome.com/", "5.15.1", "MIT"),
                    new TechnologyItemViewModel("Chart.js", "https://www.chartjs.org/", "2.9.4", "MIT"),
                    new TechnologyItemViewModel("Json.NET", "https://www.newtonsoft.com/json", "13.0.1", "MIT"),                        
                };
            }
            return data;
        }
    }
}