namespace ISMIEEmploymentApp.Models
{
    public class Candidate
    {
        public DateTime Date { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AltName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StateProvince { get; set; } = string.Empty;
        public string ZipPostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
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
    }
}
