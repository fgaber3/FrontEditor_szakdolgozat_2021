using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontEditor.Client.Models;
using FrontEditor.Client.Models.Entities;
using FrontEditor.Client.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace FrontEditor.Client.BusinessLogic
{
    public static class UsersBL 
    {
        public static List<UserViewModel> GetUserList(FrontEditorContext _context)
        {
            List<UserViewModel> response = new List<UserViewModel>();
            List<User> users = _context.Users.ToList();
            foreach(User user in users)
            {
                int projects = _context.Projects.Where(x => x.Owner == user).Count();
                IdentityUserRole<int> urole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();                 
                if(urole == null)                    
                    response.Add(new UserViewModel(user, 2, projects));
                else
                    response.Add(new UserViewModel(user, urole.RoleId, projects));
            }
            return response;
        }

        public static UserViewModel GetUser(FrontEditorContext _context, int uId)
        {
            User user = _context.Users.Where(x => x.Id == uId).FirstOrDefault();
            int projects = _context.Projects.Where(x => x.Owner == user).Count();
            IdentityUserRole<int> urole = _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
            return new UserViewModel(user, urole.RoleId, projects);
        }
        
        public static async Task<UserViewModel> CreateUser(FrontEditorContext _context, UserManager<User> _userManager, UserViewModel model)
        {
            try
            {
                User user = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if(user != null)
                {
                    throw new Exception("Ezzel az e-mail címmel már létezik felhasználó!");
                }
                else 
                {
                    user = new User
                    {       
                        DisplayName = model.DisplayName,         
                        UserName = model.UserName,
                        Email = model.Email,
                        LastActive = DateTime.Now,
                        Registration = DateTime.Now
                    };
                    model.NewPassword = GeneratePassword(_userManager);
                    var result = await _userManager.CreateAsync(user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        _context.UserRoles.Add(new IdentityUserRole<int>(){ UserId = user.Id, RoleId = model.RoleId });
                        _context.SaveChanges();                                
                        model.UserId = user.Id;
                    }              
                    foreach (var error in result.Errors)
                    {
                        if(error.Code == "DuplicateUserName") 
                            model.ErrorText += "Ezzel a felhasználónévvel már létezik felhasználó!<br/>";
                        else if(error.Code == "PasswordTooShort") 
                            model.ErrorText += "A jelszó legalább 6 karakter!<br/>";
                        else if(error.Code == "PasswordRequiresNonAlphanumeric") 
                            model.ErrorText += "A jelszó tartalmaz legalább egy nem alfanumerikus karaktert!<br/>";
                        else if(error.Code == "PasswordRequiresDigit") 
                            model.ErrorText += "A jelszó legalább egy számot tartalmaz!<br/>";
                        else if(error.Code == "PasswordRequiresUpper") 
                            model.ErrorText += "A jelszó legalább egy nagybetűt tartalmaz!<br/>";
                        else if(error.Code == "PasswordRequiresLower") 
                            model.ErrorText += "A jelszó legalább egy kisbetűt tartalmaz!<br/>";                        
                        else
                            model.ErrorText += error.Description + "<br/>";
                    }
                    model.NewUser = true;         
                    model.Registration = DateTime.Now;
                    model.LastActive = model.Registration;                                                       
                    model.DatasChanged = true;            
                }
            }
            catch(Exception ex)
            {
                model.ErrorText = ex.Message;
            }
            return model;
        }

        public static UserViewModel SaveUserProfile(FrontEditorContext _context, UserViewModel model)
        {
            User user = _context.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            user.DisplayName = model.DisplayName;
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName.ToUpper();
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            _context.SaveChanges();
            return new UserViewModel(user, model.RoleId);
        }

        public static UserViewModel EditUserProfile(FrontEditorContext _context, UserViewModel model)
        {
            User user = _context.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            user.DisplayName = model.DisplayName;
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName.ToUpper();
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            IdentityUserRole<int> userRole = _context.UserRoles.Where(x => x.UserId == model.UserId).FirstOrDefault();
            userRole.RoleId = model.RoleId;
            _context.SaveChanges();
            return new UserViewModel(user, model.RoleId) { DatasChanged = true };
        }

        public static async Task<UserViewModel> ResetPassword(FrontEditorContext _context, UserManager<User> _userManager, int userId)
        {
            UserViewModel model = new UserViewModel();
            User usr = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            string newPass = UsersBL.GeneratePassword(_userManager);
            await _userManager.RemovePasswordAsync(usr);
            var result = await _userManager.AddPasswordAsync(usr, newPass);
            if(result.Succeeded)
            {
                model.NewPassword = newPass;
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {                    
                    model.ErrorText += error.Description + "<br />";
                }                
            }
            return model;
        }

        public static void RemoveUser(FrontEditorContext _context, int uId)
        {
            User user = _context.Users.Where(x => x.Id == uId).FirstOrDefault();
            List<Project> projects = _context.Projects.Where(x => x.Owner == user).ToList();
            foreach(Project proj in projects) 
            {
                _context.Projects.Remove(proj);
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public static string GeneratePassword(UserManager<User> _userManager)
        {
            var options = _userManager.Options.Password;
            int length = options.RequiredLength;
            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);
                password.Append(c);
                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public static async Task<PasswordChangeViewModel> ChangePassword(FrontEditorContext _context, UserManager<User> _userManager, System.Security.Claims.ClaimsPrincipal User, PasswordChangeViewModel model)
        {
            User user = await _userManager.GetUserAsync(User);                
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (result.Succeeded)
            {
                model = new PasswordChangeViewModel() {
                    PasswordChanged = true
                };              
            }   
            else
            {           
                foreach (var error in result.Errors)
                {
                    if(error.Code == "PasswordTooShort") 
                        model.ErrorText += "A jelszó legalább 6 karakter!<br />";
                    else if(error.Code == "PasswordRequiresNonAlphanumeric") 
                        model.ErrorText += "A jelszó tartalmaz legalább egy nem alfanumerikus karaktert!<br />";
                    else if(error.Code == "PasswordRequiresDigit") 
                        model.ErrorText += "A jelszó legalább egy számot tartalmaz!<br />";
                    else if(error.Code == "PasswordRequiresUpper") 
                        model.ErrorText += "A jelszó legalább egy nagybetűt tartalmaz!<br />";
                    else if(error.Code == "PasswordRequiresLower") 
                        model.ErrorText += "A jelszó legalább egy kisbetűt tartalmaz!<br />";         
                    else
                        model.ErrorText += error.Description + "<br />";
                } 
            }
            return model;
        }

        public static async Task<LoginViewModel> Login(FrontEditorContext _context, SignInManager<User> _signInManager, LoginViewModel model)
        {
            User usr = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if(usr == null)
            {
               model.ErrorText += "Felhasználó nem található!";
            }
            else 
            {                   
                var result = await _signInManager.PasswordSignInAsync(usr, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    usr.LastActive = DateTime.Now;
                    _context.SaveChanges();
                }
                else
                {
                    model.ErrorText += "Nem megfelelő bejelentkezési adatok!";
                }
            }
            return model;
        }

        public static async Task<RegisterViewModel> Register(FrontEditorContext _context, UserManager<User> _userManager, SignInManager<User> _signInManager, RegisterViewModel model)
        {
            User user = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if(user != null)
            {
                model.ErrorText += "Ezzel az e-mail címmel már létezik felhasználó!";
            }
            else 
            {
                user = new User
                {       
                    DisplayName = model.DisplayName,         
                    UserName = model.UserName,
                    Email = model.Email,
                    LastActive = DateTime.Now,
                    Registration = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _context.UserRoles.Add(new IdentityUserRole<int>(){UserId = user.Id, RoleId = 2});
                    _context.SaveChanges();
                }      
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        if(error.Code == "DuplicateUserName") 
                            model.ErrorText += "Ezzel a felhasználónévvel már létezik felhasználó!<br />";
                        else if(error.Code == "PasswordTooShort") 
                            model.ErrorText += "A jelszó legalább 6 karakter!<br />";
                        else if(error.Code == "PasswordRequiresNonAlphanumeric") 
                            model.ErrorText += "A jelszó tartalmaz legalább egy nem alfanumerikus karaktert!<br />";
                        else if(error.Code == "PasswordRequiresDigit") 
                            model.ErrorText += "A jelszó legalább egy számot tartalmaz!<br />";
                        else if(error.Code == "PasswordRequiresUpper") 
                            model.ErrorText += "A jelszó legalább egy nagybetűt tartalmaz!<br />";
                        else if(error.Code == "PasswordRequiresLower") 
                            model.ErrorText += "A jelszó legalább egy kisbetűt tartalmaz!<br />";                        
                        else
                            model.ErrorText += error.Description + "<br />";
                    }  
                }                 
            }
            return model;
        }
        
        public static async Task<PasswordResetViewModel> PasswordReset(FrontEditorContext _context, UserManager<User> _userManager, PasswordResetViewModel model)
        {
            User user = _context.Users.Where(
                    x => x.Email == model.Email 
                    && x.UserName == model.UserName).FirstOrDefault();

            if(user == null)
            {
                model.ErrorText += "Felhasználó nem található!";
            }
            else 
            {
                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, model.Password);
                if (!result.Succeeded)
                {          
                    foreach (var error in result.Errors)
                    {
                        if(error.Code == "PasswordTooShort") 
                            model.ErrorText += "A jelszó legalább 6 karakter!<br />";
                        else if(error.Code == "PasswordRequiresNonAlphanumeric") 
                            model.ErrorText += "A jelszó tartalmaz legalább egy nem alfanumerikus karaktert!<br />";
                        else if(error.Code == "PasswordRequiresDigit") 
                            model.ErrorText += "A jelszó legalább egy számot tartalmaz!<br />";
                        else if(error.Code == "PasswordRequiresUpper") 
                            model.ErrorText += "A jelszó legalább egy nagybetűt tartalmaz!<br />";
                        else if(error.Code == "PasswordRequiresLower") 
                            model.ErrorText += "A jelszó legalább egy kisbetűt tartalmaz!<br />";                        
                        else
                            model.ErrorText += error.Description + "<br />";
                    }
                }    
            }
            return model;
        }

        public static bool UserIsAdmin(FrontEditorContext _context, int userId)
        {
            return _context.UserRoles.Where(x => x.UserId == userId && x.RoleId == 1).FirstOrDefault() != null;
        }

    }
}