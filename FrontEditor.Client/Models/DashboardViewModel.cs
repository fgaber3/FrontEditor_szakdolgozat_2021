using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class DashboardViewModel
    {
        public int UsersCount { get; set; }
        public int ProjectsCount  {get; set; }
        public int CategoriesCount { get; set; }
        public int ExportsCount { get; set; }
        public int NewProjectsCount  { get; set; }
        public int InProgressProjectsCount  { get; set; }
        public int ExportedProjectsCount  { get; set; }
        public List<StatusItemViewModel> StatusList { get; set; }
        public List<TechnologyItemViewModel> TechnologyList { get; set; }
        public DashboardViewModel() {
            StatusList = new List<StatusItemViewModel>();
            TechnologyList = new List<TechnologyItemViewModel>();
        }
    }
}
