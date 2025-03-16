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
                        col.Item().Text($"{application.Address}, {application.City}, {application.StateProvince}, {application.ZipPostalCode}");
                        col.Item().Text($"Phone: {application.Phone} | Lived At: {application.LivedAtAddress} years");

                        col.Item().Text("Secondary Address:");
                        col.Item().Text($"{application.Address2 ?? "N/A"}, {application.City2 ?? "N/A"}, {application.StateProvince2 ?? "N/A"}, {application.ZipPostalCode2 ?? "N/A"}");
                        col.Item().Text($"Phone: {application.Phone ?? "N/A"} | Lived At: {application.LivedAtAddress2 ?? 0} years");

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
