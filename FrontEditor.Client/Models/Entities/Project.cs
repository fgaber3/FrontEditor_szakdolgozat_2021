using System;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public User Owner { get; set; }
        public ProjectStatus Status { get;set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastEdit { get; set; }
        public int ExportCount { get; set; }
    }
}