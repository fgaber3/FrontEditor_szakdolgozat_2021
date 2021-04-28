using System;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.Models.Entities
{
    public class ProjectJSON
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectData { get; set; }
    }
}