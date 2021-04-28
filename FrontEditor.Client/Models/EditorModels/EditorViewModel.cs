
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorViewModel
    {
        public EditorMode Mode { get; set; } = EditorMode.Editor; 
        public ProjectViewModel ProjectData { get; set; }
        public EditorModelData EditorData { get; set; }        
        public List<string> ImageList { get; set; }        

        public EditorViewModel() 
        {
            ImageList = new List<string>();
        }
    }
    public enum EditorMode 
    {
        Editor,
        Preview,
        Export
    }
}