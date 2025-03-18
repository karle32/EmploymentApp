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
                await _repository.AddAsync(application);

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
        public async Task<IActionResult> GetAllApplications()
        {
            try
            {
                var applications = await _repository.GetAllAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving applications.");
                return Problem("An error occurred while fetching applications.", statusCode: 500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            try
            {
                var candidate = await _repository.GetByIdAsync(id);
                if (candidate == null)
                    return NotFound($"Candidate with ID {id} not found.");

                return Ok(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving candidate with ID {Id}", id);
                return Problem("An error occurred while fetching the candidate.", statusCode: 500);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] Candidate updatedCandidate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingCandidate = await _repository.GetByIdAsync(id);
                if (existingCandidate == null)
                    return NotFound($"Candidate with ID {id} not found.");

                updatedCandidate.Id = id;  // Ensure correct ID
                await _repository.UpdateAsync(updatedCandidate);

                return Ok(new { message = "Candidate updated successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating candidate with ID {Id}", id);
                return Problem("An error occurred while updating the candidate.", statusCode: 500);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                var existingCandidate = await _repository.GetByIdAsync(id);
                if (existingCandidate == null)
                    return NotFound($"Candidate with ID {id} not found.");

                await _repository.DeleteAsync(id);
                return Ok(new { message = "Candidate deleted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting candidate with ID {Id}", id);
                return Problem("An error occurred while deleting the candidate.", statusCode: 500);
            }
        }
    }
}
