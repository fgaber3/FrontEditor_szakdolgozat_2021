
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Entities;

namespace FrontEditor.Client.Models
{
    public class UserViewModel
    {        
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "A név mező kötelező!")]
        [Display(Name = "Név")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "A felhasználónév mező kötelező!")]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }
                
        [Display(Name = "Szerepkör")]
        public int RoleId { get; set; }
        
        [Required(ErrorMessage = "Az e-mail cím mező kötelező!")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Nem megfelelő e-mail cím formátum!")]        
        public string Email { get; set; }
        public int ProjectsCount { get; set; }
        public DateTime Registration { get; set; }
        public DateTime LastActive { get; set; }
        public string Password { get; set; }
        
        [Display(Name = "Jelszó")]
        public string NewPassword { get; set; }
        
        [Display(Name = "Jelszó ismét")]
        public string NewPasswordAgain { get; set; }
        public bool DatasChanged { get; set; } = false;
        public bool NewUser { get; set; } = false;
        public List<KeyValuePair<string, int>> Roles
        {
            get
            {
                return new List<KeyValuePair<string, int>>() {
                    new KeyValuePair<string, int>("Admin", 1),
                    new KeyValuePair<string, int>("User", 2)
                };
            }
        }     
        public string RoleName
        {
            get
            {
                return Roles.Find(x => x.Value == RoleId).Key;
            }
        }
        public string ErrorText { get; set; }
        public UserViewModel() 
        {
            
        }
        public UserViewModel(User user, int roleId, int projects = 0) 
        {
            UserId = user.Id;
            DisplayName = user.DisplayName;
            UserName = user.UserName;
            RoleId = roleId;
            ProjectsCount = projects;
            Email = user.Email;
            Registration = user.Registration;
            LastActive = user.LastActive;
        }
    }
}