using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Az email cím mező kötelező!")]
        [Display(Name = "E-mail cím")]
        [EmailAddress]
        public string Email { get; set; }
                

        [Required(ErrorMessage = "A felhasználónév mező kötelező!")]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Az új jelszó mező kötelező!")]
        [Display(Name = "Új jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Új jelszó ismét")]
        [Compare("Password", ErrorMessage ="Jelszavak nem egyeznek.")]
        public string ConfirmPassword { get; set; }

        public string ErrorText { get; set; }
    }
}
