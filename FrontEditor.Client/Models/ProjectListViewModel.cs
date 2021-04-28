using System.Collections.Generic;

namespace FrontEditor.Client.Models
{
    public class ProjectListViewModel
    {
        public bool ShowAllProject { get; set; }
        public bool AdminMode { get; set; }
        public List<ProjectViewModel> ProjectList { get; set; }

        public string RedirectTo { get; set; }

        public ProjectListViewModel()
        {
            ProjectList = new List<ProjectViewModel>();
        }
        
    }
}
