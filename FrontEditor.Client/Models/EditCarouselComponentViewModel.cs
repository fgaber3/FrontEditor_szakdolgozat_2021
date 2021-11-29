using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.EditorModels;

namespace FrontEditor.Client.Models
{
    public class EditCarouselComponentViewModel
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

        [Display(Name = "Irányítható")]
        public bool Controls { get; set; }

        [Display(Name = "Jelölők")]
        public bool Indicators { get; set; }

        [Display(Name = "Automatikus lapozás")]
        public bool AutoRun { get; set; }

        [Display(Name = "Feliratok megjelenítése")]
        public bool Captions { get; set; }

        [Display(Name = "Feliratok színe")]
        public string CaptionsTextColor { get; set; }

        [Display(Name = "Feliratok háttérszíne")]
        public string CaptionsTextBackgroundColor { get; set; }

        [Display(Name = "Animáció típusa")]
        public SliderAnimationModeEnum AnimationMode { get; set; }

        [Display(Name = "Magasság (px)")]
        public int HeightSize { get; set; }

        [Display(Name = "Lapozási idő (ms)")]
        public int PageInterval { get; set; }

        [Display(Name = "Lapozó elemek")]
        public List<CarouselItem> CarouselItems { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorText { get; set; }

        public List<KeyValuePair<string, SliderAnimationModeEnum>> AnimationTypes
        {
            get
            {
                return new List<KeyValuePair<string, SliderAnimationModeEnum>>() {
                    new KeyValuePair<string, SliderAnimationModeEnum>("Áttűnés", SliderAnimationModeEnum.Fade),
                    new KeyValuePair<string, SliderAnimationModeEnum>("Lapozás", SliderAnimationModeEnum.Slide)
                };
            }
        }

        public EditCarouselComponentViewModel()
        {
            this.CarouselItems = new List<CarouselItem>();
        }

        public EditCarouselComponentViewModel(EditorCarouselViewModel model, int projectId, int componentIndex)
        {
            this.ProjectId = projectId;
            this.ComponentIndex = componentIndex;
            this.ComponentId = model.ComponentId;
            this.ComponentName = model.ComponentName;
            this.Controls = model.Controls;
            this.Indicators = model.Indicators;
            this.AutoRun = model.AutoRun;
            this.Captions = model.Captions;
            this.CaptionsTextColor = model.CaptionsTextColor;
            this.ContainerMode = model.ContainerMode;
            this.CaptionsTextBackgroundColor = model.CaptionsTextBackgroundColor;
            this.AnimationMode = model.AnimationMode;
            this.HeightSize = model.HeightSize;
            this.PageInterval = model.PageInterval;
            this.CarouselItems = model.CarouselItems;
        }
    }
}
