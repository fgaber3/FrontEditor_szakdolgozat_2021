
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class MenuItem
    {
        
        [Display(Name = "Szöveg")]
        public string Title { get; set; }
        
        [Display(Name = "Ikon")]
        public string Icon { get; set; }
        
        [Display(Name = "Link")]
        public string Href { get; set; }
        
        [Display(Name = "Megnyitás új lapon")]
        public bool TargetBlank { get; set; }
        
        [Display(Name = "Almenü")]
        public List<MenuItem> MenuItems { get; set; }
        public MenuItem()
        {
            this.MenuItems = new List<MenuItem>();
        }
    }
    }