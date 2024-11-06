namespace Service1.Models
{
    public class ImageRecognitionResult
    {
        public int ResultId { get; set; }
        public string PlaceName { get; set; }
        public bool IsPlaceImage { get; set; }
        public string S3Url { get; set; }
        public List<DetectedLabel> DetectedLabels { get; set; } = new List<DetectedLabel>();
    }
}
