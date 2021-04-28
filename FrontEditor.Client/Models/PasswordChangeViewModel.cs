using System;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class PasswordChangeViewModel
    {
        public bool PasswordChanged { get; set; } = false;

        [Required(ErrorMessage = "Az új jelszó mező kötelező!")]
        [Display(Name = "Új jelszó")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Új jelszó ismét")]
        [Compare("NewPassword", ErrorMessage ="Jelszavak nem egyeznek.")]
        public string ConfirmNewPassword { get; set; }

        public string ErrorText { get; set; }
    }
}
