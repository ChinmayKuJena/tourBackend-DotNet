using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service1.Models;

namespace Service1.Services
{

// """
// TODO lots of things in this 
// """
    public class FeedbackService
    {
        private readonly string feedbackFilePath = "feedback.txt";  // You can adjust the path as needed

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            // Format the feedback entry as text
            var feedbackText = $"[{feedback.SubmittedAt}] {feedback.PlaceName}: {feedback.BackendOutput} : {feedback.FeedbackComment}\n";
            
            // Append the feedback to the file
            await File.AppendAllTextAsync(feedbackFilePath, feedbackText);
        }
    
    }
}