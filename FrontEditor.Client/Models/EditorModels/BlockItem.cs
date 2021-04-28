
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.Models.EditorModels
{
    public class BlockItem
    {        
        
        [Display(Name = "Azonosító")]
        public string BlockItemId { get; set; }

        [Display(Name = "Cím")]
        public string Title { get; set; }
        
        [Display(Name = "Cím színe")]
        public string TitleColor { get; set; }
        
        [Display(Name = "Cím igazítása")]
        public AlignEnum TitleAlign { get; set; }        
        
        [Display(Name = "Tartalom")]
        public string Content { get; set; }

        [Display(Name = "Kiemelt kép")]
        public string ImageHref { get; set; }
        
        [Display(Name = "Kiemelt ikon")]
        public string IconName { get; set; }     

        [Display(Name = "Chart adatok")]
        public ChartType ChartType { get; set; }  
          
        [Display(Name = "Chart adatok")]
        public List<ChartRow> ChartRows { get; set; }     
        public BlockItem() 
        {
            ChartRows = new List<ChartRow>();
        }
    }
    public class ChartRow
    {
        [Display(Name = "Címke")]
        public string Label { get; set; }

        [Display(Name = "Adat")]
        public int Data { get; set; }
        
        [Display(Name = "Szín")]
        public string Color { get; set; }  
    }
}