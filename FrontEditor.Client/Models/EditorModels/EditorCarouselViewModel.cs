using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models.EditorModels
{
    public class EditorCarouselViewModel : EditorBaseViewModel
    {
        public EditorCarouselViewModel() : base("_carouselEditor", 0)
        {
            this.CarouselItems = new List<CarouselItem>();
        }

        public EditorCarouselViewModel(int projectId) : base("_carouselEditor", projectId)
        {
            this.CarouselItems = new List<CarouselItem>();
        }

        [Display(Name = "Szűkített nézet")]
        public bool ContainerMode { get; set; }

        [Display(Name = "Irányítás")]
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

        [Display(Name = "Animáció típuse")]
        public SliderAnimationModeEnum AnimationMode { get; set; }

        [Display(Name = "Magasság (px)")]
        public int HeightSize { get; set; }

        [Display(Name = "Lapozási idő (ms)")]
        public int PageInterval { get; set; }

        [Display(Name = "Lapozó elemek")]
        public List<CarouselItem> CarouselItems { get; set; }

        internal void Update(EditCarouselComponentViewModel component)
        {
            this.ContainerMode = component.ContainerMode;
            this.ComponentId = component.ComponentId;
            this.ComponentName = component.ComponentName;
            this.Controls = component.Controls;
            this.Indicators = component.Indicators;
            this.AutoRun = component.AutoRun;
            this.Captions = component.Captions;
            this.CaptionsTextColor = component.CaptionsTextColor;
            this.CaptionsTextBackgroundColor = component.CaptionsTextBackgroundColor;
            this.AnimationMode = component.AnimationMode;
            this.HeightSize = component.HeightSize;
            this.PageInterval = component.PageInterval;
            this.CarouselItems = component.CarouselItems;
        }
    }
}
