using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service1.config;
using Service1.Models;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/")]

public class ImageRecognitionController : ControllerBase
{
    private readonly ImageRecognitionService _imageRecognitionService;
    // private readonly DatabaseService _databaseService;
private readonly DatabaseService _databaseService;
    public ImageRecognitionController(ImageRecognitionService imageRecognitionService, DatabaseService databaseService)
    {
        _imageRecognitionService = imageRecognitionService;
        _databaseService = databaseService;
    }


    [HttpGet("getPlaceImages/{placeName}")]
    public async Task<ActionResult<List<ImageRecognitionResult>>> GetImageRecognitionResultsByPlaceName(string placeName)
    {
        if (string.IsNullOrWhiteSpace(placeName))
        {
            return BadRequest("Place name cannot be empty.");
        }

        var results = await _databaseService.GetImageRecognitionResultsByPlaceNameAsync(placeName);

        if (results == null || results.Count == 0)
        {
            return NotFound($"No results found for place name: {placeName}");
        }

        return Ok(results);
    }

    [HttpPost("recognize")]
    public async Task<IActionResult> RecognizeImage(IFormFile file, [FromForm] string placeName)
    {
        // string placeName="sdcn";
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        const int maxSizeInBytes = 5 * 1024 * 1024; // 5 MB

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);

            // Check if the file exceeds the size limit
            if (memoryStream.Length > maxSizeInBytes)
            {
                // Resize the image
                memoryStream.Position = 0; // Reset position to read from the beginning
                using (var image = Image.FromStream(memoryStream))
                {
                    var resizedImage = ResizeImage(image, 800, 800);
                    memoryStream.SetLength(0); // Clear the stream
                    resizedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memoryStream.Position = 0; // Reset position for further processing
                }
            }
            else
            {
                memoryStream.Position = 0; // Reset position to read from the beginning
            }

            // Pass the (potentially resized) image stream to the service
            var recognitionResult = await _imageRecognitionService.IsPlaceImageAsync(memoryStream, placeName);

            if (!recognitionResult.IsPlaceImage)
            {
                return Ok(new { isPlaceImage = recognitionResult.IsPlaceImage });
            }
            else
            {
                // Save the recognition result to the database
                await _databaseService.SaveImageRecognitionResultAsync(recognitionResult);
                return Ok(new
                {
                    isPlaceImage = recognitionResult.IsPlaceImage,
                    detectedLabels = recognitionResult.DetectedLabels,
                    url = recognitionResult.S3Url
                });
            }
        }
    }

    private Image ResizeImage(Image image, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var newImage = new Bitmap(newWidth, newHeight);
        using (var graphics = Graphics.FromImage(newImage))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);
        }

        return newImage;
    }
}
