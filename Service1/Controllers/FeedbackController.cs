using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service1.Models;
using Service1.Services;

namespace Service1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitFeedback([FromBody] Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback.PlaceName) || 
                string.IsNullOrWhiteSpace(feedback.BackendOutput) || 
                string.IsNullOrWhiteSpace(feedback.FeedbackComment))
            {
                return BadRequest("Place name, backend output, and feedback comment are required.");
            }

            await _feedbackService.SaveFeedbackAsync(feedback);
            return Ok("Thank you for your feedback!");
        }
    }
}