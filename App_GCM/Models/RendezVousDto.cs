namespace App_GCM.Models
{
    public class RendezVousDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public DateTime? DateR { get; set; }
        public TimeSpan? HeureR { get; set; }
        public string? Status { get; set; }

        public int? IdMedecin { get; set; }
        public int? IdPatient { get; set; }
        public string? NomPatient { get; set; }
    
        public string? PrenomPatient { get; set; }
        public string? NomMedecin { get; set; }
        public string? PrenomMedecin { get; set; }
    
    }
}
