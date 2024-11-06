namespace Service1.Models
{
    public class ImageRecognitionResponse
    {
        public bool IsPlaceImage { get; set; }
        public string S3Url { get; set; }
        public List<DetectedLabel> DetectedLabels { get; set; }
        public string PlaceName { get; set; }

        public ImageRecognitionResponse(bool isPlaceImage, List<DetectedLabel> detectedLabels, string s3Url, string placeName)
        {
            IsPlaceImage = isPlaceImage;
            DetectedLabels = detectedLabels;
            S3Url = s3Url;
            PlaceName = placeName;
        }
    }
}
