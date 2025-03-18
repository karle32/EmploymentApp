using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ISMIEEmploymentApp.Models
{
    public class CandidateAddress
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; } 

        [Required, MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string StateProvince { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string ZipPostalCode { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        public int LivedAtAddress { get; set; }
    }

}
