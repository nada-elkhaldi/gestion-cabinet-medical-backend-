using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace App_GCM.Models
{
    public partial class Medecin
    {
        public Medecin()
        {
            Infermiers = new HashSet<Infermier>();
            RendezVous = new HashSet<RendezVou>();
 
        }

        public int Id { get; set; }
        public string? NomM { get; set; }
        public string? PrenomM { get; set; }
        public string? Numtel { get; set; }
        public string? Email { get; set; }
        public int? Occupe { get; set; }
        public int? IdSpecialite { get; set; }
       

        [ForeignKey("IdSpecialite")]
        public Specialite? IdSpecialiteNavigation { get; set; }
 

        public string? Specialite1 => IdSpecialiteNavigation?.Specialite1;

        public virtual ICollection<Infermier>? Infermiers { get; set; }

     

        [JsonIgnore]
        public virtual ICollection<RendezVou>? RendezVous { get; set; }

        public virtual ICollection<DossiersMedicaux>? DossiersMedicauxes { get; set; }
    }
}