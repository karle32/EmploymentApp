using System;
using System.ComponentModel.DataAnnotations;

namespace ISMIEEmploymentApp.Models
{
    public class Candidate
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public string AltName { get; set; } = string.Empty;        

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public int LivedAtAddress { get; set; }

        public string? Address2 { get; set; }
        public string? City2 { get; set; }
        public string? StateProvince2 { get; set; }
        public string? ZipPostalCode2 { get; set; }
        public string? Country2 { get; set; } = string.Empty;
        public int? LivedAtAddress2 { get; set; }

        public bool LegalAge { get; set; }
        public DateTime DataAvailable { get; set; }
        public bool AuthorizedToWork { get; set; }
        public bool SponsorshipNeeded { get; set; }
        public string? HearAboutUs { get; set; }

        // Relationships
        public List<CandidateAddress> Addresses { get; set; } = new();
    }
}
