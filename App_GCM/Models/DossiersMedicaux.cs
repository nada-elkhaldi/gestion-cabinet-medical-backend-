using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_GCM.Models
{
    public partial class DossiersMedicaux
    {
        public int Id { get; set; }
        public string? TypeTraitement { get; set; }
        public string? AntecedentsMed { get; set; }
        public int? IdPatient { get; set; }

        public int? IdMedecin { get; set; }

        [ForeignKey("IdPatient")]
        public virtual Patient? IdPatientNavigation { get; set; }
        [ForeignKey("IdMedecin")]
        public virtual Medecin? IdMedecinNavigation { get; set; }

        public string? NomP => IdPatientNavigation?.NomP;
        public string? PrenomP => IdPatientNavigation?.PrenomP;
        public string? NomM  => IdMedecinNavigation?.NomM;
        public string? PrenomM => IdMedecinNavigation?.PrenomM;
    }
}
