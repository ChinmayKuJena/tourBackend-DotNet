namespace Service1.Models
{
    public class ImageRecognitionResultWithLabels
    {
        public int ResultId { get; set; }
        public string PlaceName { get; set; }
        public bool IsPlaceImage { get; set; }
        public string S3Url { get; set; }
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public double LabelPercentage { get; set; }
    }
}
