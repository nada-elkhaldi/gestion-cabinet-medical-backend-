using System;
using System.Collections.Generic;

namespace App_GCM.Models
{
    public partial class Personnel
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Tele { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }
 
    }
}
