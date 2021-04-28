using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "A megjelenítendő név mező kötelező!")]
        [Display(Name = "Megjelenítendő név")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "A felhasználónév mező kötelező!")]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A e-mail cím mező kötelező!")]
        [Display(Name = "E-mail cím")]
        [EmailAddress(ErrorMessage = "Nem megfelelő e-mail cím formátum!")]  
        public string Email { get; set; }

        [Required(ErrorMessage = "A jelszó mező kötelező!")]
        [Display(Name = "Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Jelszó ismét")]
        [Compare("Password", ErrorMessage ="Jelszavak nem egyeznek.")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Elfogadom a felhasználási feltételeket")]
        [Compare("isTrue", ErrorMessage ="Fogadja el a felhasználási feltételeket!")]   
        public bool AcceptPolicy { get; set; }   
        public bool isTrue { get { return true; } }

         public string ErrorText { get; set; }
    }
}