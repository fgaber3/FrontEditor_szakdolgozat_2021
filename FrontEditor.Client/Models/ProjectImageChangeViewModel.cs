using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FrontEditor.Client.Models
{
    public class ProjectImageChangeViewModel
    {
        [Required(ErrorMessage = "Nincs projekt kiválasztva!")]
        public int ProjectId { get; set; }                

        [Display(Name = "Projekt kép")]        
        [Required(ErrorMessage = "Nincs kép kiválasztva!")]
        public IFormFile Image { get; set; }
        public string ErrorText { get; set; }
    }
}
