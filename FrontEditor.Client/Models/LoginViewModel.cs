using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Az email cím mező kötelező!")]
        [Display(Name = "E-mail cím")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A jelszó cím mező kötelező!")]
        [Display(Name = "Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Maradjak bejelentkezve")]
        public bool RememberMe { get; set; }   

        public string ErrorText { get; set; }
    }
}
