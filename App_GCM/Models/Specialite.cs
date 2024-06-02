using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_GCM.Models
{
    public partial class Specialite
    {
        public Specialite()
        {
            Medecins = new HashSet<Medecin>();
        }


        public int Id { get; set; }
        public string? Specialite1 { get; set; }

        
        public virtual ICollection<Medecin>? Medecins { get; set; }
    }
}
