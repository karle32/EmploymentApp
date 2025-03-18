using ISMIEEmploymentApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text;

namespace ISMIEEmploymentApp.Services
{
    public class PdfService
    {
        public byte[] GenerateApplicationPdf(Candidate application)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var primaryAddress = application.Addresses.FirstOrDefault();
            var secondaryAddress = application.Addresses.Skip(1).FirstOrDefault();

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text("Employment Application").Bold().FontSize(18);
                        col.Item().Text($"Date: {application.Date:yyyy-MM-dd}");
                        col.Item().Text($"Name: {application.FirstName} {application.LastName}");
                        col.Item().Text($"Alternative Name: {application.AltName ?? "N/A"}");

                        col.Item().Text("Primary Address:");
                        col.Item().Text(primaryAddress != null
                            ? $"{primaryAddress.Address}, {primaryAddress.City}, {primaryAddress.StateProvince}, {primaryAddress.ZipPostalCode}"
                            : "N/A");
                        col.Item().Text($"Phone: {application.Phone} | Lived At: {primaryAddress?.LivedAtAddress ?? 0} years");

                        col.Item().Text("Secondary Address:");
                        col.Item().Text(secondaryAddress != null
                            ? $"{secondaryAddress.Address}, {secondaryAddress.City}, {secondaryAddress.StateProvince}, {secondaryAddress.ZipPostalCode}"
                            : "N/A");

                        col.Item().Text($"Legal Age: {(application.LegalAge ? "Yes" : "No")}");
                        col.Item().Text($"Date Available: {application.DataAvailable:yyyy-MM-dd}");
                        col.Item().Text($"Authorized to Work: {(application.AuthorizedToWork ? "Yes" : "No")}");
                        col.Item().Text($"Sponsorship Needed: {(application.SponsorshipNeeded ? "Yes" : "No")}");
                        col.Item().Text($"Heard About Us: {application.HearAboutUs ?? "N/A"}");
                    });
                });
            }).GeneratePdf();
        }
    }
}
