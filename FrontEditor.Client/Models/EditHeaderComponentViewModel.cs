using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.EditorModels;

namespace FrontEditor.Client.Models
{
    public class EditHeaderComponentViewModel
    {
        public int ProjectId { get; set; }
        public int ComponentIndex { get; set; }


        [Display(Name = "Komponens neve")]
        [Required(ErrorMessage = "A komponens név kötelező!")]
        public string ComponentName { get; set; }

        [Display(Name = "Komponens azonosító")]
        [Required(ErrorMessage = "A komponens azonosító kötelező!")]
        public string ComponentId { get; set; }

        [Display(Name = "Oldal címe")]
        [Required(ErrorMessage = "A cím mező kötelező!")]
        public string Title { get; set; }

        [Display(Name = "Ikon")]
        public string Icon { get; set; }

        [Display(Name = "Logó kép")]
        public string LogoImage { get; set; }

        [Display(Name = "Rögzítve az oldal tetején")]
        public bool FixedMenu { get; set; }

        [Display(Name = "Mobil menü nézet")]
        public bool MobileMenu { get; set; }

        [Display(Name = "Szűkített nézet")]
        public bool ContainerMode { get; set; }

        [Display(Name = "Keresőmező megjelenítése")]
        public bool ShowSearchBox { get; set; }

        [Display(Name = "Színséma")]
        public ColorShemeEnum ColorSheme { get; set; }

        [Display(Name = "Háttérszín")]
        public string BackgroundColor { get; set; }

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

        public string LogoType
        {
            get
            {
                if (!string.IsNullOrEmpty(this.LogoImage)) return "image";
                else if (!string.IsNullOrEmpty(this.Icon)) return "icon";
                return "none";
            }
        }

        public EditHeaderComponentViewModel()
        {
            this.MenuItems = new List<MenuItem>();
        }

        public EditHeaderComponentViewModel(EditorHeaderViewModel model, int projectId, int componentIndex)
        {
            this.ProjectId = projectId;
            this.ComponentIndex = componentIndex;
            this.ComponentId = model.ComponentId;
            this.ComponentName = model.ComponentName;
            this.Title = model.Title;
            this.Icon = model.Icon;
            this.LogoImage = model.LogoImage;
            this.FixedMenu = model.FixedMenu;
            this.MobileMenu = model.MobileMenu;
            this.ContainerMode = model.ContainerMode;
            this.ShowSearchBox = model.ShowSearchBox;
            this.ColorSheme = model.ColorSheme;
            this.BackgroundColor = model.BackgroundColor;
            this.MenuItems = model.MenuItems;
        }
    }
}
