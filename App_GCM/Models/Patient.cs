using System;
using System.Collections.Generic;

namespace App_GCM.Models
{
    public partial class Patient
    {
        public Patient()
        {
            DossiersMedicauxes = new HashSet<DossiersMedicaux>();
            RendezVous = new HashSet<RendezVou>();
        }

        public int Id { get; set; }
        public string? NomP { get; set; }
        public string? PrenomP { get; set; }
        public string? Cin { get; set; }
        public string? Numtel { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<DossiersMedicaux> DossiersMedicauxes { get; set; }
        public virtual ICollection<RendezVou> RendezVous { get; set; }
    }
}
