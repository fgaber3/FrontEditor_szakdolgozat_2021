using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.EditorModels;

namespace FrontEditor.Client.Models
{
    public class EditBlocksComponentViewModel
    {
        public int ProjectId { get; set; }
        public int ComponentIndex { get; set; }

        [Display(Name = "Komponens neve")]
        [Required(ErrorMessage = "A komponens név kötelező!")]
        public string ComponentName { get; set; }

        [Display(Name = "Komponens azonosító")]
        [Required(ErrorMessage = "A komponens azonosító kötelező!")]
        public string ComponentId { get; set; }

        [Display(Name = "Szűkített nézet")]
        public bool ContainerMode { get; set; }

        [Display(Name = "Blokkok soronként")]
        public int ItemsPerRow { get; set; }

        [Display(Name = "Háttérszín")]
        public string BackgroundColor { get; set; }

        [Display(Name = "Blokkok")]
        public List<BlockItem> BlockItems { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorText { get; set; }

        public EditBlocksComponentViewModel()
        {
            this.BlockItems = new List<BlockItem>();
        }

        public EditBlocksComponentViewModel(EditorBlocksViewModel model, int projectId, int componentIndex)
        {
            this.ProjectId = projectId;
            this.ComponentIndex = componentIndex;
            this.ComponentId = model.ComponentId;
            this.ComponentName = model.ComponentName;
            this.ContainerMode = model.ContainerMode;
            this.ItemsPerRow = model.ItemsPerRow;
            this.BackgroundColor = model.BackgroundColor;
            this.BlockItems = model.BlockItems;
        }
    }
}
