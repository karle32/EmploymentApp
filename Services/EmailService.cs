using ISMIEEmploymentApp.Models;
using Resend;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace ISMIEEmploymentApp.Services
{
    public class EmailService
    {
        private readonly IResend _resend;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IResend resend, ILogger<EmailService> logger)
        {
            _resend = resend;
            _logger = logger;
        }

        public async Task<bool> SendApplicationEmailAsync(Candidate application, byte[] pdfData)
        {
            try
            {
                var message = new EmailMessage
                {
                    From = "insert Resend registered email", // The email has to match the registered email in REsend
                    To = { "Add the person receiving the email here" }, // Add the person that would process the applications 
                    Subject = "New Employment Application Submission",
                    HtmlBody = GenerateEmailBody(application),
                    Attachments = new List<EmailAttachment>
                    {
                        new EmailAttachment
                        {
                            Filename = $"Application_{application.LastName}.pdf",
                            ContentType = MediaTypeNames.Application.Pdf,
                            Content = pdfData 
                        }
                    }
                };

                var response = await _resend.EmailSendAsync(message);
                _logger.LogInformation("Email sent successfully, ID: {EmailId}", response.Content);
                return true;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Email sending failed: {Message}", ex.Message);
                return false;
            }
        }

        private string GenerateEmailBody(Candidate application)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h2>New Employment Application Submitted</h2>");
            sb.AppendLine($"<p><strong>Name:</strong> {application.FirstName} {application.LastName}</p>");
            sb.AppendLine($"<p><strong>Phone:</strong> {application.Phone}</p>");
            sb.AppendLine($"<p><strong>Email:</strong> {application.HearAboutUs ?? "N/A"}</p>");
            sb.AppendLine($"<p><strong>Authorized to Work:</strong> {(application.AuthorizedToWork ? "Yes" : "No")}</p>");
            sb.AppendLine($"<p>See attached PDF for full details.</p>");
            return sb.ToString();
        }
    }
}
