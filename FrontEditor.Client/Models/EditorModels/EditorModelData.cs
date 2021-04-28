using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorModelData
    {
        [Display(Name = "Oldalcím")]        
        public string Title { get; set; }

        [Display(Name = "Meta leírás")]        
        public string Description { get; set; }

        [Display(Name = "Egyedi stílus")]
        public string CustomCss { get; set; }

        public List<EditorBaseViewModel> Blocks { get; set; }

        public EditorModelData()
        { 
            Blocks = new List<EditorBaseViewModel>();
        }
    }
}