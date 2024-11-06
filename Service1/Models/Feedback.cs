using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Models
{
    public class Feedback
    {
        // public int Id { get; set; }
        public string PlaceName { get; set; }
        public string BackendOutput { get; set; }
        public string FeedbackComment { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}