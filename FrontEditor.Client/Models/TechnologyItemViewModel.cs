using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEditor.Client.Models
{
    public class TechnologyItemViewModel
    {
        public string Name { get; set; }
        public string Url {get; set; }
        public string Version { get; set; }
        public string License { get; set; }
        
        public TechnologyItemViewModel(string name, string url, string version, string license) {
            Name = name;
            Url = url;
            Version = version;
            License = license;
        }
    }
}
