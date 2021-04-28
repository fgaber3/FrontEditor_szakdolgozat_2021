
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class ProjectHeaderDataViewModel 
    {        
        [Required(ErrorMessage = "Nincs projekt kiválasztva!")]
        public int ProjectIdForBaseDatas { get; set; }

        [Display(Name = "Oldalcím")]        
        [Required(ErrorMessage = "Oldalcím megadása kötelező!")]
        public string TitleForBaseDatas { get; set; }

        [Display(Name = "Meta leírás")]        
        public string Description { get; set; }

        public bool DatasChanged { get; set; } = false;
    }
}