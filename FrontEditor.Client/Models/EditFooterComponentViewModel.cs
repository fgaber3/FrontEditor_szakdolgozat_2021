using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.EditorModels;

namespace FrontEditor.Client.Models
{
    public class EditFooterComponentViewModel 
    {
        public int ProjectId { get; set; }   
        public int ComponentIndex  { get; set; }
        
        [Display(Name = "Komponens neve")]
        [Required(ErrorMessage = "A komponens név kötelező!")]
        public string ComponentName  { get; set; }

        [Display(Name = "Komponens azonosító")]
        [Required(ErrorMessage = "A komponens azonosító kötelező!")]
        public string ComponentId { get; set; }

        [Display(Name = "Lábrész szöveg")]  
        public string CopyTextExtended { get; set; }   

        [Display(Name = "Igazítás")]
        public AlignEnum CopyTextAlign { get; set; }
 
        [Display(Name = "Szűkített nézet")]
        public bool ContainerMode { get; set; }   
        
        [Display(Name = "Színséma")]
        public ColorShemeEnum ColorSheme { get; set; }   

        [Display(Name = "Szöveg színe")]
        public string TextColor { get; set; }   

        [Display(Name = "Háttérszín")]
        public string BackgroundColor { get; set; }   

        [Display(Name = "Menü igazítása")]
        public AlignEnum MenuItemsAlign { get; set; }

        [Display(Name = "Menüelemek")]
        public List<MenuItem> MenuItems { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorText { get; set; }

        public List<KeyValuePair<string, ColorShemeEnum>> ColorShemes 
        { 
            get 
            {
                return new List<KeyValuePair<string, ColorShemeEnum>>() {
                    new KeyValuePair<string, ColorShemeEnum>("Világos", ColorShemeEnum.Light),
                    new KeyValuePair<string, ColorShemeEnum>("Sötét", ColorShemeEnum.Dark)
                };
            }
        }

        public List<KeyValuePair<string, AlignEnum>> AlignTypes
        { 
            get 
            {
                return new List<KeyValuePair<string, AlignEnum>>() {
                    new KeyValuePair<string, AlignEnum>("Balra", AlignEnum.Left),
                    new KeyValuePair<string, AlignEnum>("Jobbra", AlignEnum.Right),
                    new KeyValuePair<string, AlignEnum>("Középre", AlignEnum.Center)
                };
            }
        }

        

        public EditFooterComponentViewModel() {
            this.MenuItems = new List<MenuItem>();
         }

        public EditFooterComponentViewModel(EditorFooterViewModel model, int projectId, int componentIndex) {
            this.ProjectId = projectId;
            this.ComponentIndex = componentIndex;
            this.ComponentId = model.ComponentId;
            this.ComponentName = model.ComponentName;
            this.CopyTextExtended = model.CopyTextExtended;
            this.CopyTextAlign = model.CopyTextAlign;
            this.ContainerMode = model.ContainerMode;
            this.ColorSheme = model.ColorSheme;
            this.TextColor = model.TextColor;
            this.BackgroundColor = model.BackgroundColor;
            this.MenuItemsAlign = model.MenuItemsAlign;
            this.MenuItems = model.MenuItems;
        }
    }
}
