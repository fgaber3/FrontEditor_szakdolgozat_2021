
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class CarouselItem
    {        
        [Display(Name = "Kép")]
        public string Image { get; set; }
        
        [Display(Name = "Cím")]
        public string Title { get; set; }
        
        [Display(Name = "Leírás")]
        public string Description { get; set; } 
    }
}