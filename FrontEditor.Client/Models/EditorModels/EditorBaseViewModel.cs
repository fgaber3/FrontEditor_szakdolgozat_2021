using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorBaseViewModel
    {   
        public string ViewName { get; private set; }     
        public int ProjectId { get; set; }   
        public string ComponentName { get; set; }
        public string ComponentId { get; set; }

        public List<KeyValuePair<string, ColorShemeEnum>> ColorShemes 
        { 
            get 
            {
                return new List<KeyValuePair<string, ColorShemeEnum>>() {
                    new KeyValuePair<string, ColorShemeEnum>("Világos", ColorShemeEnum.Light),
                    new KeyValuePair<string, ColorShemeEnum>("Sötét", ColorShemeEnum.Dark)
                };
            }
        }

        public EditorBaseViewModel(string viewName, int projectId)
        {
            ViewName = viewName;
            ProjectId = projectId;
        }
    }
}
