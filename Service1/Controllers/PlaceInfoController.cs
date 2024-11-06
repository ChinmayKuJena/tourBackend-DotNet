using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// using GroqPlaceInfoApi.Services;
using GroqPlaceInfoApi.Models;
using System.Diagnostics;
using System.Text.Json;
using Service1.Models;

namespace GroqPlaceInfoApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PlaceInfoController : ControllerBase
    {
        private readonly GroqService _groqService;
        // private readonly KafkaProducer _kafkaProducer;
        // private readonly SqsService _sqsService;

        public PlaceInfoController(GroqService groqService)
        {
            _groqService = groqService;
            // _kafkaProducer = kafkaProducer;
        }
        // public PlaceInfoController(GroqService groqService, KafkaProducer kafkaProducer)
        // {
        //     _groqService = groqService;
        //     _kafkaProducer = kafkaProducer;
        // }

        Stopwatch stopwatch = new Stopwatch();
        [HttpGet("details/{place}")]
        public async Task<IActionResult> GetPlaceDetailsController(string place)
        {
            // Start the stopwatch to measure execution time
            stopwatch.Start();

            if (string.IsNullOrWhiteSpace(place))
            {
                return BadRequest("Place name cannot be empty.");
            }

            // Check if the input is a valid place name
            if (!await _groqService.IsPlaceName(place))
            {
                return NotFound($"'{place}' is not recognized as a valid place name.");
            }

            // Fetch place details if it's a valid place name
            Dictionary<string, object> placeInfo = await _groqService.GetPlaceDetails(place);

            // Serialize the placeInfo to a JSON string
            var message = JsonSerializer.Serialize(placeInfo);

            try
            {
                // _= _kafkaProducer.ProduceMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error producing message: {ex.Message}");
            }


            // Stop the stopwatch and log the elapsed time
            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");

            // Return the response as JSON
            return Ok(placeInfo);
        }

        [HttpGet("historical_facts/{place}")]
        public async Task<IActionResult> GetHistoricalFacts(string place)
        {
            stopwatch.Start();

            if (!await _groqService.IsPlaceName(place))
            {
                return new NotFoundObjectResult($"'{place}' is not recognized as a valid place name.");
            }
            Dictionary<string, object> placeInfo = await _groqService.GetSpecificPlaceDetail(place, "historical_facts");
            var message = JsonSerializer.Serialize(placeInfo);

            try
            {
                // await _kafkaProducer.ProduceMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error producing message: {ex.Message}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");
            return Ok(placeInfo);
            // return await _groqService.GetSpecificPlaceDetail(place, "historical_facts");
        }

        [HttpGet("attractions/{place}")]
        public async Task<IActionResult> GetAttractions(string place)
        {
            stopwatch.Start();
            if (!await _groqService.IsPlaceName(place))
            {
                return new NotFoundObjectResult($"'{place}' is not recognized as a valid place name.");
            }
            Dictionary<string, object> placeInfo = await _groqService.GetSpecificPlaceDetail(place, "attractions");

            var message = JsonSerializer.Serialize(placeInfo);

            try
            {
                // await _kafkaProducer.ProduceMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error producing message: {ex.Message}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");
            return Ok(placeInfo);
            // return await _groqService.GetSpecificPlaceDetail(place, "attractions");
        }

        [HttpGet("famous_places/{place}")]
        public async Task<IActionResult> GetFamousPlaces(string place)
        {
            stopwatch.Start();
            if (!await _groqService.IsPlaceName(place))
            {
                return new NotFoundObjectResult($"'{place}' is not recognized as a valid place name.");
            }

            Dictionary<string, object> placeInfo = await _groqService.GetSpecificPlaceDetail(place, "famous_places");
            var message = JsonSerializer.Serialize(placeInfo);

            try
            {
                // await _kafkaProducer.ProduceMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error producing message: {ex.Message}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");
            return Ok(placeInfo);
        }

        [HttpGet("unique_information/{place}")]
        public async Task<IActionResult> GetUniqueInformation(string place)
        {
            stopwatch.Start();
            if (!await _groqService.IsPlaceName(place))
            {
                return new NotFoundObjectResult($"'{place}' is not recognized as a valid place name.");
            }
            Dictionary<string, object> placeInfo = await _groqService.GetSpecificPlaceDetail(place, "unique_information");
            var message = JsonSerializer.Serialize(placeInfo);

            try
            {
                // await _kafkaProducer.ProduceMessageAsync(message);
            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                Console.WriteLine($"Error producing message: {ex.Message}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");

            return Ok(placeInfo);
            // return await _groqService.GetSpecificPlaceDetail(place, "unique_information");
        }

        [HttpGet("trivia/{place}")]
        public async Task<IActionResult> GetTriviaAndFunFacts(string place)
        {
            stopwatch.Start();

            if (string.IsNullOrWhiteSpace(place))
            {
                return BadRequest("Place name cannot be empty.");
            }
            if (!await _groqService.IsPlaceName(place))
            {
                return NotFound($"'{place}' is not recognized as a valid place name.");
            }

            var triviaData = await _groqService.GetTriviaAndFunFacts(place);

            stopwatch.Stop();
            Console.WriteLine($"Time taken to process '{place}': {stopwatch.ElapsedMilliseconds} ms");

            return Ok(triviaData);
        }

        [HttpPost("compare")]
        public async Task<IActionResult> ComparePlaces([FromBody] PlaceComparisonRequest request)
        {
            stopwatch.Start();

            if (request.Places == null || request.Places.Count < 2)
            {
                return BadRequest("At least two places are required for comparison.");
            }

            var comparisonData = await _groqService.GetPlaceComparison(request.Places, request.Criteria);

            var response = request.Places.Select(place => new PlaceComparisonResponse
            {
                PlaceName = place,
                Details = comparisonData.ContainsKey(place) ? comparisonData[place] : new Dictionary<string, string>()
            }).ToList();

            stopwatch.Stop();
            Console.WriteLine($"Time taken to process : {stopwatch.ElapsedMilliseconds} ms");

            return Ok(response);
        }
    }
}
