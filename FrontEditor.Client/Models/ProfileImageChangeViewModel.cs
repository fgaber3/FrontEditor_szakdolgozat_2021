using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FrontEditor.Client.Models
{
    public class ProfileImageChangeViewModel
    {
        [Display(Name = "Projekt kép")]        
        [Required(ErrorMessage = "Nincs kép kiválasztva!")]
        public IFormFile Image { get; set; }
        public string ErrorText { get; set; }
    }
}
