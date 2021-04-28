using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "A cím mező kötelező!")]
        [Display(Name = "Cím")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A leírás mező kötelező!")]
        [Display(Name = "Leírás")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "A kategória mező kötelező!")]
        [Display(Name = "Kategória")]
        public string Category { get; set; }
        public int OwnerId { get; set; } 
        public string OwnerDisplayName { get; set; }
        public ProjectStatus Status { get;set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastEdit { get; set; }
        public int ExportCount { get; set; }

        public bool DatasChanged { get; set; }
        public bool NewProject { get; set; }



        public ProjectViewModel() {}
        public ProjectViewModel(Project project) {
            ProjectId = project.Id;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            if(project.Owner != null)
            {
                OwnerId = project.Owner.Id;
                OwnerDisplayName = project.Owner.DisplayName;
            }
            Status = project.Status;
            CreateTime = project.CreateTime;
            LastEdit = project.LastEdit;
            ExportCount = project.ExportCount;
        }
    }
}
