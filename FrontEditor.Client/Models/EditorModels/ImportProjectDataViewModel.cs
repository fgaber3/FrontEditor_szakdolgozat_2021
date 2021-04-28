using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FrontEditor.Client.Models.EditorModels
{
    public class ImportProjectDataViewModel
    {
        public int ProjectId { get; set; }                
        [Display(Name = "Korábbi exportált projekt fájl")]        
        [Required(ErrorMessage = "Nincs mentés kiválasztva!")]
        public IFormFile FromFile { get; set; }
        public string ErrorText { get; set; }
        public bool ImportSuccess { get; set; }
    }
}
