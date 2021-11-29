using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorBlocksViewModel : EditorBaseViewModel
    {
        public EditorBlocksViewModel() : base("_blocksEditor", 0)
        {
            this.BlockItems = new List<BlockItem>();
        }

        public EditorBlocksViewModel(int projectId) : base("_blocksEditor", projectId)
        {
            this.BlockItems = new List<BlockItem>();
        }

        [Display(Name = "Szűkített nézet")]
        public bool ContainerMode { get; set; }

        [Display(Name = "Blokkok soronként")]
        public int ItemsPerRow { get; set; }

        [Display(Name = "Háttérszín")]
        public string BackgroundColor { get; set; }

        [Display(Name = "Blokkok")]
        public List<BlockItem> BlockItems { get; set; }

        internal void Update(EditBlocksComponentViewModel component)
        {
            this.ContainerMode = component.ContainerMode;
            this.ComponentId = component.ComponentId;
            this.ComponentName = component.ComponentName;
            this.ItemsPerRow = component.ItemsPerRow;
            this.BackgroundColor = component.BackgroundColor;
            this.BlockItems = component.BlockItems;
        }
    }
}
