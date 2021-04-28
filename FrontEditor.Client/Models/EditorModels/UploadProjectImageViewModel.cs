using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FrontEditor.Client.Models.EditorModels
{
    public class UploadProjectImageViewModel
    {
        [Required(ErrorMessage = "Nincs projekt kiválasztva!")]
        public int ProjectId { get; set; }                

        [Display(Name = "Projekt kép")]        
        [Required(ErrorMessage = "Nincs kép kiválasztva!")]
        public IFormFile Image { get; set; }
        public string ErrorText { get; set; }
        public string UploadedFileName { get; set; }
    }
}
