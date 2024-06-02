using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_GCM.Models
{
    public partial class Infermier
    {
        public int Id { get; set; }
        public string? NomInf { get; set; }
        public string? PrenomInf { get; set; }
        public string? Nomtele { get; set; }
        public string? Email { get; set; }
        public int? IdMedecin { get; set; }

        [ForeignKey("IdMedecin")]
        public virtual Medecin? IdMedecinNavigation { get; set; }
        public string? NomM=> IdMedecinNavigation?.NomM;
        public string? PrenomM => IdMedecinNavigation?.PrenomM;
    }
}
