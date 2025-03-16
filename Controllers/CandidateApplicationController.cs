using ISMIEEmploymentApp.Models;
using ISMIEEmploymentApp.Repository;
using ISMIEEmploymentApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISMIEEmploymentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationController : ControllerBase
    {
        private readonly ICandidateApplicationRepository _repository;
        private readonly PdfService _pdfService;
        private readonly EmailService _emailService;
        private readonly ILogger<CandidateApplicationController> _logger;

        public CandidateApplicationController(
            ICandidateApplicationRepository repository,
            PdfService pdfService,
            EmailService emailService,
            ILogger<CandidateApplicationController> logger )
        {
            _repository = repository;
            _pdfService = pdfService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAppication([FromBody] Candidate application) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Save application to mock database
                _repository.Add(application);

                // Generate PDF
                var pdfBytes = _pdfService.GenerateApplicationPdf(application);

                // Send Email
                var emailSent = await _emailService.SendApplicationEmailAsync(application, pdfBytes);

                if (!emailSent)
                {
                    _logger.LogError("Failed to send email for application submitted by {Applicant}", application.LastName);
                    return StatusCode(500, "Failed to send email.");
                }

                _logger.LogInformation("Application submitted and email sent successfully for {Applicant}", application.LastName);
                return Ok(new { message = "Application submitted and emailed successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing application for {Applicant}", application.LastName);
                return StatusCode(500, "An unexpected error occurred. Please try again.");
            }
        }

        [HttpGet]
        public IActionResult GetApplications()
        {
            var applications = _repository.GetAll();
            return Ok(applications);
        }
    }
}
