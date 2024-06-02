using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_GCM.Models
{
    public partial class RendezVou
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        public DateTime? DateR { get; set; }
        public TimeSpan? HeureR { get; set; }
        public string? Status { get; set; }

        public int? IdPatient { get; set; }
        public int? IdMedecin { get; set; }

        [ForeignKey("IdMedecin")]
        public virtual Medecin? IdMedecinNavigation { get; set; }

        [ForeignKey("IdPatient")]
       
        public virtual Patient? IdPatientNavigation { get; set; }

        public string? NomPatient  => IdPatientNavigation?.NomP;
        public string? EmailP => IdPatientNavigation?.Email;
        public string? PrenomPatient  => IdPatientNavigation?.PrenomP;
        public string? NomM  => IdMedecinNavigation?.NomM;
        public string? PrenomM  => IdMedecinNavigation?.PrenomM;
        public string? EmailM => IdMedecinNavigation?.Email;
    }
}
