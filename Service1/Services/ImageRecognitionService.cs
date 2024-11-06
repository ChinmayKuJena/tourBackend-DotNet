using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Service1.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


    public class ImageRecognitionService
    {
        private readonly IAmazonRekognition _rekognitionClient;
        private readonly IAmazonS3 _s3Client;
        private const string BucketName = "test6766";

        public ImageRecognitionService(IAmazonRekognition rekognitionClient, IAmazonS3 s3Client)
        {
            _rekognitionClient = rekognitionClient;
            _s3Client = s3Client;
        }
        // """ One Of the Best AWS ML Serc "AWS-REKOGNITION" scan and detect that user is just not uploading any type of wrong image!!  """
        public async Task<ImageRecognitionResponse> IsPlaceImageAsync(Stream imageStream, string placeName)
        {
            // Prepare the request for Rekognition
            var detectLabelsRequest = new DetectLabelsRequest
            {
                Image = new Image
                {
                    Bytes = new MemoryStream()
                },
                MaxLabels = 10,
                MinConfidence = 80
            };

            // Read the image stream to byte array
            using (var memoryStream = new MemoryStream())
            {
                await imageStream.CopyToAsync(memoryStream);
                detectLabelsRequest.Image.Bytes = new MemoryStream(memoryStream.ToArray());
            }

            // Call the Rekognition DetectLabels API
            var response = await _rekognitionClient.DetectLabelsAsync(detectLabelsRequest);

            // Prepare response data
            var detectedLabels = new List<DetectedLabel>();
            var placeLabels = new HashSet<string> { "Landmark", "Mountain", "Building", "Nature", "City", "Town" };
            bool isPlaceImage = false;

            // Check if any place-related label is detected
            foreach (var label in response.Labels)
            {
                if (placeLabels.Contains(label.Name))
                {
                    isPlaceImage = true;
                    break;
                }
            }

            // If it's a place image, collect labels with confidence > 98
            if (isPlaceImage)
            {
                foreach (var label in response.Labels)
                {
                    if (label.Confidence > 98)
                    {
                        detectedLabels.Add(new DetectedLabel(label.Name, label.Confidence));
                    }
                }
            }

            // If the image is identified as a place image, upload to S3 and get the URL
            string s3Url = "";
            if (isPlaceImage)
            {
                s3Url = await UploadImageToS3Async(imageStream); // Upload and get the S3 URL
            }

            // Return response with the S3 URL
            return new ImageRecognitionResponse(isPlaceImage, detectedLabels, s3Url, placeName);
        }

        // """ ANother AWS BEST service i.e. S3 , Used for uploading all the Rekognied images to s3 to get a url """
        private async Task<string> UploadImageToS3Async(Stream imageStream)
        {
            // Generate a unique key for the image
            var imageKey = $"images/{Guid.NewGuid()}.jpg";

            // Reset the stream position to ensure it's readable from the start
            imageStream.Position = 0;

            // Prepare the S3 upload request
            var putRequest = new PutObjectRequest
            {
                BucketName = BucketName,
                Key = imageKey,
                InputStream = imageStream,
                ContentType = "image/jpeg",
                CannedACL = S3CannedACL.PublicRead // Make the image publicly accessible
            };

            // Upload the image to S3
            var putResponse = await _s3Client.PutObjectAsync(putRequest);

            // Return the S3 URL of the uploaded image
            return $"https://{BucketName}.s3.amazonaws.com/{imageKey}";
        }




    }
