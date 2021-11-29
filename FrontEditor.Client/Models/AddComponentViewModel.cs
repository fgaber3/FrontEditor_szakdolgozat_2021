using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEditor.Client.Models.Enums;

namespace FrontEditor.Client.Models
{
    public class AddComponentViewModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Komponens típusa")]
        public ComponentType SelectedType { get; set; }
        public List<KeyValuePair<string, ComponentType>> ComponentTypes
        {
            get
            {
                return new List<KeyValuePair<string, ComponentType>>() {
                    new KeyValuePair<string, ComponentType>("Képlapozó", ComponentType.Carousel),
                    new KeyValuePair<string, ComponentType>("Blokkok", ComponentType.Blocks)
                };
            }
        }
    }
}
