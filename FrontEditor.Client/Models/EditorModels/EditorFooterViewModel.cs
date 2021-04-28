using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorFooterViewModel : EditorBaseViewModel
    {
        public EditorFooterViewModel(int projectId) : base("_footerEditor", projectId)
        { 
            this.MenuItems = new List<MenuItem>();
        }

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
    }
}
