using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FrontEditor.Client.Models.Entities
{
    public class User : IdentityUser<int>
    {
        public string DisplayName { get; set; }
        public byte[] ProfileImage { get; set; }
        public DateTime Registration { get; set; }
        public DateTime LastActive { get; set; }
    }
}