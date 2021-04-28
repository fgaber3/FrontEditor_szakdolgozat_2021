using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class StatusItemViewModel
    {
        public string Name { get; set; }
        public string Status {get; set; }
        public bool IsActive { get; set; }
        public StatusItemViewModel(string name, string status, bool isactive) {
            Name = name;
            Status = status;
            IsActive = isactive;
        }
    }
}
