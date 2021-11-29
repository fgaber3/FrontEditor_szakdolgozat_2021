using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorHeaderViewModel : EditorBaseViewModel
    {
        public EditorHeaderViewModel() : base("_headerEditor", 0)
        {
            this.MenuItems = new List<MenuItem>();
        }
        public EditorHeaderViewModel(int projectId) : base("_headerEditor", projectId)
        {
            this.MenuItems = new List<MenuItem>();
        }

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

        public void Update(EditHeaderComponentViewModel model)
        {
            this.ProjectId = model.ProjectId;
            this.ComponentName = model.ComponentName;
            this.ComponentId = model.ComponentId;
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
